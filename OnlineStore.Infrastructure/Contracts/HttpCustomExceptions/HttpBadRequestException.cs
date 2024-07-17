using System.Net;

namespace OnlineStore.Infrastructure.Contracts
{
    public class HttpBadRequestException : AppException
    {

        public HttpBadRequestException()
        : base (HttpStatusCode.BadRequest)
        {
        }
        public HttpBadRequestException(string message)
            : base(message, HttpStatusCode.BadRequest)
        {
        }

        public HttpBadRequestException(string message, Exception innerException)
            : base(message, innerException, HttpStatusCode.BadRequest)
        {
        }
    }
}
