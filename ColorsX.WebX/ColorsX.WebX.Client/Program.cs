using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ColorsX.WebX.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        // builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new("https+http://apiservice") });
        builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7471/") });


        //builder.Services.AddTransient(sp =>
        //{
        //    var httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7471") };
        //    return new ColorsApiClient(httpClient);
        //});

        var host = builder.Build();



        await host.RunAsync();
    }
}
