using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SignalIR.Chat.Hubs;

namespace SignalIR.Chat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var host = new HostBuilder()
            //   .ConfigureLogging(logging =>
            //   {
            //       logging.AddConsole();
            //   })
            //   .ConfigureServices((services) =>
            //   {
            //       services.AddHostedService<ClockHubClient>();
            //   })
            //   .Build();

            //host.Run();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
