using SGCAE.Domain.Enums;
using SGCAE.Domain.Exceptions;
using SGCAE.Domain.ValueObjects;

namespace SGCAE.Domain.Entities;


public sealed class Credencial
{
    public Guid Id { get; private set; }
    public CodigoCredencial Codigo { get; private set; } = null!;

    
    public Guid EmpleadoId { get; private set; }

    public EstadoCredencial Estado { get; private set; }
    public DateTime FechaAsignacion { get; private set; }

    
    public bool PuedeUsarse => Estado == EstadoCredencial.Vigente;

    
    private Credencial()
    {
    }

    public Credencial(Guid empleadoId, CodigoCredencial codigo)
    {
        if (empleadoId == Guid.Empty)
        {
            throw new BusinessRuleException("El empleado es requerido.");
        }

        Id = Guid.CreateVersion7();
        EmpleadoId = empleadoId;
        Codigo = codigo;
        Estado = EstadoCredencial.Vigente;
        FechaAsignacion = DateTime.UtcNow;
    }

    public void Bloquear()
    {
        if (Estado == EstadoCredencial.Revocada)
        {
            throw new BusinessRuleException("No se puede bloquear una credencial revocada.");
        }

        if (Estado == EstadoCredencial.Bloqueada)
        {
            throw new BusinessRuleException("La credencial ya está bloqueada.");
        }

        Estado = EstadoCredencial.Bloqueada;
    }

    public void Desbloquear()
    {
        if (Estado != EstadoCredencial.Bloqueada)
        {
            throw new BusinessRuleException("Solo se puede desbloquear una credencial bloqueada.");
        }

        Estado = EstadoCredencial.Vigente;
    }

    public void Revocar()
    {
        if (Estado == EstadoCredencial.Revocada)
        {
            throw new BusinessRuleException("La credencial ya está revocada.");
        }

        Estado = EstadoCredencial.Revocada;
    }
}