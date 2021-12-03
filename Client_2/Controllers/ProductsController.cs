using Client_2.Models;
using Client_2.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client_2.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IApiResourcesHttpClient _apiResourcesHttpClient;

        public ProductsController(IConfiguration configuration, IApiResourcesHttpClient apiResourcesHttpClient)
        {
            _configuration = configuration;
            _apiResourcesHttpClient = apiResourcesHttpClient;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products = new List<Product>();
            var client = await _apiResourcesHttpClient.GetHttpClient();

            var response = await client.GetAsync("https://localhost:5007/products/getproducts/");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                products = JsonConvert.DeserializeObject<List<Product>>(content);
                return View(products);
            }
                
            return View(products);
        }
    }
}