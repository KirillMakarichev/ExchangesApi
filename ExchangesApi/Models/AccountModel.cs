using System.ComponentModel.DataAnnotations;

namespace ExchangesApi.Models;

internal class AccountModel
{
    public string CurrencyCode { get; set; }
    public float Balance { get; set; }
}