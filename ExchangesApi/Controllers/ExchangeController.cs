using ExchangesApi.Models;
using ExchangesApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExchangesApi.Controllers;

[ApiController]
[Route("api/exchange")]
public class ExchangeController : ControllerBase
{
    private readonly IUsersService _userService;

    public ExchangeController(IUsersService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> ConvertBalance(Convertation convertModel)
    {
        if (convertModel.Amount <= 0)
            return BadRequest(new { message = $"{nameof(Convertation.Amount)} must be greater than 0." });
        if (convertModel.ExchangeRate <= 0)
            return BadRequest(new { message = $"{nameof(Convertation.ExchangeRate)} must be greater than 0." });

        try
        {
            var result = await _userService.ConvertBalanceAsync(convertModel);
            return this.ErrorToActionResult(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}