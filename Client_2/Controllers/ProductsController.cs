using Client_2.Models;
<<<<<<< HEAD
using Client_2.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
=======
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
>>>>>>> e182333
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client_2.Controllers
{
<<<<<<< HEAD
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IApiResourceHttpClient _apiResourceHttpClient;

        public ProductsController(IConfiguration configuration, IApiResourceHttpClient apiResourceHttpClient)
        {
            _configuration = configuration;
            _apiResourceHttpClient = apiResourceHttpClient;
=======
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
>>>>>>> e182333
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products = new List<Product>();
<<<<<<< HEAD
            using (HttpClient client = await _apiResourceHttpClient.GetHttpClient())
            {
                var response = await client.GetAsync("https://localhost:5007/products/getproducts/");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    products = JsonConvert.DeserializeObject<List<Product>>(content);
                    return View(products);
                }   
            }
            return View(products);
        }
=======
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
        
>>>>>>> e182333
    }
}
