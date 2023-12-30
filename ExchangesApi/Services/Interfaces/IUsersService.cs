using ExchangesApi.Database.Models;
using ExchangesApi.Models;
using User = ExchangesApi.Database.Models.User;

namespace ExchangesApi.Services.Interfaces;

public interface IUsersService
{
    Task<ReturnResult<ErrorType, User>> CreateUserAsync(string name);
    Task<ReturnResult<ErrorType, bool>> ChangeBalanceAsync(string name, float? newBalance, string currency);
    Task<ReturnResult<ErrorType, bool>> ConvertBalanceAsync(Convertation convertModel);
    Task<ReturnResult<ErrorType, List<Account>>> GetUserAccountsAsync(string userName);
}