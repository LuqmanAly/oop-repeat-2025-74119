using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace GaragePortal.Razor.Controllers
{
    public class BaseEntity : Controller
    {
        protected bool IsAuthenticated()
        {
            return HttpContext.Session.GetString("UserSession") != null || HttpContext.Session.GetString("UserRole") != null;
        }

        protected bool IsAdmin()
        {
            return HttpContext.Session.GetString("UserRole") == "Admin";
        }

        protected bool IsMechanic()
        {
            return HttpContext.Session.GetString("UserRole") == "Mechanic";
        }

        protected bool IsCustomer()
        {
            return HttpContext.Session.GetString("UserRole") == "Customer";
        }

        protected int GetCurrentUserId()
        {
            string sessionId = HttpContext.Session.GetString("UserSession");
            int.TryParse(sessionId, out int userId);
            return userId;
        }

        protected string GetCurrentUserRole()
        {
            return HttpContext.Session.GetString("UserRole") ?? string.Empty;
        }

        protected void SetSessionData(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }

        protected string GetSessionData(string key)
        {
            return HttpContext.Session.GetString(key) ?? string.Empty;
        }

        protected void ClearSession()
        {
            HttpContext.Session.Clear();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["MySession"] = HttpContext.Session.GetString("UserSession");
            ViewData["UserRole"] = HttpContext.Session.GetString("UserRole");
            ViewData["IsAuthenticated"] = IsAuthenticated();
            base.OnActionExecuting(context);
        }

        protected IActionResult RedirectToLogin()
        {
            return RedirectToAction("Login", "Home");
        }

        protected IActionResult RedirectToUnauthorized()
        {
            return RedirectToAction("Index", "Home");
        }
    }
} 