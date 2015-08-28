using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using OWINAuthentication.Models;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace OWINAuthentication.Controllers
{
    public class AccountController : Controller
    {

        private IAuthenticationManager AuthenticationManager { get { return HttpContext.GetOwinContext().Authentication; } }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var identity = new ClaimsIdentity(new[] { 
                    new Claim(ClaimTypes.Name, model.Username)
                }, DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.Name, ClaimTypes.Role);

                identity.AddClaim(new Claim(ClaimTypes.Role, "guest"));

                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult Logoff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Login");
        }

    }
}