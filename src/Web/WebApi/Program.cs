using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try 
            {  
                Log.Information("Application Starting"); 
                
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();  
            
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(config)
                    .CreateLogger();
                
                CreateHostBuilder(args).Build().Run();  
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The Application failed to start");
            } 
            finally
            {
                Log.CloseAndFlush();
            }  
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
