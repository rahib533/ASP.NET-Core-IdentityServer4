using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client_1.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
            //await HttpContext.SignOutAsync("oidc");
        }

        public async Task<IActionResult> GetRefreshToken()
        {
            HttpClient httpClient = new HttpClient();
            var discovery = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");

            var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            RefreshTokenRequest refreshTokenRequest = new RefreshTokenRequest();
            refreshTokenRequest.ClientId = _configuration["Client-MVC:ClientId"];
            refreshTokenRequest.ClientSecret = _configuration["Client-MVC:ClientSecret"];
            refreshTokenRequest.RefreshToken = refreshToken;
            refreshTokenRequest.Address = discovery.TokenEndpoint;

            var token = await httpClient.RequestRefreshTokenAsync(refreshTokenRequest);

            var tokens = new List<AuthenticationToken>()
            {
                new AuthenticationToken{Name = OpenIdConnectParameterNames.IdToken, Value = token.IdentityToken},
                new AuthenticationToken{Name = OpenIdConnectParameterNames.AccessToken, Value = token.AccessToken},
                new AuthenticationToken{Name = OpenIdConnectParameterNames.RefreshToken, Value = token.RefreshToken},
                new AuthenticationToken{Name = OpenIdConnectParameterNames.ExpiresIn, Value = DateTime.UtcNow.AddSeconds(token.ExpiresIn).ToString("o",CultureInfo.InvariantCulture)}
            };

            var authenticationResult = await HttpContext.AuthenticateAsync();
            var properties = authenticationResult.Properties;

            properties.StoreTokens(tokens);

            await HttpContext.SignInAsync("Cookies",authenticationResult.Principal,properties);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public IActionResult AdminPage()
        {
            return View();
        }
        [Authorize(Roles = "user")]
        public IActionResult UserPage()
        {
            return View();
        }
    }
}
