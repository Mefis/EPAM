namespace HMT_13.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using HMT_13.Models;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Text = string.Empty;
            ViewBag.Error = string.Empty;
            return this.View("Index", DataManager.GetOrderListFromDB());
        }

        [HttpGet]
        public ActionResult CreateOrder()
        {
            try
            {
                DataManager.CreateOrder();
            }
            catch
            {
                ViewBag.Text = string.Empty;
                ViewBag.Error = "Something went wrong!";//todo pn все сообщения в ресурсы
                return this.View("Index", DataManager.GetOrderListFromDB());
            }

            ViewBag.Text = "Order created!";
            ViewBag.Error = string.Empty;
            return this.View("Index", DataManager.GetOrderListFromDB());
        }

        [HttpPost]
        public ActionResult EditOrder(EditOrderModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DataManager.EditOrder(model);
                }
            }
            catch
            {
                ViewBag.Text = string.Empty;
                ViewBag.Error = "Something went wrong!";
                return this.View("Index", DataManager.GetOrderListFromDB());
            }

            ViewBag.Text = "Order edited!";
            ViewBag.Error = string.Empty;
            return this.View("Index", DataManager.GetOrderListFromDB());
        }

        [HttpGet]
        public ActionResult DeleteOrder(string orderID)
        {
            try
            {
                DataManager.DeleteOrder(orderID);
            }
            catch
            {
                ViewBag.Text = string.Empty;
                ViewBag.Error = "Something went wrong!";
                return this.View("Index", DataManager.GetOrderListFromDB());
            }

            ViewBag.Text = "Order deleted!";
            ViewBag.Error = string.Empty;
            return this.View("Index", DataManager.GetOrderListFromDB());
        }

        [HttpGet]
        public ActionResult OrderItems(string orderID)
        {
            ViewBag.OrderID = orderID;
            return this.View("OrderItems", DataManager.GetOrderListFromDB().Where(x => x.OrderID == orderID && x.ProductID != string.Empty).ToList());
        }

        [HttpPost]
        public ActionResult EditOrCreateOrderItem(EditOrCreateOrderItemModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DataManager.EditOrCreateOrderItem(model);
                }
            }
            catch
            {
                ViewBag.Text = string.Empty;
                ViewBag.Error = "Something went wrong!";
                return this.View("Index", DataManager.GetOrderListFromDB());
            }

            ViewBag.Text = "Order item edited or created!";
            ViewBag.Error = string.Empty;
            return this.View("Index", DataManager.GetOrderListFromDB());
        }

        [HttpGet]
        public ActionResult DeleteOrderItem(string orderID, string productID)
        {
            try
            {
                DataManager.DeleteOrderItem(orderID, productID);
            }
            catch
            {
                ViewBag.Text = string.Empty;
                ViewBag.Error = "Something went wrong!";
                return this.View("Index", DataManager.GetOrderListFromDB());
            }

            ViewBag.Text = "Order item deleted!";
            ViewBag.Error = string.Empty;
            return this.View("Index", DataManager.GetOrderListFromDB());
        }
    }
}