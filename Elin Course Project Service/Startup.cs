using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using NLog;

namespace Elin_Course_Project_Service
{
  /// <summary>
  ///  Класс-конфигуратор приложения ASP.Net
  /// </summary>
  public class Startup
  {
    public IConfiguration Configuration { get; }
    /// <summary>
    ///  Стандартный конструктор класса
    /// </summary>
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }
    /// <summary>
    ///  Настраивает и добавляет в коллекцию все сервисы приложения
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddGrpc();
    }
    /// <summary>
    ///  Конфигуратор приложения
    /// </summary>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapGrpcService<Services.CPSService>();

        endpoints.MapGet("/", async context =>
              {
            await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
          });
      });
    }
  }
}
