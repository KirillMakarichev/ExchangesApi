using System.ComponentModel.DataAnnotations;

namespace ExchangesApi.Models;

public class CreateUserModel
{
    [MinLength(1)]
    public string Username { get; set; }
}