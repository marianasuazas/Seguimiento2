using System.Text.RegularExpressions;
using SGCAE.Domain.Exceptions;

namespace SGCAE.Domain.ValueObjects;

// Value object: el correo electrónico de un empleado. Se valida solo al crearse y se guarda en minúsculas.
public sealed record CorreoElectronico
{
    public const int LongitudMaxima = 100;

   
    private static readonly Regex Formato = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    public string Valor { get; }

    public CorreoElectronico(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new BusinessRuleException("El correo electrónico es requerido.");
        }

        
        valor = valor.Trim().ToLowerInvariant();

        if (valor.Length > LongitudMaxima)
        {
            throw new BusinessRuleException($"El correo electrónico no puede superar los {LongitudMaxima} caracteres.");
        }

        if (!Formato.IsMatch(valor))
        {
            throw new BusinessRuleException("El correo electrónico no tiene un formato válido.");
        }

        Valor = valor;
    }
}