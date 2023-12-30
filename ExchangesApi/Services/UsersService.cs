using ExchangesApi.Database;
using ExchangesApi.Database.Models;
using ExchangesApi.Models;
using ExchangesApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using User = ExchangesApi.Database.Models.User;

namespace ExchangesApi.Services;

internal class UsersService : IUsersService
{
    private readonly CurrenciesDbContext _dbContext;

    public UsersService(CurrenciesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReturnResult<ErrorType, User>> CreateUserAsync(string name)
    {
        if (await _dbContext.Users.AnyAsync(u => u.Name == name))
        {
            return ReturnResult<ErrorType, User>.CreateError(ErrorType.UserAlreadyExists);
        }

        var newUser = new User { Name = name };
        _dbContext.Users.Add(newUser);
        await _dbContext.SaveChangesAsync();

        return ReturnResult<ErrorType, User>.Create(ErrorType.Ok, newUser);
    }

    public async Task<ReturnResult<ErrorType, bool>> ChangeBalanceAsync(string name, float? newBalance, string currency)
    {
        var user = await _dbContext.Users
            .Include(u => u.Accounts)
            .FirstOrDefaultAsync(u => u.Name == name);

        if (user == null)
        {
            return ReturnResult<ErrorType, bool>.CreateError(ErrorType.UserNotFound);
        }

        var account = user.Accounts.FirstOrDefault(a => a.Currency == currency);

        if (account == null)
        {
            account = new Account { Currency = currency, Balance = 0 };
            user.Accounts.Add(account);
        }

        account.Balance = newBalance ?? 0;

        await _dbContext.SaveChangesAsync();

        return ReturnResult<ErrorType, bool>.Create(ErrorType.Ok, true);
    }

    public async Task<ReturnResult<ErrorType, bool>> ConvertBalanceAsync(Convertation convertModel)
    {
        var user = await _dbContext.Users.Include(u => u.Accounts)
            .FirstOrDefaultAsync(u => u.Name == convertModel.Name);

        if (user == null)
        {
            return ReturnResult<ErrorType, bool>.CreateError(ErrorType.UserNotFound);
        }

        var fromCurrency = convertModel.FromCurrencyCode;
        var toCurrency = convertModel.ToCurrencyCode;
        var amount = convertModel.Amount;
        var exchangeRate = convertModel.ExchangeRate;
        var commissionPercentage =
            convertModel.CommissionPercentage is null or <= 0
                ? MathF.Round(0.05f, 2)
                : convertModel.CommissionPercentage.Value;

        var fromAccount = user.Accounts.FirstOrDefault(a => a.Currency == fromCurrency);
        var toAccount = user.Accounts.FirstOrDefault(a => a.Currency == toCurrency);

        if (fromAccount == null || toAccount == null)
        {
            return ReturnResult<ErrorType, bool>.CreateError(ErrorType.AccountNotFound);
        }

        var finalAmount = convertModel.Amount * exchangeRate;
        var commission = finalAmount * commissionPercentage;

        if (fromAccount.Balance < amount)
        {
            return ReturnResult<ErrorType, bool>.CreateError(ErrorType.InsufficientFunds);
        }

        fromAccount.Balance -= amount;

        toAccount.Balance += finalAmount - commission;

        await _dbContext.SaveChangesAsync();
        return ReturnResult<ErrorType, bool>.Create(ErrorType.Ok, true);
    }

    public async Task<ReturnResult<ErrorType, List<Account>>> GetUserAccountsAsync(string userName)
    {
        var user = await _dbContext.Users.Include(u => u.Accounts)
            .FirstOrDefaultAsync(u => u.Name == userName);

        return user == null
            ? ReturnResult<ErrorType, List<Account>>.CreateError(ErrorType.UserNotFound)
            : ReturnResult<ErrorType, List<Account>>.Create(ErrorType.Ok, user.Accounts);
    }
}