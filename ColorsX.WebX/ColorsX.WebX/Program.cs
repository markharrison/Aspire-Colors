using ColorsX.WebX.Client.Pages;
using ColorsX.WebX.Components;

namespace ColorsX.WebX;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new("https+http://apiservice") });

        //builder.Services.AddHttpClient<ColorsX.WebX.Client.ColorsApiClient>(client =>
        //{
        //    // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
        //    // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
        //    client.BaseAddress = new("https+http://apiservice");
        //});

        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.MapStaticAssets();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .WithStaticAssets()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.Run();
    }
}
