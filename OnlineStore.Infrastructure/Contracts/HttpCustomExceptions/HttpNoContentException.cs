using System.Net;

namespace OnlineStore.Infrastructure.Contracts
{
    public class HttpNoContentException : AppException
    {
        public HttpNoContentException()
            : base(HttpStatusCode.NoContent)
        {
        }

        public HttpNoContentException(string message)
            : base(message, HttpStatusCode.NoContent)
        {
        }

        public HttpNoContentException(string message, Exception innerException)
            : base(message, innerException, HttpStatusCode.NoContent)
        {
        }
    }
}