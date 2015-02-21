# WebApp-OpenIdConnect-AspNet5
This sample shows how to build a .Net MVC web application that uses OpenID Connect to sign-in users from a single Azure Active Directory tenant, using the ASP.Net 5 OpenID Connect OWIN middleware.

For more information about how the protocols work in this scenario and other scenarios, see [Authentication Scenarios for Azure AD](http://go.microsoft.com/fwlink/?LinkId=394414).

## How To Run This Sample

Getting started is simple!  To run this sample you will need:
- Visual Studio 2015 CTP6
- An Internet connection
- An Azure subscription (a free trial is sufficient)

Every Azure subscription has an associated Azure Active Directory tenant.  If you don't already have an Azure subscription, you can get a free subscription by signing up at [http://wwww.windowsazure.com](http://www.windowsazure.com).  All of the Azure AD features used by this sample are available free of charge.

### Step 1:  Clone or download this repository

From your shell or command line:

`git clone https://github.com/AzureADSamples/WebApp-OpenIDConnect-AspNet5.git`

### Step 2:  Create a user account in your Azure Active Directory tenant

If you already have a user account in your Azure Active Directory tenant, you can skip to the next step.  This sample will not work with a Microsoft account, so if you signed in to the Azure portal with a Microsoft account and have never created a user account in your directory before, you need to do that now.  If you create an account and want to use it to sign-in to the Azure portal, don't forget to add the user account as a co-administrator of your Azure subscription.

### Step 3:  Register the sample with your Azure Active Directory tenant

1. Sign in to the [Azure management portal](https://manage.windowsazure.com).
2. Click on Active Directory in the left hand nav.
3. Click the directory tenant where you wish to register the sample application.
4. Click the Applications tab.
5. In the drawer, click Add.
6. Click "Add an application my organization is developing".
7. Enter a friendly name for the application, for example "WebApp-OpenIDConnect-DotNet", select "Web Application and/or Web API", and click next.
8. For the sign-on URL, enter the base URL for the sample, which is by default `https://localhost:44322/`.
9. For the App ID URI, enter `https://<your_tenant_name>/WebApp-OpenIDConnect-DotNet`, replacing `<your_tenant_name>` with the name of your Azure AD tenant.

All done!  Before moving on to the next step, you need to find the Client ID of your application.

1. While still in the Azure portal, click the Configure tab of your application.
2. Find the Client ID value and copy it to the clipboard.

### Step 4:  Configure the sample to use your Azure Active Directory tenant

1. Open the solution in Visual Studio 2015.
2. Open the `config.json` file.
3. Find the `Tenant` property and replace the value with your AAD tenant name.
4. Find the `ClientId` and replace the value with the Client ID from the Azure portal.
5. If you changed the base URL of the sample, find the app key `ida:PostLogoutRedirectUri` and replace the value with the new base URL of the sample.

### Step 5:  Run the sample

Clean the solution, rebuild the solution, and run it.

Click the sign-in link on the homepage of the application to sign-in.  On the Azure AD sign-in page, enter the name and password of a user account that is in your Azure AD tenant.

## How To Deploy This Sample to Azure

Coming soon.

## About The Code

This sample shows how to use the OpenID Connect ASP.Net OWIN middleware to sign-in users from a single Azure AD tenant.  The middleware is initialized in the `Startup.cs` file, by passing it the Client ID of the application and the URL of the Azure AD tenant where the application is registered.  The middleware then takes care of:
- Downloading the Azure AD metadata, finding the signing keys, and finding the issuer name for the tenant.
- Processing OpenID Connect sign-in responses by validating the signature and issuer in an incoming JWT, extracting the user's claims, and putting them on ClaimsPrincipal.Current.
- Integrating with the session cookie ASP.Net OWIN middleware to establish a session for the user. 

You can trigger the middleware to send an OpenID Connect sign-in request by decorating a class or method with the `[Authorize]` attribute, or by issuing a challenge,
```C#
Context.Response.Challenge(
	new AuthenticationProperties { RedirectUri = "/" },
	OpenIdConnectAuthenticationDefaults.AuthenticationType);
```
Similarly you can send a signout request,
```C#
Context.Response.SignOut(new List<string>() 
{
	OpenIdConnectAuthenticationDefaults.AuthenticationType,
	CookieAuthenticationDefaults.AuthenticationType
});
```
When a user is signed out, they will be redirected to the `Post_Logout_Redirect_Uri` specified when the OpenID Connect middleware is initialized.

All of the OWIN middleware in this project is created as a part of the open source [Asp.Net Security](https://github.com/aspnet/Security) project.  You can read more about OWIN [here](http://owin.org).

## How To Recreate This Sample

1. In Visual Studio 2015 CTP6, create a new "ASP.NET 5 Preview Starter Web" application.
2. Set SSL Enabled to be True.  Note the SSL URL. BUGBUG: Um, how?
4. Add the `Microsoft.AspNet.Security.OpenIdConnect` ASP.Net OWIN middleware NuGet to the project.
5. Remove a few excess files that come with the template - they are not needed for this sample.  Delete the `Migrations` folder, the `Views/Account` folder, the `Models` folder, and the `Compiler` folder.
6. Replace the implementation of the `Controllers\AccountController.cs` class with the one from the project.
6. In `Views\Shared`, replace the implementation of `_LoginPartial.cshtml` with the one from the sample.
7. Replace the contents of `config.json` with the one from the sample.
6. Replace the contents of `Startup.cs` with the one from the sample.
12. If you want the user to be required to sign-in before they can see any page of the app, then in the `HomeController`, decorate the `HomeController` class with the `[Authorize]` attribute.  If you leave this out, the user will be able to see the home page of the app without having to sign-in first, and can click the sign-in link on that page to get signed in.
13. Almost done!  Follow the steps in "How To Run This Sample" above to register the application in your AAD tenant.
