using Microsoft.AspNetCore.Mvc.Filters;
using Xan.AspNetCore.Http;
using Xan.AspNetCore.Parameter;

namespace Xan.AspNetCore.Mvc.Filters;

public sealed class PageSizeFilter
    : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        ListParameter? parameter = context.FirstOrDefaultArgument<ListParameter>();
        if (parameter != null)
        {
            string cookieKey = PageSizeCookie.FindKey(context.RouteData.Values);
            if (parameter.PageSize.HasValue)
            {
                context.HttpContext.Response.Cookies.SetPageSize(cookieKey, parameter.PageSize.Value);
            }
            else
            {
                if (context.HttpContext.Request.Cookies.HasPageSize(cookieKey))
                {
                    parameter.PageSize = context.HttpContext.Request.Cookies.GetPageSize(cookieKey);
                }
                else
                {
                    parameter.PageSize = ListParameter.DefaultPageSize;
                }
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    { }
}
