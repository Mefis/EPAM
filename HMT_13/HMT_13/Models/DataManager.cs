namespace HMT_13.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Web.Configuration;

    public class DataManager
    {
        private const string ConnectionName = "NorthwindConnection";

        private static string connectionString;

        private static DbProviderFactory factory;

        public static void GetFactorySettingsFromConfig()
        {
            var connectionStringItem = WebConfigurationManager.ConnectionStrings[ConnectionName];
            connectionString = connectionStringItem.ConnectionString;
            var providerName = connectionStringItem.ProviderName;
            factory = DbProviderFactories.GetFactory(providerName);
        }

        public static List<Order> GetOrderListFromDB()
        {
            GetFactorySettingsFromConfig();

            var orderList = new List<Order>();

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = string.Format("select o.OrderID, c.CompanyName, o.OrderDate, o.ShippedDate, od.ProductID, od.UnitPrice, od.Quantity, od.Discount, p.ProductName, " + 
                    "round((od.UnitPrice-od.UnitPrice*od.Discount)*od.Quantity, 2) as Price from Orders as o left outer join Customers as c on c.CustomerID = o.CustomerID " + 
                    "left outer join [Order Details] as od on od.OrderID = o.OrderID left outer join Products as p on p.ProductID = od.ProductID");

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var orderID = (reader["OrderID"] ?? string.Empty).ToString();
                        var companyName = (reader["CompanyName"] ?? string.Empty).ToString();
                        var orderDateString = reader["OrderDate"].ToString();
                        var orderDate = orderDateString == 
                            string.Empty ? default(DateTime) : DateTime.ParseExact(orderDateString, "dd.MM.yyyy H:mm:ss", System.Globalization.CultureInfo.InvariantCulture);//todo pn упало здесь
						var shippedDateString = reader["ShippedDate"].ToString();
                        var shippedDate = shippedDateString == 
                            string.Empty ? default(DateTime) : DateTime.ParseExact(shippedDateString, "dd.MM.yyyy H:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        var productID = (reader["ProductID"] ?? string.Empty).ToString();
                        var unitPriceString = reader["UnitPrice"].ToString();
                        var unitPrice = unitPriceString == string.Empty ? default(float) : float.Parse(unitPriceString);
                        var quantity = (reader["Quantity"] ?? string.Empty).ToString();
                        var discount = (reader["Discount"] ?? string.Empty).ToString();
                        var productName = (reader["ProductName"] ?? string.Empty).ToString();
                        var priceString = reader["Price"].ToString();
                        var price = priceString == string.Empty ? default(float) : float.Parse(priceString);

                        var order = new Order(orderID, companyName, orderDate, shippedDate, productID, unitPrice, quantity, discount, productName, price);

                        orderList.Add(order);
                    }
                }

                connection.Close();
            }

            return orderList.OrderBy(x => x.OrderID).ToList();
        }

        public static void CreateOrder()
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "insert into Orders (OrderDate) values (null)";

                command.ExecuteScalar();
                connection.Close();
            }
        }

        public static void EditOrder(EditOrderModel model)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = 
                    model.CustomerID == null ? string.Empty : string.Format("update Orders set CustomerID = '{0}' where OrderID = {1};", model.CustomerID, model.OrderID);
                command.CommandText += 
                    model.EmployeeID == null ? string.Empty : string.Format("update Orders set EmployeeID = '{0}' where OrderID = {1};", model.EmployeeID, model.OrderID);
                command.CommandText += 
                    model.OrderDate == null ? string.Empty : string.Format("update Orders set OrderDate = convert(datetime, '{0}', 101) where OrderID = {1};", model.OrderDate, model.OrderID);
                command.CommandText += 
                    model.RequiredDate == null ? string.Empty : string.Format("update Orders set RequiredDate = convert(datetime, '{0}', 101) where OrderID = {1};", model.RequiredDate, model.OrderID);
                command.CommandText += 
                    model.ShippedDate == null ? string.Empty : string.Format("update Orders set ShippedDate = convert(datetime, '{0}', 101) where OrderID = {1};", model.ShippedDate, model.OrderID);
                command.CommandText += 
                    model.ShipVia == null ? string.Empty : string.Format("update Orders set ShipVia = '{0}' where OrderID = {1};", model.ShipVia, model.OrderID);
                command.CommandText += 
                    model.Freight == null ? string.Empty : string.Format("update Orders set Freight = '{0}' where OrderID = {1};", model.Freight, model.OrderID);
                command.CommandText += 
                    model.ShipName == null ? string.Empty : string.Format("update Orders set ShipName = '{0}' where OrderID = {1};", model.ShipName.Replace("'","''"), model.OrderID);
                command.CommandText += 
                    model.ShipAddress == null ? string.Empty : string.Format("update Orders set ShipAddress = '{0}' where OrderID = {1};", model.ShipAddress.Replace("'", "''"), model.OrderID);
                command.CommandText += 
                    model.ShipCity == null ? string.Empty : string.Format("update Orders set ShipCity = '{0}' where OrderID = {1};", model.ShipCity.Replace("'", "''"), model.OrderID);
                command.CommandText += 
                    model.ShipRegion == null ? string.Empty : string.Format("update Orders set ShipRegion = '{0}' where OrderID = {1};", model.ShipRegion, model.OrderID);
                command.CommandText += 
                    model.ShipPostalCode == null ? string.Empty : string.Format("update Orders set ShipPostalCode = '{0}' where OrderID = {1};", model.ShipPostalCode, model.OrderID);
                command.CommandText += 
                    model.ShipCountry == null ? string.Empty : string.Format("update Orders set ShipCountry = '{0}' where OrderID = {1};", model.ShipCountry, model.OrderID);

                command.ExecuteScalar();
                connection.Close();
            }
        }

        public static void DeleteOrder(string orderID)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = string.Format("delete from [Order Details] where OrderID = {0};", orderID);
                command.CommandText += string.Format("delete from Orders where OrderID = {0};", orderID);

                command.ExecuteScalar();
                connection.Close();
            }
        }

        public static void CreateOrderItem(string orderID)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = string.Format("insert into [Order Details] (OrderID) values ({0})", orderID);

                command.ExecuteScalar();
                connection.Close();
            }
        }

        public static void EditOrCreateOrderItem(EditOrCreateOrderItemModel model)
        {
            using (var connection = factory.CreateConnection())
            {
                var commandText = string.Empty;
                var orderList = GetOrderListFromDB();

                foreach (var item in orderList)
                {
                    if (item.OrderID == model.OrderID && item.ProductID == model.ProductID)
                    {
                        var textString = "update [Order Details] set UnitPrice = {0}, Quantity = {1}, Discount = {2} where OrderID = {3} and ProductID = {4}";
                        commandText = string.Format(textString, model.UnitPrice, model.Quantity, model.Discount, model.OrderID, model.ProductID);
                    }
                }

                if (commandText == string.Empty)
                {
                    var textString = "insert into [Order Details] (OrderID, ProductID, UnitPrice, Quantity, Discount) values ({0}, {1}, {2}, {3}, {4})";
                    commandText = string.Format(textString, model.OrderID, model.ProductID, model.UnitPrice, model.Quantity, model.Discount);
                }

                connection.ConnectionString = connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = commandText;

                command.ExecuteScalar();
                connection.Close();
            }
        }

        public static void DeleteOrderItem(string orderID, string productID)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = string.Format("delete from [Order Details] where OrderID = {0} and ProductID = {1};", orderID, productID);

                command.ExecuteScalar();
                connection.Close();
            }
        }

        public static void ChangeOrderStatus(string orderID, int statusNum)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = connection.CreateCommand();

                switch (statusNum)
                {
                    case 0:
                        command.CommandText = string.Format("update Orders set OrderDate = NULL, ShippedDate = NULL where OrderID = '{0}'", orderID);
                        break;
                    case 1:
                        command.CommandText = string.Format("update Orders set OrderDate = getdate() where OrderID = '{0}'", orderID);
                        break;
                    case 2:
                        command.CommandText = string.Format("update Orders set ShippedDate = getdate() where OrderID = '{0}'", orderID);
                        break;
                    default:
                        break;
                }

                command.ExecuteScalar();
                connection.Close();
            }
        }
    }
}