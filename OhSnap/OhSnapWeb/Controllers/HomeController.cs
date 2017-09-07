namespace OhSnapWeb.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var fullPictureList = OhSnapDAL.Managers.PhotoManager.GetFullPhotoListFromDB();

            var newestPictureList = fullPictureList.OrderByDescending(x => x.UploadDate).Take(9).ToList();

            ViewBag.Text = string.Empty;
            ViewBag.Warning = string.Empty;
            return View(newestPictureList);
        }

        public ActionResult FindSnaps()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FindSnaps(string photoName)
        {
            var fullPictureList = OhSnapDAL.Managers.PhotoManager.GetFullPhotoListFromDB();

            if (photoName != string.Empty)
            {
                var pictures = fullPictureList.Where(x => x.PhotoName.Contains(photoName)).ToList();

                return View(pictures);
            }

            return View(fullPictureList);
        }

        public ActionResult MySnaps()
        {
            var fullPictureList = OhSnapDAL.Managers.PhotoManager.GetFullPhotoListFromDB();

            var activeUser = OhSnapDAL.Managers.UserManager.GetUserFromDB(User.Identity.Name);

            var myPictureList = fullPictureList.Where(x => x.UserID == activeUser.UserID).OrderByDescending(y => y.UploadDate).ToList();

            return View(myPictureList);
        }

        public ActionResult ShowSnaps(int userID)
        {
            var fullPictureList = OhSnapDAL.Managers.PhotoManager.GetFullPhotoListFromDB();

            var userPictureList = fullPictureList.Where(x => x.UserID == userID).OrderByDescending(y => y.UploadDate).ToList();

            return View(userPictureList);
        }

        public ActionResult SnapUpload()
        {
            ViewBag.Warning = string.Empty;
            return View();
        }

        [HttpGet]
        public ActionResult ViewSnap(int photoID = default(int))
        {
            var photo = OhSnapDAL.Managers.PhotoManager.GetPhotoFromDB(photoID);

            ViewBag.Warning = string.Empty;
            return View(photo);
        }

        [HttpGet]
        public ActionResult DeleteSnap(int photoID = default(int))
        {
            var activeUser = OhSnapDAL.Managers.UserManager.GetUserFromDB(User.Identity.Name);

            var photo = OhSnapDAL.Managers.PhotoManager.GetPhotoFromDB(photoID);

            if (activeUser.UserID == photo.UserID || activeUser.RoleID == 1)
            {
                OhSnapDAL.Managers.PhotoManager.DeletePhotoFromDB(photoID);
            }
            else
            {
                ViewBag.Warning = "You can`t delete others snaps.";
                return View("ViewSnap", photo);
            }
            
            return RedirectToAction("MySnaps");
        }

        [HttpGet]
        public ActionResult LikeSnap(int photoID)
        {
            var activeUser = OhSnapDAL.Managers.UserManager.GetUserFromDB(User.Identity.Name);
            
            OhSnapDAL.Managers.PhotoManager.LikePhoto(photoID, activeUser.UserID);

            var photo = OhSnapDAL.Managers.PhotoManager.GetPhotoFromDB(photoID);

            ViewBag.Warning = string.Empty;
            return View("ViewSnap", photo);
        }

        [HttpPost]
        public ActionResult Upload()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var fileType = file.ContentType;

                        if (fileType != "image/png" && fileType != "image/gif" && fileType != "image/jpeg")
                        {
                            ViewBag.Warning = "You can only load images (png, jpeg, gif).";
                            return View("SnapUpload");
                        }

                        byte[] tempFile = new byte[file.ContentLength];
                        file.InputStream.Read(tempFile, 0, file.ContentLength);

                        var activeUser = OhSnapDAL.Managers.UserManager.GetUserFromDB(User.Identity.Name);

                        OhSnapDAL.Managers.PhotoManager.SavePhotoToDB(fileName, tempFile, fileType, activeUser.UserID);
                    }
                }
            }
            else
            {
                ViewBag.Warning = "You need to sign in to upload images.";
                return View("SnapUpload");
            }

            return RedirectToAction("MySnaps");
        }
    }
}