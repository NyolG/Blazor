using BlazorABC.Client;
using BlazorABC.Client.Servicios;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorABC.Client.Extensiones;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5135") });
builder.Services.AddScoped<IUsuarioServicio, UsuariosServicio>();
builder.Services.AddScoped<IHistorialServicio, HistorialServicio>();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, autenticacion>();
builder.Services.AddAuthorizationCore();
builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
