using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineStore.Infrastructure.Contracts;
using OnlineStore.Infrastructure.Pagination;
using System.Net;

namespace OnlineStore.Infrastructure.Filters;

public class ApiResultFilterAttribute : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            var statusCode = (HttpStatusCode)objectResult.StatusCode.GetValueOrDefault(500);
            var isSuccess = statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Created;

            var apiResult = new ApiResult<object>(isSuccess, statusCode, objectResult.Value);
            
            var valueType = objectResult.Value?.GetType();
            if (valueType != null && valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(PagedList<>))
            {
                var pagedListType = typeof(PagedList<>).MakeGenericType(valueType.GetGenericArguments());
                var pagedListInstance = Convert.ChangeType(objectResult.Value, pagedListType);
                
                FillPaginationParameters(apiResult, pagedListInstance);
            }
            
            context.Result = new ObjectResult(apiResult)
            {
                StatusCode = objectResult.StatusCode
            };
        }
        else if (context.Result is StatusCodeResult statusCodeResult)
        {
            var statusCode = (HttpStatusCode)statusCodeResult.StatusCode;
            var isSuccess = statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Created;

            var apiResult = new ApiResult(isSuccess, statusCode);

            context.Result = new ObjectResult(apiResult)
            {
                StatusCode = statusCodeResult.StatusCode
            };
        }
        else if (context.Result is ContentResult contentResult)
        {
            var apiResult = new ApiResult<string>(true, HttpStatusCode.OK, contentResult.Content);

            context.Result = new ObjectResult(apiResult)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
        else if (context.Result is BadRequestObjectResult badRequestObjectResult)
        {
            var apiResult = badRequestObjectResult;

            context.Result = new ObjectResult(apiResult)
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
        else if (context.Result is NotFoundObjectResult notFoundObjectResult)
        {
            var apiResult = notFoundObjectResult;

            context.Result = new ObjectResult(apiResult)
            {
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }
        
        base.OnResultExecuting(context);
    }

    private void FillPaginationParameters(ApiResult<object> apiResult, object pagedList)
    {
        var totalCountProperty = pagedList.GetType().GetProperty("TotalCount");
        var currentPageProperty = pagedList.GetType().GetProperty("CurrentPage");
        var pageSizeProperty = pagedList.GetType().GetProperty("PageSize");
        var totalPagesProperty = pagedList.GetType().GetProperty("TotalPages");

        if (totalCountProperty != null && currentPageProperty != null && pageSizeProperty != null && totalPagesProperty != null)
        {
            var pagination = new Pagination.Pagination()
            {
                TotalItemCount = (int)totalCountProperty.GetValue(pagedList),
                PageNumber = (int)currentPageProperty.GetValue(pagedList),
                PageSize = (int)pageSizeProperty.GetValue(pagedList),
                PageCount = (int)totalPagesProperty.GetValue(pagedList)
            };

            apiResult.Pagination = pagination;
        }
    }

}