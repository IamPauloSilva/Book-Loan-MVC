using BookLoanApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;

namespace BookLoanApp.Filters
{
    public class LoggedUser : ActionFilterAttribute
    {

        public override void OnActionExecuted(ActionExecutedContext context)
        {

            string userSession = context.HttpContext.Session.GetString("UserSession");
            if (string.IsNullOrEmpty(userSession))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "Action", "Login" }
                });
            }
            else
            {
                UserModel user = JsonConvert.DeserializeObject<UserModel>(userSession);

                if (user == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        { "controller", "Home" },
                        { "Action", "Login" }
                    });
                }
            }

            base.OnActionExecuted(context);
        }
    }
}
