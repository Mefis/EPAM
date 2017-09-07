namespace OhSnapWeb.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Security;

    public class UserController : Controller
    {
        public ActionResult CreateUser()
        {
            ViewBag.Text = string.Empty;
            ViewBag.Warning = string.Empty;
            return View();
        }

        public ActionResult ShowSnapUsers()
        {
            var users = OhSnapDAL.Managers.UserManager.GetFullUserListFromDB();

            return View(users);
        }

        [HttpPost]
        public ActionResult CreateUser(OhSnapDAL.Models.User user)
        {
            if (ModelState.IsValid)
            {
                OhSnapDAL.Managers.UserManager.CreateUser(user);
                ViewBag.Text = "User created!";
                ViewBag.Warning = string.Empty;
                return View();
            }
            ViewBag.Warning = "Couldn`t create a user!";
            ViewBag.Text = string.Empty;
            return View();
        }

        public ActionResult EditUser(int userID = default(int))
        {
            ViewBag.Text = string.Empty;
            ViewBag.Warning = string.Empty;

            var activeUser = OhSnapDAL.Managers.UserManager.GetUserFromDB(User.Identity.Name);

            if (activeUser.UserID == userID | activeUser.RoleID == 1)
            {
                var user = OhSnapDAL.Managers.UserManager.GetUserFromDB(userID);

                return View(user);
            }

            ViewBag.Warning = "You can`t edit this user.";
            return View();
        }

        [HttpPost]
        public ActionResult EditUser(OhSnapDAL.Models.User user)
        {
            ViewBag.Warning = string.Empty;
            ViewBag.Text = string.Empty;

            if (ModelState.IsValid)
            {
                var activeUser = OhSnapDAL.Managers.UserManager.GetUserFromDB(User.Identity.Name);

                if (activeUser.RoleID == 2 && user.RoleID == 1)
                {
                    ViewBag.Warning = "Changes discarded";
                    return View();
                }

                user.RoleID = user.RoleID == 0 ? 2 : user.RoleID;
                OhSnapDAL.Managers.UserManager.EditUser(user);
                ViewBag.Text = "Changes saved!";
                return View();
            }

            ViewBag.Warning = "Changes discarded";
            return View();
        }

        public ActionResult UserInfo(int userID)
        {
            var user = OhSnapDAL.Managers.UserManager.GetUserFromDB(userID);

            return View(user);
        }

        public ActionResult DeleteUser(int userID = default(int))
        {
            ViewBag.Text = string.Empty;
            ViewBag.Warning = string.Empty;

            var activeUser = OhSnapDAL.Managers.UserManager.GetUserFromDB(User.Identity.Name);

            var fullPictureList = OhSnapDAL.Managers.PhotoManager.GetFullPhotoListFromDB();

            var newestPictureList = fullPictureList.OrderByDescending(x => x.UploadDate).Take(9).ToList();

            if (activeUser.UserID == userID)
            {
                OhSnapDAL.Managers.UserManager.DeleteUserFromDB(userID);
                FormsAuthentication.SignOut();
                ViewBag.Text = "User deleted!";
                return View("~/Views/Home/Index.cshtml", newestPictureList);
            }

            if (activeUser.RoleID == 1)
            {
                OhSnapDAL.Managers.UserManager.DeleteUserFromDB(userID);
                ViewBag.Text = "User deleted!";

                return View("~/Views/Home/Index.cshtml", newestPictureList);
            }

            ViewBag.Warning = "You can`t delete this user.";
            return View("~/Views/Home/Index.cshtml", newestPictureList);
        }
    }
}