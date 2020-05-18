using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog;
using System;

namespace Elin_Course_Project_Service
{
  /// <summary>
  ///  Класс с точкой входа в программу
  /// </summary>
  public class Program
  {
    /// <summary>
    ///  Точка входа в программу
    /// </summary>
    public static void Main(string[] args)
    {
      Logger _logger = LogManager.GetCurrentClassLogger();
      _logger.Info($"Запуск сервиса осуществлён в {DateTime.Now}");
      CreateHostBuilder(args).Build().Run();
      _logger.Info($"Сервер завершил работу в {DateTime.Now}");
    }
    /// <summary>
    /// Создаёт универсальный узел
    /// </summary>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
