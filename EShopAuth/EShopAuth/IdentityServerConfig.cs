using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopAuth
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResource()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiScope> GetApiScope()
        {
            return new List<ApiScope> {
                new ApiScope("eshopapi", "Eshop Api"),
                new ApiScope("angularapi", "Angular API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                //new Client{
                //    ClientId = "eshop_clent",
                //    //ClientSecrets = { new Secret("eshop_secret".ToSha256())},
                //    RequireClientSecret= false,
                //    AllowedGrantTypes = GrantTypes.Implicit,
                //    AllowedScopes = {"EshopApi",
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        IdentityServerConstants.StandardScopes.Email},
                //    AllowAccessTokensViaBrowser = true,
                //    AllowedCorsOrigins = { "http://localhost:7005" },
                //    RedirectUris = { "https://localhost:7005/callback.html" },
                //    PostLogoutRedirectUris = { "http://localhost:7005/index.html" }
                //},
                new Client
                {
                    ClientId = "eshop_client",
                    ClientName = "Eshop Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,

                    RedirectUris =           { "https://localhost:7005/callback.html" },
                    PostLogoutRedirectUris = { "https://localhost:7005/index.html" },
                    AllowedCorsOrigins =     { "https://localhost:7005" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "eshopapi"
                    }
                },
               
                new Client
                {
                    ClientName = "Angular-Client",
                    ClientId = "angular-client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string>{ "http://localhost:4200/signin-callback", "http://localhost:4200/assets/silent-callback.html" },
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "angularapi"
                    },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    RequireClientSecret = false,
                    PostLogoutRedirectUris = new List<string> { "http://localhost:4200/signout-callback" },
                    RequireConsent = false,
                    AccessTokenLifetime = 600
                }
            };
        }
    }
}
