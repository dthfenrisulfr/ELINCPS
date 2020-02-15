using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

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
            CreateHostBuilder(args).Build().Run();
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
