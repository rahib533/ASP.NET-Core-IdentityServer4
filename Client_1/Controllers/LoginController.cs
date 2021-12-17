using Client_1.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Client_1.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            var client = new HttpClient();
            var discovery = await client.GetDiscoveryDocumentAsync(_configuration["AuthServerUrl"]);
            var password = new PasswordTokenRequest();
            password.Address = discovery.TokenEndpoint;
            password.UserName = loginViewModel.Email;
            password.Password = loginViewModel.Password;
            password.ClientId = _configuration["ClientResourceOwner:ClientId"];
            password.ClientSecret = _configuration["ClientResourceOwner:ClientSecret"];

            var token = await client.RequestPasswordTokenAsync(password);


            var userInforequest = new UserInfoRequest();

            userInforequest.Token = token.AccessToken;
            userInforequest.Address = discovery.UserInfoEndpoint;
            var userInfo = await client.GetUserInfoAsync(userInforequest);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(userInfo.Claims, "Cookies", "name", "role");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProps = new AuthenticationProperties();
            authProps.StoreTokens(new List<AuthenticationToken>()
            {
                new AuthenticationToken{Name = OpenIdConnectParameterNames.AccessToken, Value = token.AccessToken},
                new AuthenticationToken{Name = OpenIdConnectParameterNames.RefreshToken, Value = token.RefreshToken},
                new AuthenticationToken{Name = OpenIdConnectParameterNames.ExpiresIn, Value = DateTime.UtcNow.AddSeconds(token.ExpiresIn).ToString("o",CultureInfo.InvariantCulture)}
            });
            await HttpContext.SignInAsync("Cookies", claimsPrincipal, authProps);

            return RedirectToAction("Index","User");
        }
    }
}
