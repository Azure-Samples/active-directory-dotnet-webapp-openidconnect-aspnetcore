using System;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Authentication.OpenIdConnect;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Mvc;

namespace WebApp_OpenIdConnect_DotNet.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.User == null || !HttpContext.User.Identity.IsAuthenticated)
                return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "/" });
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/LogOff
        [HttpGet]
        public IActionResult LogOff()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                HttpContext.Authentication.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
                HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}