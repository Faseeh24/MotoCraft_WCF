using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCF_MySQL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string AddCustomer(User user)
        {
            return DataBase.AddCustomer(user);
        }

        public string AddManufacturer(User user)
        {
            return DataBase.AddManufacturer(user);
        }

        public bool CustomerLogin(string id, string password)
        {
            return DataBase.CustomerLogin(id, password);
        }

        public bool AdminLogin(string id, string password)
        {
            return DataBase.AdminLogin(id, password);
        }

        public bool ManufacturerLogin(string id, string password)
        {
            return DataBase.ManufacturerLogin(id, password);
        }

        public List<User> GetManufacturers()
        {
            return DataBase.GetManufacturers();
        }

        public bool RemoveManufacturer(string id)
        {
            return DataBase.RemoveManufacturer(id);
        }

        public List<Feedback> GetFeedbacks()
        {
            return DataBase.GetFeedbacks();
        }

        public string AddRawMaterial(RawMaterial material)
        {
            return DataBase.AddRawMaterial(material);
        }

        public bool UpdateRawMaterial(RawMaterial material)
        {
            return DataBase.UpdateRawMaterial(material);
        }

        public bool RemoveRawMaterial(string id)
        {
            return DataBase.RemoveRawMaterial(id);
        }

        public List<RawMaterial> GetRawMaterials()
        {
            return DataBase.GetRawMaterials();
        }

        public string AddProduct(Product material)
        {
            return DataBase.AddProduct(material);
        }

        public bool UpdateProduct(Product material)
        {
            return DataBase.UpdateProduct(material);
        }

        public bool RemoveProduct(string id)
        {
            return DataBase.RemoveProduct(id);
        }

        public List<Product> GetProducts()
        {
            return DataBase.GetProducts();
        }

        public Product GetAProduct(string id)
        {
            return DataBase.GetAProduct(id);
        }

        public string PlaceOrder(Order order)
        {
            return DataBase.PlaceOrder(order);
        }

        public List<Order> GetOrders()
        {
            return DataBase.GetOrders();
        }

        public bool UpdateOrderStatus(string id, string status)
        {
            return DataBase.UpdateOrderStatus(id, status);
        }

        public List<Order> GetOrdersbyCustomer(string id)
        {
            return DataBase.GetOrdersbyCustomer(id);
        }

        public List<Order> GetUnfulfilledOrders()
        {
            return DataBase.GetUnfulfilledOrders();
        }

        public bool GiveFeedback(Feedback feedback)
        {
            return DataBase.GiveFeedback(feedback);
        }

        public List<Order> GetOrdersbyMonth(string month)
        {
            return DataBase.GetOrdersbyMonth(month);
        }

        public List<string> GetOrdersMonths()
        {
            return DataBase.GetOrdersMonths();
        }

        public Dictionary<string, int> GetOrderStatusData(string month)
        {
            return DataBase.GetOrderStatusData(month);
        }

        public Dictionary<string, int> GetOrderProductsData(string month)
        {
            return DataBase.GetOrderProductsData(month);
        }


        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
