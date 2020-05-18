using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog;
using System;

namespace Elin_Course_Project_Service
{
  /// <summary>
  ///  ����� � ������ ����� � ���������
  /// </summary>
  public class Program
  {
    /// <summary>
    ///  ����� ����� � ���������
    /// </summary>
    public static void Main(string[] args)
    {
      Logger _logger = LogManager.GetCurrentClassLogger();
      _logger.Info($"������ ������� ���������� � {DateTime.Now}");
      CreateHostBuilder(args).Build().Run();
      _logger.Info($"������ �������� ������ � {DateTime.Now}");
    }
    /// <summary>
    /// ������ ������������� ����
    /// </summary>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
