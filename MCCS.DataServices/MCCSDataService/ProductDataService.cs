using MCCS.Interfaces;
using MCCS.Interfaces.MCCSInterfaces;
using MCCS.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace MCCS.DataServices.MCCSDataService
{
    public class ProductDataService : BaseRepository, IProductDataService
    {
        //private static DbParameter CreateParameter(string name, DbType dbType, object value = null, ParameterDirection direction = ParameterDirection.Input) => new SqlParameter { DbType = dbType, ParameterName = name, Value = value ?? DBNull.Value, Direction = direction };

        //private DbParameter CreateParameter(string name, DbType dbType, object value = null, ParameterDirection direction = ParameterDirection.Input, int size = 1)
        //{
        //    if (!name.StartsWith("@"))
        //        name = $"@{name}";
        //    var param = new SqlParameter { DbType = dbType, ParameterName = name, Value = value ?? DBNull.Value, Direction = direction };
        //    if (size > 1)
        //        param.Size = size;
        //    return param;
        //}

        //protected DbParameter IntParameter(string name, Int32? value = null, ParameterDirection direction = ParameterDirection.Input) => CreateParameter(name, DbType.Int32, value, direction);
        //protected DbParameter DecimalParameter(string name, Decimal? value = null, ParameterDirection direction = ParameterDirection.Input) => CreateParameter(name, DbType.Decimal, value, direction);
        //protected DbParameter StringParameter(string name, string value = null, ParameterDirection direction = ParameterDirection.Input, int size = 1) => CreateParameter(name, DbType.String, value, direction, size);



        //protected readonly IDataService _dataService;
        public ProductDataService(IDataService dataService) : base(dataService)
        {
            //_dataService = dataService;
        }

        public List<Product> GetAllProducts()
        {
            var productList = new List<Product>();  
            using (DbDataReader dbDataReader = _dataService.ExecuteReader("[dbo].[GetAllProducts]", null))
            {
                while (dbDataReader.Read())
                    productList.Add(FillProductDetails(dbDataReader));
                dbDataReader.Close();
            }
            return productList;
        }

        private Product FillProductDetails(DbDataReader dbDataReader) => new Product
        {
            ProductID = dbDataReader.GetInt32("ProdictID"),
            ProductName = dbDataReader.GetString("ProductName"),
            Price = dbDataReader.GetDecimal("Price"),
        };

        public InvoiceDetails SavePurchaseDetails(InvoiceDetails invoice)
        {
            DbParameter[] paramList = new DbParameter[7];
            foreach (var item in invoice.productList)
            {
                paramList[0] = StringParameter("@CustomerName", invoice.CustomerName);
                paramList[1] = StringParameter("@PhoneNumber", invoice.PhoneNumber);
                paramList[2] = StringParameter("@IDNumber", invoice.NIC);
                paramList[3] = StringParameter("@Address", invoice.Address);
                paramList[4] = IntParameter("@ProductID", item.ProductID);
                paramList[5] = IntParameter("@Quantity", item.Quantity);
                paramList[6] = DecimalParameter("@Price", item.Price);
                _dataService.ExecuteNonQuery("[dbo].[SavePurchaseDetails]", paramList);
            }
            
            return invoice;
        }

        public List<InvoiceDetails> GetAllPurchaseDetails()
        {
            var invoiceDetailList = new List<InvoiceDetails>();
            using (DbDataReader dbDataReader = _dataService.ExecuteReader("[dbo].[GetAllPurchaseDetails]", null))
            {
                while (dbDataReader.Read())
                    invoiceDetailList.Add(FillPurchaseDetails(dbDataReader));
                dbDataReader.Close();
            }
            return invoiceDetailList;
        }

        private InvoiceDetails FillPurchaseDetails(DbDataReader dbDataReader) => new InvoiceDetails
        {
            CustomerName = dbDataReader.GetString("CustomerName"),
            PhoneNumber = dbDataReader.GetString("PhoneNumber"),
            Address = dbDataReader.GetString("Address"),
            NIC = dbDataReader.GetString("IDNumber"),
            PurchaseDate = dbDataReader.GetString("CreatedDate"),
            Quantity = dbDataReader.GetInt32("Quantity"),
            Price = dbDataReader.GetString("Price"),
            ProductName = dbDataReader.GetString("ProductName"),
        };
    }
}
