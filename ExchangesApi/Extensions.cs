using ExchangesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExchangesApi;

internal static class Extensions
{
    public static IActionResult ErrorToActionResult<TModel>(this ControllerBase controller,
        ReturnResult<ErrorType, TModel> result,
        Func<TModel, object>? cast = null)
    {
        switch (result.Error)
        {
            case ErrorType.Ok:
                return controller.Ok(cast == null ? result.Model : cast(result.Model));
            case ErrorType.UserNotFound:
                return controller.NotFound(new { error = "The user does not exist." });
            case ErrorType.UserAlreadyExists:
                return controller.BadRequest(new { error = "The user already exist." });
            case ErrorType.AccountNotFound:
                return controller.BadRequest(new { error = "The user does not have the specified account." });
            case ErrorType.InsufficientFunds:
                return controller.BadRequest(new { error = "No enough money on the user's account." });
            case ErrorType.UnknownError:
            default:
                return controller.StatusCode(500);
        }
    }
}