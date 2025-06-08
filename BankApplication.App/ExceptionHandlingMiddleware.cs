using BankApplication.App.Exceptions;

namespace BankApplication.App
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notFound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFound.Message);
            }
            catch (NotActiveClientException notActive)  
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(notActive.Message);
            }
            catch (Exception ex)
            {

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
