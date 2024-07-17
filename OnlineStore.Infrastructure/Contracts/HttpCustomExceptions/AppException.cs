using System.Net;

namespace OnlineStore.Infrastructure.Contracts
{
    public class AppException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }

        public AppException(string message, HttpStatusCode httpStatusCode)
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public AppException(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;

        }

        public AppException(string message, Exception innerException, HttpStatusCode httpStatusCode)
            : base(message, innerException)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
