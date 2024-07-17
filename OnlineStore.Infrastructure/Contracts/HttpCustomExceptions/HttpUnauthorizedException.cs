using System.Net;

namespace OnlineStore.Infrastructure.Contracts
{
    public class HttpUnAuthorizedException : AppException
    {
        public HttpUnAuthorizedException()
        : base(HttpStatusCode.Unauthorized)
        {
        }

        public HttpUnAuthorizedException(string message)
            : base(message, HttpStatusCode.Unauthorized)
        {
        }

        public HttpUnAuthorizedException(string message, Exception innerException)
            : base(message, innerException, HttpStatusCode.Unauthorized)
        {
        }
    }
}
