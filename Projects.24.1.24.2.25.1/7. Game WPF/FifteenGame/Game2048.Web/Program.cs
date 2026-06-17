using Microsoft.Extensions.DependencyInjection;
using Game2048.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Game2048.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<GameHttpProxy>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7011/");
});

await builder.Build().RunAsync();
