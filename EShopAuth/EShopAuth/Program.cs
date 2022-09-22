using EShopAuth;
using EShopAuth.Data;
using EShopAuth.DataModel;
using IdentityServer4;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddCors(options =>
{
    // this defines a CORS policy called "default"
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins("https://localhost:7005")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
    options.EmitStaticAudienceClaim = true;
})
    .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResource())
    .AddInMemoryApiScopes(IdentityServerConfig.GetApiScope())
    .AddInMemoryClients(IdentityServerConfig.GetClients())
    .AddDeveloperSigningCredential()
    .AddAspNetIdentity<ApplicationUser>();

builder.Services.AddAuthentication()
                .AddOpenIdConnect("oidc", "Demo IdentityServer", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                    options.SaveTokens = true;

                    options.Authority = "https://demo.identityserver.io/";
                    options.ClientId = "interactive.confidential";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                });


builder.Services.AddControllersWithViews();
var app = builder.Build();



#region seed data
//var seed = args.Contains("/seed");
//if (seed)
//{
//    args = args.Except(new[] { "/seed" }).ToArray();
//}

////var host = CreateHostBuilder(args).Build();

//if (seed)
//{
    //var config = app.Services.GetRequiredService<IConfiguration>();
    //var connectionString = config.GetConnectionString("DefaultConnection");
    //SeedData.EnsureSeedData(connectionString);
//}

#endregion


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseRouting();

app.UseDefaultFiles();
app.UseStaticFiles();


app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());

app.UseAuthentication();
app.UseIdentityServer();

//app.UseCors("default");

app.UseAuthorization();
app.UseEndpoints(endpoint =>
{
    endpoint.MapDefaultControllerRoute();
});

app.Run();

