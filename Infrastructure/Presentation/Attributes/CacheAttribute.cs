using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    public class CacheAttribute(int DurationInSec ) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var CacheService= context.HttpContext.RequestServices.GetRequiredService<IServiceManger>().cacheService;

            var CacheKey = GeneratedCacheKey(context.HttpContext.Request);
           var res=await  CacheService.GetCacheValueAsync(CacheKey);
            if (!string.IsNullOrEmpty(res))
            {
                context.Result = new ContentResult()
                {
                    Content = res,
                    ContentType="application/json",
                    StatusCode=StatusCodes.Status200OK
                };
                return;
            }
            var ContentResult= await next.Invoke();
            if (ContentResult.Result is OkObjectResult okObject)
            {
               await CacheService.SetCacheValueAsync(CacheKey, okObject.Value, TimeSpan.FromSeconds(DurationInSec));
            }
            

        }

        private string GeneratedCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);
            foreach (var item in request.Query.OrderBy(o=>o.Key))
            {
                key.Append($"|{item.Key}-{item.Value}");
            }
            return key.ToString();  
        }
    }
}
