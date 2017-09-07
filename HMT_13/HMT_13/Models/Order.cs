namespace HMT_13.Models
{
    using System;

    public class Order//todo pn валидации нет вообще
	{
        public Order(string orderID, string companyName, DateTime orderDate, DateTime shippedDate, string productID, float unitPrice, string quantity, string discount, string productName, float price)
        {
            this.OrderID = orderID;
            this.CompanyName = companyName;
            this.OrderDate = orderDate;
            this.ShippedDate = shippedDate;
            this.ProductID = productID;
            this.UnitPrice = unitPrice;
            this.Quantity = quantity;
            this.Discount = discount;
            this.ProductName = productName;
            this.Price = price;
            if (orderDate == default(DateTime))
            {
                this.Status = OrderStatus.NewOrder;
            }
            else if (shippedDate == default(DateTime))
            {
                this.Status = OrderStatus.InWork;
            }
            else
            {
                this.Status = OrderStatus.Done;
            }
        }

        public enum OrderStatus
        {
            NewOrder,
            InWork,
            Done
        }

        public string OrderID { get; }

        public string CompanyName { get; }

        public DateTime OrderDate { get; }

        public DateTime ShippedDate { get; }

        public string ProductID { get; }

        public float UnitPrice { get; }

        public string Quantity { get; }

        public string Discount { get; }

        public string ProductName { get; }

        public float Price { get; }

        public OrderStatus Status { get; }
    }
}