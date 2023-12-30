using ExchangesApi.Models;
using ExchangesApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExchangesApi.Controllers;

[ApiController]
[Route("api/currencies")]
public class CurrenciesController : ControllerBase
{
    private readonly ICurrenciesService _currenciesService;

    public CurrenciesController(ICurrenciesService currenciesService)
    {
        _currenciesService = currenciesService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCurrency(CreateCurrencyModel createCurrencyModel)
    {
        try
        {
            var result = await _currenciesService.CreateCurrencyAsync(createCurrencyModel.CurrencyCode);
            return this.ErrorToActionResult(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}