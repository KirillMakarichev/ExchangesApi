namespace ExchangesApi.Models;

public enum ErrorType
{
    Ok,
    UserNotFound,
    UserAlreadyExists,
    CurrencyAlreadyExists,
    AccountNotFound,
    InsufficientFunds,
    UnknownError
}