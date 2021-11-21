using Client_1.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client_1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products = new List<Product>();
            using (HttpClient client = new HttpClient())
            
            {
                var discovery = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
                if (!discovery.IsError)
                {
                    ClientCredentialsTokenRequest clientCredentialsTokenRequest = new ClientCredentialsTokenRequest();
                    clientCredentialsTokenRequest.ClientId = _configuration["Client:ClientId"];
                    clientCredentialsTokenRequest.ClientSecret = _configuration["Client:ClientSecret"];
                    clientCredentialsTokenRequest.Address = discovery.TokenEndpoint;
                    var token = await client.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

                    if (!token.IsError)
                    {
                        client.SetBearerToken(token.AccessToken);

                        var response = await client.GetAsync("https://localhost:5007/products/getproducts/");
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();

                            products = JsonConvert.DeserializeObject<List<Product>>(content);
                            return View(products);
                        }
                    }
                }
            }
            return View(products);
        }
    }
}
