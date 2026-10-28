using SGCAE.Domain.Exceptions;

namespace SGCAE.Domain.ValueObjects;

//  la identificación de un empleado. Se valida sola al crearse.
public sealed record Identificacion
{
    public const int LongitudMaxima = 20;

    public string Valor { get; }

    public Identificacion(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new BusinessRuleException("La identificación es requerida.");
        }

        valor = valor.Trim();

        if (valor.Length > LongitudMaxima)
        {
            throw new BusinessRuleException($"La identificación no puede superar los {LongitudMaxima} caracteres.");
        }

        Valor = valor;
    }
}