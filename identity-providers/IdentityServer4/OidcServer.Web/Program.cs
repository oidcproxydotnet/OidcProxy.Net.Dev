using Microsoft.AspNetCore.HttpOverrides;
using OidcServer.Web.ModuleInitializers;

namespace OidcServer.Web;

public class Program
{
    public static void Main(string[] args)
    {
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

        ConfigureServices(builder);

        var app = builder.Build();

        ConfigureApp(app);

        app.Run();
    }

    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews();
        builder.Services.AddIdentityServer4();
    }
    
    public static void ConfigureApp(WebApplication app)
    {
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        app.UseCertificateForwarding();
        app.UseCookiePolicy();

        app.UseDeveloperExceptionPage();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseIdentityServer();

        app.UseAuthorization();

        app.MapDefaultControllerRoute();
    }
}