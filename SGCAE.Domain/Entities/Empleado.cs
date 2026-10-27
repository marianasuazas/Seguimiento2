using SGCAE.Domain.Enums;
using SGCAE.Domain.Exceptions;
using SGCAE.Domain.ValueObjects;

namespace SGCAE.Domain.Entities;


public sealed class Empleado
{
    public const int LongitudMaximaTexto = 100;

    
    public Guid Id { get; private set; }
    public Identificacion Identificacion { get; private set; } = null!;
    public string Nombres { get; private set; } = null!;
    public string Apellidos { get; private set; } = null!;
    public CorreoElectronico Correo { get; private set; } = null!;
    public string Cargo { get; private set; } = null!;
    public string Area { get; private set; } = null!;
    public EstadoLaboral Estado { get; private set; }

    
    public bool EstaActivo => Estado == EstadoLaboral.Activo;

    
    private Empleado()
    {
    }

    public Empleado(Identificacion identificacion, string nombres, string apellidos,
                    CorreoElectronico correo, string cargo, string area)
    {
        Id = Guid.CreateVersion7();
        Identificacion = identificacion;
        Nombres = ValidarTexto(nombres, "nombres");
        Apellidos = ValidarTexto(apellidos, "apellidos");
        Correo = correo;
        Cargo = ValidarTexto(cargo, "cargo");
        Area = ValidarTexto(area, "área");
        Estado = EstadoLaboral.Activo;
    }

    
    public void Actualizar(string nombres, string apellidos, CorreoElectronico correo, string cargo, string area)
    {
        
        string nuevosNombres = ValidarTexto(nombres, "nombres");
        string nuevosApellidos = ValidarTexto(apellidos, "apellidos");
        string nuevoCargo = ValidarTexto(cargo, "cargo");
        string nuevaArea = ValidarTexto(area, "área");

        Nombres = nuevosNombres;
        Apellidos = nuevosApellidos;
        Correo = correo;
        Cargo = nuevoCargo;
        Area = nuevaArea;
    }

    public void Desactivar()
    {
        if (Estado == EstadoLaboral.Inactivo)
        {
            throw new BusinessRuleException("El empleado ya está inactivo.");
        }

        Estado = EstadoLaboral.Inactivo;
    }

    private static string ValidarTexto(string valor, string campo)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new BusinessRuleException($"El campo {campo} es requerido.");
        }

        valor = valor.Trim();

        if (valor.Length > LongitudMaximaTexto)
        {
            throw new BusinessRuleException($"El campo {campo} no puede superar los {LongitudMaximaTexto} caracteres.");
        }

        return valor;
    }
}