using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Infrastructure.Contracts
{
    public class JsonResult : ActionResult
    {
        public object Value { get; }
        public int? StatusCode { get; set; }

        public JsonResult(object value)
        {
            Value = value;
        }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = context.HttpContext.Response;
            response.ContentType = "application/json";

            if (StatusCode.HasValue)
            {
                response.StatusCode = StatusCode.Value;
            }

            var json = System.Text.Json.JsonSerializer.Serialize(Value);
            await response.WriteAsync(json);
        }
    }
}