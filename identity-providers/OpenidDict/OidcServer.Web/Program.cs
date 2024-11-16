using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OidcServer.Web;
using OpenIddict.Server;

try
{
    var base64Pfx = Environment.GetEnvironmentVariable("PFX_BASE64");
    if (!string.IsNullOrEmpty(base64Pfx))
    {
        var pfxBytes = Convert.FromBase64String(base64Pfx);
        File.WriteAllBytes("https_certificate.pfx", pfxBytes);
    }
}
catch (Exception e)
{
    Console.WriteLine("Failed to load PFX onto disk. ");
    throw;
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite($"Filename={Path.Combine(Path.GetTempPath(), "openiddict-oidcproxy-test-server.sqlite3")}");
    options.UseOpenIddict();
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddOpenIddict()
    .AddCore(o => o.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>())
    .AddServer(options =>
    {
        options.SetTokenEndpointUris("connect/token");

        options.SetAuthorizationEndpointUris("connect/authorize");

        options.SetLogoutEndpointUris("connect/logout");
        
        options
            .AllowAuthorizationCodeFlow()
                .RequireProofKeyForCodeExchange()
                .AllowRefreshTokenFlow()
            .SetAccessTokenLifetime(TimeSpan.FromSeconds(90));;
        
        options.AddEncryptionKey(new SymmetricSecurityKey(
            Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=")));
        
        options
            .AddDevelopmentSigningCertificate();

        options
            .UseAspNetCore()
            .EnableTokenEndpointPassthrough()
            .EnableAuthorizationEndpointPassthrough()
            .EnableLogoutEndpointPassthrough();

        // Todo: Fix this properly (help wanted)
        
        // When exchanging the authorization code for a token, the request to the token-endpoint must contain
        // at least the scope 'openid' and 'offline_access'. Otherwise, the id_token and the refresh_token will
        // be empty.
        
        // When submitting this request to the OpenIddict server, it throws an exception: "The 'scope' parameter is not
        // valid in this context."
        
        // This is because the ticket doesn't contain any scopes. However, when skipping this check, everything works
        // properly...
        options
            .RemoveEventHandler(OpenIddictServerHandlers.Exchange.ValidateScopeParameter.Descriptor);
    })

    .AddValidation(options =>
    {
        options.UseLocalServer();
        options.UseAspNetCore();
    });

builder.Services.AddHostedService<ClientConfiguration>();

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(options =>
{
    options.MapControllers();
    options.MapDefaultControllerRoute();
});

app
    .MapAuthorizeEndpoint()
    .MapTokenEndpoint()
    .MapLogoutEndpoint();

app.Run();