using System.ComponentModel.DataAnnotations;

namespace ExchangesApi.Models;

public class Convertation
{
    [MinLength(1)]
    public string Name { get; set; }
    
    [MinLength(1)]
    public string FromCurrencyCode { get; set; }
    
    [MinLength(1)]
    public string ToCurrencyCode { get; set; }
    public float Amount { get; set; }
    public float ExchangeRate { get; set; }
    public float? CommissionPercentage { get; set; }
}