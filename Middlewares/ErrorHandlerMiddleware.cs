using System.Net;
using System.Text.Json;
using liquorApi.Exceptions;

namespace liquorApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        
        private readonly RequestDelegate _request;


        public ErrorHandlerMiddleware(RequestDelegate request)
        {
            this._request = request;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this._request(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responeModel = new Dictionary<object, object>();
                responeModel.Add("message", exception.Message);

                switch (exception)
                {
                    case ApiExceptions ex:
                    response.StatusCode = (int) HttpStatusCode.BadRequest;

                    break;
                    case UserNotFoundException ex:
                    response.StatusCode = (int) HttpStatusCode.NotFound;
                    responeModel["message"] = ex.Message;    

                    break;
                    case ProductNotFoundException ex:
                    response.StatusCode = (int) HttpStatusCode.NotFound;
                    responeModel["message"] = ex.Message;   

                    break;
                    default:
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;

                    break;
                }

                var result = JsonSerializer.Serialize(responeModel);
                
                await context.Response.WriteAsync(result);
            }
        }
    }
}