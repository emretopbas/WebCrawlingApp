using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebCrawlingApp;

IHost _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
    {
        services.AddSingleton<Browse>();
    }).Build();

var app = _host.Services.GetRequiredService<Browse>();
app.Run();