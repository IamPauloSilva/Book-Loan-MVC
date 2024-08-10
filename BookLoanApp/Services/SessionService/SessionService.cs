using BookLoanApp.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace BookLoanApp.Services.SessionService
{
    public class SessionService : ISessionInterface
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void CreateSession(UserModel userModel)
        {
            string userJson = JsonConvert.SerializeObject(userModel);
            _httpContextAccessor.HttpContext.Session.SetString("UserSession", userJson);
        }

        public UserModel GetSession()
        {
            string userSession = _httpContextAccessor.HttpContext.Session.GetString("UserSession");
            if (string.IsNullOrEmpty(userSession))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<UserModel>(userSession);
        }

        public void RemoveSession()
        {
            _httpContextAccessor.HttpContext.Session.Remove("UserSession");
        }
    }
}
