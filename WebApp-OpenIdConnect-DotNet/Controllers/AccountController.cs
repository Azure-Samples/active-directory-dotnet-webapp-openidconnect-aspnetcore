using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_OpenIdConnect_DotNet.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        [HttpGet]
        public void Login()
        {
            if (HttpContext.User == null || !HttpContext.User.Identity.IsAuthenticated)
                HttpContext.Authentication.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "/" });
        }

        // GET: /Account/LogOff
        [HttpGet]
        public void LogOff()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                HttpContext.Authentication.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
                HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}