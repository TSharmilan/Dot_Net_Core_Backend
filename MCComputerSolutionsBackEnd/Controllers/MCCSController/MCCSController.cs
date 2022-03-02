using MCCS.ApplicationServices;
using MCCS.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCCS.WEBAPI.Controllers.MCCSController
{
    [Route("api/[controller]")]
    [ApiController]
    public class MCCSController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MCCSController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public List<Product> GetAllProducts()
        {
            ApplicationService mccsAS = new ApplicationService();
            var result = mccsAS.GetAllProducts();
            return result;
        }

        [HttpPost]
        [Route("SavePurchaseDetails")]
        public InvoiceDetails SavePurchaseDetails([FromBody] InvoiceDetails invoiceDetails)
        {
            ApplicationService mccsAS = new ApplicationService();
            var result = mccsAS.SavePurchaseDetails(invoiceDetails);
            return result;
        }

        [HttpGet]
        [Route("GetAllPurchaseDetails")]
        public List<InvoiceDetails> GetAllPurchaseDetails()
        {
            ApplicationService mccsAS = new ApplicationService();
            var result = mccsAS.GetAllPurchaseDetails();
            return result;
        }
    }
}
