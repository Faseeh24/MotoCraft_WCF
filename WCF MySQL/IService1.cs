using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCF_MySQL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string AddCustomer(User user);

        [OperationContract]
        string AddManufacturer(User user);

        [OperationContract]
        bool CustomerLogin(string id, string password);

        [OperationContract]
        bool AdminLogin(string id, string password);

        [OperationContract]
        bool ManufacturerLogin(string id, string password);

        [OperationContract]
        List<User> GetManufacturers();

        [OperationContract]
        bool RemoveManufacturer(string id);

        [OperationContract]
        List<Feedback> GetFeedbacks();

        [OperationContract]
        string AddRawMaterial(RawMaterial material);

        [OperationContract]
        bool UpdateRawMaterial(RawMaterial material);

        [OperationContract]
        bool RemoveRawMaterial(string id);

        [OperationContract]
        List<RawMaterial> GetRawMaterials();

        [OperationContract]
        string AddProduct(Product material);

        [OperationContract]
        bool UpdateProduct(Product material);

        [OperationContract]
        bool RemoveProduct(string id);

        [OperationContract]
        List<Product> GetProducts();

        [OperationContract]
        Product GetAProduct(string id);

        [OperationContract]
        string PlaceOrder(Order order);

        [OperationContract]
        List<Order> GetOrders();

        [OperationContract]
        bool UpdateOrderStatus(string order_id, string new_status);

        [OperationContract]
        List<Order> GetUnfulfilledOrders();

        [OperationContract]
        List<Order> GetOrdersbyCustomer(string id);

        [OperationContract]
        bool GiveFeedback(Feedback feedback);

        [OperationContract]
        List<Order> GetOrdersbyMonth(string month);

        [OperationContract]
        List<string> GetOrdersMonths();

        [OperationContract]
        Dictionary<string, int> GetOrderStatusData(string month);

        [OperationContract]
        Dictionary<string, int> GetOrderProductsData(string month);


        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
