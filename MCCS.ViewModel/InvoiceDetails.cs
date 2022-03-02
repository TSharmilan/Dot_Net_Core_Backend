using System;
using System.Collections.Generic;
using System.Text;

namespace MCCS.ViewModel
{
    public class InvoiceDetails
    {
         public string CustomerName { get; set; } 
        public string PhoneNumber { get; set; }
        public string NIC { get; set; }
        public string Address { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
        public List<Product> productList { get; set; }
        public string PurchaseDate { get; set; }
        public string ProductName { get; set; }
    }
}
