using BlazorSignalRApp.Client.Pages;
using BlazorSignalRApp.Components;
using Microsoft.AspNetCore.ResponseCompression;
using BlazorSignalRApp.Hubs;
using Microsoft.Azure.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR()
    .AddAzureSignalR(options =>
    {
        options.ConnectionString = "Endpoint=https://xgarip00.service.signalr.net;AccessKey=EL3OQdaEsaEBV6UTlK34pOzdhd0EV5auV1UkJI5khJUUrF5AFqcCJQQJ99AKACPV0roXJ3w3AAAAASRSPg0T;Version=1.0;";
    });

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// builder.Services.AddSignalR();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        ["application/octet-stream"]);
});

var app = builder.Build();

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorSignalRApp.Client._Imports).Assembly);

app.MapHub<ChatHub>("/chathub");

app.Run();