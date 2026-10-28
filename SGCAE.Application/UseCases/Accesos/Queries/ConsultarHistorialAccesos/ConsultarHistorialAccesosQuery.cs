using SGCAE.Application.Utilities.Mediator;
using SGCAE.Application.Utilities.Pagination;
using SGCAE.Domain.Enums;

namespace SGCAE.Application.UseCases.Accesos.Queries.ConsultarHistorialAccesos;

// Consulta: "quiero el historial de accesos", con filtros opcionales y paginado.
// Las fechas se comparan en UTC, igual que como se guardan.
public class ConsultarHistorialAccesosQuery : IRequest<PaginationResponse<RegistroAccesoDTO>>
{
    public Guid? EmpleadoId { get; set; }
    public DateTime? Desde { get; set; }
    public DateTime? Hasta { get; set; }
    public Guid? PuntoAccesoId { get; set; }
    public ResultadoAcceso? Resultado { get; set; }

    public PaginationRequest Pagination { get; set; } = PaginationRequest.Default();
}
