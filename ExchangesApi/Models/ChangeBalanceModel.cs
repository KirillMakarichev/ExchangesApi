using System.ComponentModel.DataAnnotations;

namespace ExchangesApi.Models;

public class ChangeBalanceModel
{
    [MinLength(1)]
    public string UserName { get; set; }
    
    public float? NewBalance { get; set; }
    
    [MinLength(1)]
    public string Currency { get; set; }
}