using SGCAE.Application.Contracts;
using SGCAE.Application.Contracts.Repositories;
using SGCAE.Application.Exceptions;
using SGCAE.Application.Utilities.Mediator;
using SGCAE.Domain.Entities;
using SGCAE.Domain.Enums;
using SGCAE.Domain.Exceptions;
using SGCAE.Domain.ValueObjects;

namespace SGCAE.Application.UseCases.Accesos.Commands.RegistrarAcceso;

// Caso de uso central del sistema: valida una solicitud de acceso y registra el resultado.
// Un acceso rechazado NO es un error: se guarda con su motivo y la API responde normalmente.
public class RegistrarAccesoUseCase : IRequestHandler<RegistrarAccesoCommand, RegistroAccesoDTO>
{
    private readonly ICredencialRepository _credenciales;
    private readonly IEmpleadoRepository _empleados;
    private readonly IPuntoAccesoRepository _puntos;
    private readonly IRegistroAccesoRepository _registros;
    private readonly IUnitOfWork _unitOfWork;

    public RegistrarAccesoUseCase(ICredencialRepository credenciales, IEmpleadoRepository empleados,
                                  IPuntoAccesoRepository puntos, IRegistroAccesoRepository registros,
                                  IUnitOfWork unitOfWork)
    {
        _credenciales = credenciales;
        _empleados = empleados;
        _puntos = puntos;
        _registros = registros;
        _unitOfWork = unitOfWork;
    }

    public async Task<RegistroAccesoDTO> Handle(RegistrarAccesoCommand request)
    {
        // 1. Datos mal formados: son errores de la petición y no se registran.
        if (!Enum.IsDefined(request.Tipo))
        {
            throw new BusinessRuleException("El tipo de movimiento no es válido.");
        }

        CodigoCredencial codigo = new(request.Codigo);

        PuntoAcceso punto = await _puntos.GetByIdAsync(request.PuntoAccesoId)
            ?? throw new NotFoundException("El punto de acceso no existe.");

        // 2. Evalúa las condiciones y obtiene el registro (autorizado o rechazado).
        RegistroAcceso registro = await EvaluarAsync(codigo, punto, request.Tipo);

        // 3. Se guarda siempre, también los rechazados: es el historial del sistema.
        await _registros.AddAsync(registro);
        await _unitOfWork.CommitAsync();

        return registro.ToDto();
    }

    // Las condiciones se revisan en este orden; la primera que falle es el motivo del rechazo.
    private async Task<RegistroAcceso> EvaluarAsync(CodigoCredencial codigo, PuntoAcceso punto, TipoMovimiento tipo)
    {
        // a) La credencial debe existir.
        Credencial? credencial = await _credenciales.GetByCodigoAsync(codigo);

        if (credencial is null)
        {
            return RegistroAcceso.CrearRechazado(codigo, null, punto.Id, tipo, MotivoRechazo.CredencialNoEncontrada);
        }

        Guid empleadoId = credencial.EmpleadoId;

        // b) La credencial debe estar vigente (no bloqueada ni revocada).
        if (!credencial.PuedeUsarse)
        {
            return RegistroAcceso.CrearRechazado(codigo, empleadoId, punto.Id, tipo, MotivoRechazo.CredencialNoVigente);
        }

        // c) El empleado debe estar activo.
        Empleado empleado = await _empleados.GetByIdAsync(empleadoId)
            ?? throw new NotFoundException("El empleado de la credencial no existe.");

        if (!empleado.EstaActivo)
        {
            return RegistroAcceso.CrearRechazado(codigo, empleadoId, punto.Id, tipo, MotivoRechazo.EmpleadoInactivo);
        }

        // d) El punto de acceso debe estar habilitado.
        if (!punto.Habilitado)
        {
            return RegistroAcceso.CrearRechazado(codigo, empleadoId, punto.Id, tipo, MotivoRechazo.PuntoAccesoDeshabilitado);
        }

        // e) Coherencia del movimiento: no se entra dos veces seguidas ni se sale sin haber entrado.
        TipoMovimiento? ultimo = await _registros.GetUltimoMovimientoAutorizadoAsync(empleadoId);

        if (tipo == TipoMovimiento.Entrada && ultimo == TipoMovimiento.Entrada)
        {
            return RegistroAcceso.CrearRechazado(codigo, empleadoId, punto.Id, tipo, MotivoRechazo.YaSeEncuentraAdentro);
        }

        if (tipo == TipoMovimiento.Salida && ultimo != TipoMovimiento.Entrada)
        {
            return RegistroAcceso.CrearRechazado(codigo, empleadoId, punto.Id, tipo, MotivoRechazo.NoHaIngresado);
        }

        // Todas las condiciones se cumplen: acceso autorizado.
        return RegistroAcceso.CrearAutorizado(codigo, empleadoId, punto.Id, tipo);
    }
}
