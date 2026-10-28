namespace SGCAE.Api.Requests;


public sealed record ActualizarEmpleadoRequest(
    string Nombres,
    string Apellidos,
    string Correo,
    string Cargo,
    string Area);
