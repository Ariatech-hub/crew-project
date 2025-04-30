using FarmToFork.Core.Exception;
using FarmToFork.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FarmToFork.Application.Filter
{
    public class CheckAccessForMenu : ActionFilterAttribute

    {
        public int MenuCode { get; set; }


        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var menuCode = MenuCode;
            var userName = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "username")?.Value;
            var menuRepository =
                (IMenuRepository)context.HttpContext.RequestServices.GetService(typeof(IMenuRepository))!;
            var hasAccess = await menuRepository.HasAccessForMenu(menuCode, userName ?? throw new DataValidationException("Invalid User name"));
            if (!hasAccess)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "You are not authorized to access this resource"
                };
            }
            else
            {
                await next();
            }

        }

       
    }
}
