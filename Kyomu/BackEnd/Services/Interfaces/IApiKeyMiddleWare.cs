namespace BackEnd.Services.Interfaces
{
    public interface IApiKeyMiddleWare
    {
        Task InvokeAsync(HttpContext context);
    }
}
