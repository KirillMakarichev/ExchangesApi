using ExchangesApi.Models;
using ExchangesApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExchangesApi.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _userService;

    public UsersController(IUsersService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserModel createUserModel)
    {
        try
        {
            var result = await _userService.CreateUserAsync(createUserModel.Username);

            return this.ErrorToActionResult(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("balance")]
    public async Task<IActionResult> ChangeBalance(ChangeBalanceModel changeBalanceModel)
    {
        if (changeBalanceModel.NewBalance < 0)
            return BadRequest(new { message = $"{nameof(Convertation.Amount)} must be 0 or greater." });
        
        try
        {
            var result = await _userService.ChangeBalanceAsync(
                changeBalanceModel.UserName,
                changeBalanceModel.NewBalance,
                changeBalanceModel.Currency
            );

            return this.ErrorToActionResult(result, changed => new { changed });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("accounts/{userName}")]
    public async Task<IActionResult> GetUserAccounts(string userName)
    {
        try
        {
            var result = await _userService.GetUserAccountsAsync(userName);

            return this.ErrorToActionResult(result, accounts =>
            {
                var convertedAccounts = accounts.Select(x => new AccountModel()
                {
                    Balance = x.Balance,
                    CurrencyCode = x.Currency
                });

                return new
                {
                    count = accounts.Count,
                    accounts = convertedAccounts
                };
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}