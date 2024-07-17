using Newtonsoft.Json;
using System.Net;

namespace OnlineStore.Infrastructure.Contracts;

public class ApiResult
{
    public bool IsSuccess { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Message { get; set; }
    public Pagination.Pagination? Pagination { get; set; }
    public string ExceptionMessage { get; set; }

    private static Dictionary<HttpStatusCode, string> Messages { get; } = new()
    {
        { HttpStatusCode.OK, "OK" },
        { HttpStatusCode.Created, "Created" },
        { HttpStatusCode.BadRequest, "Bad Request" },
        { HttpStatusCode.Unauthorized, "Unauthorized" },
        { HttpStatusCode.Forbidden, "Forbidden" },
        { HttpStatusCode.NotFound, "Not Found" },
        { HttpStatusCode.InternalServerError, "Internal Server Error" },
        { HttpStatusCode.NotImplemented, "Not Implemented" },
        { HttpStatusCode.BadGateway, "Bad Gateway" },
        { HttpStatusCode.ServiceUnavailable, "Service Unavailable" },
        { HttpStatusCode.GatewayTimeout, "Gateway Timeout" },
    };

    public ApiResult(bool isSuccess, HttpStatusCode statusCode, string? message = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message ?? Messages[statusCode];
    }

    public void Error(string exceptionMessage)
    {
        ExceptionMessage = exceptionMessage;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented
        });
    }
}

public class ApiResult<TData> : ApiResult where TData : class
{
    //[JsonProperty(NullValueHandling = NullValueHandling.Ignore), ]
    [JsonProperty("data")] public TData Data { get; set; }

    public ApiResult(bool isSuccess, HttpStatusCode statusCode, TData data, string message = null,
        int? totalItemCount = null, int? pageNumber = null, int? pageSize = null, int? pageCount = null)
        : base(isSuccess, statusCode, message)
    {
        Data = data;
        Pagination = new Pagination.Pagination()
        {
            PageNumber = pageNumber,
            TotalItemCount = totalItemCount,
            PageSize = pageSize,
            PageCount = pageCount
        };
    }
}