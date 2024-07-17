using System.Net;

namespace OnlineStore.Infrastructure.Contracts
{
    public class HttpForbiddenException : AppException
    {
        public HttpForbiddenException()
        : base(HttpStatusCode.Forbidden)
        {
        }

        public HttpForbiddenException(string message)
            : base(message, HttpStatusCode.Forbidden)
        {
        }

        public HttpForbiddenException(string message, Exception innerException)
            : base(message, innerException, HttpStatusCode.Forbidden)
        {
        }
    }
}
