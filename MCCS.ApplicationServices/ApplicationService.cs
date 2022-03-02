using MCCS.DataServices;
using MCCS.Interfaces.MCCSInterfaces;
using MCCS.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCCS.ApplicationServices
{
    public class ApplicationService
    {
        public List<Product> GetAllProducts()
        {
            using (var dataService = DataServiceBuilder.CreateDataService())
            {
                IProductDataService mccsDS = DataServiceBuilder.CreateProductDataService(dataService);
                List<Product> productList = mccsDS.GetAllProducts();
                return productList;
            }
        }

        public InvoiceDetails SavePurchaseDetails(InvoiceDetails invoice)
        {
            var dataService = DataServiceBuilder.CreateDataService();
            try
            {
                dataService.BeginTransaction();
                IProductDataService mccsDS = DataServiceBuilder.CreateProductDataService(dataService);
                InvoiceDetails invoiceDetails = mccsDS.SavePurchaseDetails(invoice);
                dataService.CommitTransaction();
                return invoiceDetails;
            }
            catch (Exception)
            {
                dataService.RollbackTransaction();
                throw;
            }
            
         
        }

        public List<InvoiceDetails> GetAllPurchaseDetails()
        {
            using (var dataService = DataServiceBuilder.CreateDataService())
            {
                IProductDataService mccsDS = DataServiceBuilder.CreateProductDataService(dataService);
                List<InvoiceDetails> GetAllPurchaseDetails = mccsDS.GetAllPurchaseDetails();
                return GetAllPurchaseDetails;
            }
        }
    }
}
