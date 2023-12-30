using ExchangesApi.Database.Models;
using ExchangesApi.Models;

namespace ExchangesApi.Services.Interfaces;

public interface ICurrenciesService
{
    Task<ReturnResult<ErrorType, Currency>> CreateCurrencyAsync(string currencyCode);
}