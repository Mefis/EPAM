namespace OhSnapWeb.Controllers
{
    using OhSnapWeb.Models;
    using System.Web.Mvc;
    using System.Web.Security;

    public class AccountController : Controller
    {
        public ActionResult SignIn()
        {
            ViewBag.Warning = string.Empty;
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(ActiveUserModel activeUser, string ReturnUrl)
        {
            if (!string.IsNullOrEmpty(activeUser.Login) && !string.IsNullOrEmpty(activeUser.Password))
            {
                if (OhSnapDAL.Managers.AccountManager.IsUserValid(activeUser.Login, activeUser.Password))
                {
                    FormsAuthentication.SetAuthCookie(activeUser.Login, false);

                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Warning = "Incorrect Login or Password.";
                return View("SignIn");
            }

            return View(activeUser);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}