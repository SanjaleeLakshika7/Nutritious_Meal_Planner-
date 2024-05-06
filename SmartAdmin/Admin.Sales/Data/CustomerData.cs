using System.Collections.Generic;
using System.Threading.Tasks;
using DBAccess;
using Sales.Models;

namespace Sales.Data
{
    public class CustomerData : ICustomerData
    {
        #region Construction

        private readonly IDBAccess db;

        public CustomerData(IDBAccess db)
        {
            this.db = db;
        }

        #endregion


        public Task<string> AddEdit(string APIKey, Customer obj, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "CustomerID",obj.CustomerID },
                { "FacebookID",obj.FacebookID },
                { "GoogleID",obj.GoogleID },
                { "CustomerCategoryID",obj.CustomerCategoryID },
                { "CustomerGroupID",obj.CustomerGroupID },
                { "PriceType",obj.PriceType },
                { "LoginEmail",obj.LoginEmail },
                { "LoginPassword",obj.LoginPassword },

                { "Salutation",obj.Salutation },
                { "FirstName",obj.FirstName },
                { "MiddleName",obj.MiddleName },
                { "LastName",obj.LastName },
                { "CompanyName",obj.CompanyName },
                { "AddressLine1",obj.AddressLine1 },
                { "AddressLine2",obj.AddressLine2 },
                { "City",obj.City },
                { "State",obj.State },
                { "PostalCode",obj.PostalCode },
                { "Country",obj.Country },

                { "TelephoneMobile",obj.TelephoneMobile },
                { "TelephoneOther",obj.TelephoneOther },
                { "ShipAway",obj.ShipAway },

                { "ShipAttTo",obj.ShipAttTo },
                { "ShipAddressLine1",obj.ShipAddressLine1 },
                { "ShipAddressLine2",obj.ShipAddressLine2 },
                { "ShipCity",obj.ShipCity },
                { "ShipState",obj.ShipState },
                { "ShipPostalCode",obj.ShipPostalCode },
                { "ShipCountry",obj.ShipCountry },
                { "Remarks",obj.Remarks },
                { "ActiveStatus",obj.ActiveStatus },

                { "LogUserID",LogUserID }
            };

            string query = "sal.Customer_AddEdit";
            return db.Execute(query, param);
        }

        public Task<string> EditPassword(string APIKey, string CustomerID, string LoginPassword, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "CustomerID",CustomerID },
                { "LoginPassword",LoginPassword },
                { "LogUserID",LogUserID }
            };

            string query = "sal.Customer_PasswordEdit";
            return db.Execute(query, param);
        }

        public Task<int> GetCount(string APIKey, string KeyW = "", string CustomerCategoryID = "", string CustomerGroupID = "", string PriceType = "", string ActiveStatus = "")
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "CustomerCategoryID",CustomerCategoryID },
                { "CustomerGroupID",CustomerGroupID },
                { "PriceType",PriceType },
                { "ActiveStatus",ActiveStatus }
            };

            string query = "sal.Customer_Count";
            return db.GetCount(query, param);
        }

        public Task<string> Delete(string APIKey, string ID, string LogUserID)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID },
                { "LogUserID",LogUserID }
            };

            string query = "sal.Customer_Delete";
            return db.Execute(query, param);
        }

        public Task<Customer> Get(string APIKey, string ID = "", string LoginEmail = "", string FacebookID = "", string GoogleID = "")
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "ID",ID },
                { "LoginEmail",LoginEmail },
                { "FacebookID",FacebookID },
                { "GoogleID",GoogleID }
            };

            string query = "sal.Customer_Get";
            return db.Get<Customer, dynamic>(query, param);
        }

        public Task<List<Customer>> GetList(string APIKey, string KeyW = "", string CustomerCategoryID = "", string CustomerGroupID = "", string PriceType = "", string ActiveStatus = "", int Page = 0, int PageSize = 99999)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "KeyW",KeyW },
                { "CustomerCategoryID",CustomerCategoryID },
                { "CustomerGroupID",CustomerGroupID },
                { "PriceType",PriceType },
                { "ActiveStatus",ActiveStatus },
                { "Page",Page },
                { "PageSize",PageSize }
            };

            string query = "sal.Customer_List";
            return db.GetList<Customer, dynamic>(query, param);
        }

        public Task<Customer> GetByEmail(string APIKey, string EmailAddress)
        {
            var param = new Dictionary<string, object>
            {
                { "APIKey",APIKey },
                { "EmailAddress",EmailAddress }
            };

            string query = "sal.Customer_GetByEmail";
            return db.Get<Customer, dynamic>(query, param);
        }

    }
}
