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
            if (Context.User == null || !Context.User.Identity.IsAuthenticated)
                return new ChallengeResult(OpenIdConnectAuthenticationDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "/" });
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/LogOff
        [HttpGet]
        public IActionResult LogOff()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                Context.Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationScheme);
                Context.Authentication.SignOut(OpenIdConnectAuthenticationDefaults.AuthenticationScheme);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}