using System.ComponentModel.DataAnnotations;

namespace ExchangesApi.Models;

public class CreateCurrencyModel
{
    [MinLength(1)]
    public string CurrencyCode { get; set; }
}