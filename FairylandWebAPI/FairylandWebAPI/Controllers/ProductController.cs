using FairylandWebAPI.DataAccess;
using FairylandWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;

namespace FairylandWebAPI.Controllers
{
    public class ProductController : ApiController
    {
        ProductInfo obj = new ProductInfo();
        [HttpGet]
        public HttpResponseMessage GetAllProducts()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new ObjectContent<List<Product>>(obj.GetAllProducts(), new JsonMediaTypeFormatter());            
            return response;
        }
        [HttpPost]
        [Route("api/Product/GetProductById")]
        public HttpResponseMessage GetProductById(Product productData)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new ObjectContent<Product>(obj.GetProductById(productData.ProductId), new JsonMediaTypeFormatter());
            return response;
        }
        [HttpPost]
        [Route("api/Product/SaveProduct")]
        public HttpResponseMessage SaveProduct(Product productData)
        {
            string insertCheck = obj.SaveProduct(productData);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(insertCheck, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpPost]
        [Route("api/Product/DeleteProductById")]
        public HttpResponseMessage DeleteProductById(Product productData)
        {
            string insertCheck = obj.DeleteProductById(productData);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(insertCheck, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpPost]
        [Route("api/Product/UpdateProduct")]
        public HttpResponseMessage UpdateProduct(Product productData)
        {
            string insertCheck = obj.UpdateProduct(productData);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(insertCheck, Encoding.UTF8, "application/json");
            return response;
        }
    }
}
