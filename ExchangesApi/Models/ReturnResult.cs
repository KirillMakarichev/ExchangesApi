namespace ExchangesApi.Models;

public class ReturnResult<TError, TModel>
    where TError : struct
{
    public TError Error { get; set; }
    public TModel Model { get; set; }

    public static ReturnResult<TError, TModel> CreateError(TError error)
    {
        return new ReturnResult<TError, TModel>()
        {
            Error = error,
            Model = default
        };
    }
    
    public static ReturnResult<TError, TModel> Create(TError error, TModel model)
    {
        return new ReturnResult<TError, TModel>()
        {
            Error = error,
            Model = model
        };
    }
}