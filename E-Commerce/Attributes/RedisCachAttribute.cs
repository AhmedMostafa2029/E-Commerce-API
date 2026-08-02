using E_Commerce.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_Commerce.API.Attributes
{
    public class RedisCachAttribute : ActionFilterAttribute
    {
        private readonly int _durationInSec;

        public RedisCachAttribute(int durationInSec)
        {
            _durationInSec = durationInSec;
        }


        public override async Task OnActionExecutionAsync(ActionExecutingContext context , ActionExecutionDelegate next)
        {
            // Get Cache Service From DI Container
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheServices>();
            var cacheKey = CreateCacheKey(context.HttpContext.Request);

            // Check if cached Data Exsists
            var cached = await cacheService.GetAsync(cacheKey);

            //if Exsists ,Return Cache Data and Skip Exceution of EndPoint
            if (!string.IsNullOrEmpty(cached))
            {
                context.Result = new ContentResult()
                {
                    Content = cached,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            //if not Exsists , Execute EndPoint , and Store The Result in Cache if 200 ok Response
            var Executed = await next.Invoke();
            if(Executed.Result is OkObjectResult {Value: not null } ok)
            {
                await cacheService.SetAsync(cacheKey, ok.Value, TimeSpan.FromSeconds(_durationInSec));
            }
            return;
        }

        private static string CreateCacheKey(HttpRequest request)
        {
            // Path
            // api/Product?
            var key = new StringBuilder();
            key.Append(request.Path).Append("?");

            foreach(var (k,v) in request.Query.OrderBy(q => q.Key))
            {
                key.Append(k).Append("=").Append(v).Append("&");
            }
            return key.ToString();
        }

    }
}
