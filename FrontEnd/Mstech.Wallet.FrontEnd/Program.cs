using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Mstech.Wallet.FrontEnd.Extensions;
using System.Text.Json;
using Mstech.Wallet.FrontEnd.Setting;
using  Mstech.Wallet.FrontEnd;



var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");

builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Configuration.AddJsonFile("appsettings.json");

string clientApi = builder.Configuration["ApiSettings:ClientApi"];

string jsonFilePath = "appsettings.json";


string jsonString = File.ReadAllText(jsonFilePath);

AppSettingsJson setting = JsonSerializer.Deserialize<AppSettingsJson>(jsonString);

builder.Services.AddCustomServices(setting.AppSettings.ClientIdForApi);

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();
