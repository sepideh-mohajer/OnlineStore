using System.Net;

namespace OnlineStore.Infrastructure.Contracts
{
    public class HttpNotFoundException : AppException
    {
        public HttpNotFoundException()
        : base(HttpStatusCode.NotFound)
        {
        }

        public HttpNotFoundException(string message)
            : base(message, HttpStatusCode.NotFound)
        {
        }

        public HttpNotFoundException(string message, Exception innerException)
            : base(message, innerException, HttpStatusCode.NotFound)
        {
        }
    }
}
