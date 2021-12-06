using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Base.Seed
{
    public static class IdentityServerSeedData
    {
        public static void Seed(ConfigurationDbContext configurationDbContext)
        {
            if (!configurationDbContext.Clients.Any())
            {
                foreach (var client in Config.GetClients())
                {
                    configurationDbContext.Clients.Add(client.ToEntity());
                }
            }
            if (!configurationDbContext.ApiResources.Any())
            {
                foreach (var apiResource in Config.GetApiResources())
                {
                    configurationDbContext.ApiResources.Add(apiResource.ToEntity());
                }
            }
            if (!configurationDbContext.ApiScopes.Any())
            {
                foreach (var apiScope in Config.GetApiScopes())
                {
                    configurationDbContext.ApiScopes.Add(apiScope.ToEntity());
                }
            }
            if (!configurationDbContext.IdentityResources.Any())
            {
                foreach (var identityResource in Config.GetIdentityResources())
                {
                    configurationDbContext.IdentityResources.Add(identityResource.ToEntity());
                }
            }

            configurationDbContext.SaveChanges();
        }
    }
}
