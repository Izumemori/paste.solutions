using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace PasteSolutions
{
    public class Program
    {
        public static Task Main(string[] args)
            => CreateHostBuilder(args).Build().RunAsync();
            
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(x => {
                    x.AddJsonFile("Config/config.json");
                    x.AddEnvironmentVariables("PASTE_SOLUTIONS_");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseWebRoot("wwwroot");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
