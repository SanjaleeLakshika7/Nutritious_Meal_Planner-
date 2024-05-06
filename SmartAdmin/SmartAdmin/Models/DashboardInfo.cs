using Catalog.Models;
using Sales.Models;
using System.Collections.Generic;

namespace SmartAdmin.Models
{
    public class DashboardInfo
    {

        public List<Customer> customer = new List<Customer>();
        public List<SaleOrder> saleOrders = new List<SaleOrder>();
        public int ItemCount { get; set; } = 0;
        public int Jan { get; set; } = 0;
        public int Feb { get; set; } = 0;
        public int Mar { get; set; } = 0;
        public int Apr { get; set; } = 0; 
        public int May { get; set; } = 0;
        public int Jun { get; set; } = 0;
        public int Jul { get; set; } = 0;
        public int Aug { get; set; } = 0;
        public int Sep { get; set; } = 0;
        public int Oct { get; set; } = 0;
        public int Nov { get; set; } = 0;
        public int Dec { get; set; } = 0;
    }
}
