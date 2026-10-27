using System.Text.RegularExpressions;
using SGCAE.Domain.Exceptions;

namespace SGCAE.Domain.ValueObjects;


public sealed record CodigoCredencial
{
    public const int LongitudMaxima = 50;

    
    private static readonly Regex Formato = new(@"^[A-Z0-9\-]+$", RegexOptions.Compiled);

    public string Valor { get; }

    public CodigoCredencial(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new BusinessRuleException("El código de la credencial es requerido.");
        }

        
        valor = valor.Trim().ToUpperInvariant();

        if (valor.Length > LongitudMaxima)
        {
            throw new BusinessRuleException($"El código de la credencial no puede superar los {LongitudMaxima} caracteres.");
        }

        if (!Formato.IsMatch(valor))
        {
            throw new BusinessRuleException("El código de la credencial solo puede contener letras, números y guiones.");
        }

        Valor = valor;
    }
}