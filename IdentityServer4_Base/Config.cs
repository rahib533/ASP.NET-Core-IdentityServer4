using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer4_Base
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("resource_api1"){Scopes = { "api1.read", "api1.write", "api1.update" }, ApiSecrets = new []{ new Secret("secretapi1".Sha256()) } },
                new ApiResource("resource_api2"){Scopes = { "api2.read", "api2.write", "api2.update" }, ApiSecrets = new []{ new Secret("secretapi2".Sha256()) } }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("api1.read","API 1 uchun oxuma icazesi"),
                new ApiScope("api1.write", "API 1 uchun yazmaq icazesi"),
                new ApiScope("api1.update", "API 1 uchun deyishmek icazesi"),
                new ApiScope("api2.read","API 2 uchun oxuma icazesi"),
                new ApiScope("api2.write", "API 2 uchun yazmaq icazesi"),
                new ApiScope("api2.update", "API 2 uchun deyishmek icazesi")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client()
                {
                    ClientId = "client1",
                    ClientName = "Client 1 app tetbiqi",
                    ClientSecrets = new[]{new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"api1.write","api1.update","api1.read"}
                },
                new Client()
                {
                    ClientId = "client2",
                    ClientName = "Client 2 app tetbiqi",
                    ClientSecrets = new[]{new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"api2.read", "api1.read" }
                },
                new Client()
                {
                    ClientId = "client1-MVC",
                    RequirePkce = false,
                    ClientName = "Client 1 MVC app tetbiqi",
                    ClientSecrets = new[]{new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = new List<string>{ "https://localhost:5003/signin-oidc" },
                    PostLogoutRedirectUris = new List<string>{ "https://localhost:5003/signout-callback-oidc" },
                    AllowedScopes = {IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "api1.read", IdentityServerConstants.StandardScopes.OfflineAccess },
                    AccessTokenLifetime = 60,
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
<<<<<<< HEAD
                    AbsoluteRefreshTokenLifetime = 3000
<<<<<<< HEAD
<<<<<<< HEAD
=======
                    AbsoluteRefreshTokenLifetime = 3000,
                    RequireConsent = true
>>>>>>> e182333
                },
                new Client()
                {
                    ClientId = "client2-MVC",
                    RequirePkce = false,
                    ClientName = "Client 2 MVC app tetbiqi",
                    ClientSecrets = new[]{new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = new List<string>{ "https://localhost:5005/signin-oidc" },
                    PostLogoutRedirectUris = new List<string>{ "https://localhost:5005/signout-callback-oidc" },
<<<<<<< HEAD
                    AllowedScopes = {IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "api1.read", IdentityServerConstants.StandardScopes.OfflineAccess },
=======
                    AllowedScopes = {IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "api1.read", IdentityServerConstants.StandardScopes.OfflineAccess, "CountryAndCity", "Roles"},
>>>>>>> e182333
                    AccessTokenLifetime = 60,
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
<<<<<<< HEAD
                    AbsoluteRefreshTokenLifetime = 3000
=======
>>>>>>> parent of c741ab1 (role base auth)
=======
>>>>>>> parent of c741ab1 (role base auth)
=======
                    AbsoluteRefreshTokenLifetime = 3000,
                    RequireConsent = true
>>>>>>> e182333
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser{SubjectId = "1", Username = "rahibjafar", Password = "password", Claims = new List<Claim>(){
                    new Claim("given_name","Rahib"),
                    new Claim("family_name","Jafarov")
                } },
                new TestUser{SubjectId = "2", Username = "testuser", Password = "password", Claims = new List<Claim>(){
                    new Claim("given_name","Test"),
                    new Claim("family_name","User")
                } }
            };
        }
    }
}
