using System.Net;

namespace PruebaTecnica.Application.Common
{
    public class ResponseMessage<T>(HttpStatusCode statusCode, string message, T? data = default)
    {
        public HttpStatusCode StatusCode { get; set; } = statusCode;
        public string Message { get; set; } = message;
        public T? Data { get; set; } = data;
        public bool IsSuccess => ((int)StatusCode >= 200 && (int)StatusCode < 300); 

        public static ResponseMessage<T> SuccessResponse(T data, string message = "Operación exitosa")
        {
            return new ResponseMessage<T>(HttpStatusCode.OK, message, data);
        }

        public static ResponseMessage<T> ErrorResponse(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ResponseMessage<T>(statusCode, message);
        }
    }
}
