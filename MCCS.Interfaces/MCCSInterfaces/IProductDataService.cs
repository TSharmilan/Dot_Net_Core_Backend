using MCCS.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCCS.Interfaces.MCCSInterfaces
{
    public interface IProductDataService
    {
        List<Product> GetAllProducts();
        InvoiceDetails SavePurchaseDetails(InvoiceDetails invoice);
        List<InvoiceDetails> GetAllPurchaseDetails();
    }
}
