using BookLoanApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookLoanApp.ViewComponents
{
    public class Menu : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string userSession = HttpContext.Session.GetString("UserSession");

            if (string.IsNullOrEmpty(userSession)) return View();
            
            UserModel user = JsonConvert.DeserializeObject<UserModel>(userSession);
            return View(user);
        }
    }
}
