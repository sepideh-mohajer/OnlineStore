using System.Net;

namespace OnlineStore.Infrastructure.Contracts
{
    public class HttpInternalServerErrorException : AppException
    {
        public HttpInternalServerErrorException()
        : base(HttpStatusCode.InternalServerError)
        {
        }

        public HttpInternalServerErrorException(string message)
            : base(message, HttpStatusCode.InternalServerError)
        {
        }

        public HttpInternalServerErrorException(string message, Exception innerException)
            : base(message, innerException, HttpStatusCode.InternalServerError)
        {
        }
    }
}
