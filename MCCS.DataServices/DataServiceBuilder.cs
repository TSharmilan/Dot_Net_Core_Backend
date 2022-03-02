using MCCS.DataServices.MCCSDataService;
using MCCS.Interfaces;
using MCCS.Interfaces.MCCSInterfaces;
using MCCS.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCCS.DataServices
{
    public class DataServiceBuilder
    {
        public static IDataService CreateDataService() => new SqlDataService(ÀpplicationSettings.DBConnectionString);

        public static IProductDataService CreateProductDataService(IDataService dataService) => new ProductDataService(dataService);
    }
}
