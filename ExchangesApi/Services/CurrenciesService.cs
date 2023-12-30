using ExchangesApi.Database;
using ExchangesApi.Database.Models;
using ExchangesApi.Models;
using ExchangesApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExchangesApi.Services;

internal class CurrenciesService : ICurrenciesService
{
    private readonly CurrenciesDbContext _dbContext;

    public CurrenciesService(CurrenciesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReturnResult<ErrorType, Currency>> CreateCurrencyAsync(string currencyCode)
    {
        if (await _dbContext.Currencies.AnyAsync(c => c.Code == currencyCode))
        {
            return ReturnResult<ErrorType, Currency>.CreateError(ErrorType.CurrencyAlreadyExists);
        }

        var newCurrency = new Currency { Code = currencyCode };
        _dbContext.Currencies.Add(newCurrency);
        await _dbContext.SaveChangesAsync();

        return ReturnResult<ErrorType, Currency>.Create(ErrorType.Ok, newCurrency);
    }
}