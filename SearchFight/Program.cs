using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SearchFight.Application.Services;
using SearchFight.Application.Interfaces;
using SearchFight.Domain.Repositories;
using SearchFight.Infrastructure;

namespace SearchFight.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args);
            var host = hostBuilder.Build();
            host.RunAsync().Wait();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile("hostsettings.json", optional: true);
                    configHost.AddEnvironmentVariables(prefix: "PREFIX_");
                    configHost.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddJsonFile("appsettings.json", optional: true);
                    configApp.AddJsonFile(
                        $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                        optional: true);
                    configApp.AddEnvironmentVariables(prefix: "PREFIX_");
                    configApp.AddCommandLine(args);
                })
                .ConfigureServices((builderContext, services) =>
                {
                    services.AddSingleton(new CommandLineArguments { Arguments = args });
                    services.AddHostedService<SearchFightHostedService>();
                    services.AddTransient<IRankingService, RankingService>();
                    
                    string currentAPI = builderContext.Configuration["SearchEngineAPI:DefaultAPI"];

                    if (currentAPI == "SerpApi")
                    {
                        services.AddTransient<ISearchEngineAPIService, SerpAPIService>();
                        services.AddTransient<ISearchEngineAPIRepository, SerpAPIRepository>(sp =>
                        {
                            string baseURL = builderContext.Configuration["SearchEngineAPI:SerpApi:BaseURL"];
                            string token = builderContext.Configuration["SearchEngineAPI:SerpApi:Token"];
                            string[] engines = builderContext.Configuration.GetSection("SearchEngineAPI:SerpApi:Engines")
                                .GetChildren()
                                .Select(c => c.Value)
                                .ToArray();

                            return new SerpAPIRepository(baseURL, token, engines);
                        });
                    }
                    else
                    {
                        throw new Exception("Using not implemented Search API");
                    }
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging.AddConsole();
                })
                .UseConsoleLifetime();
        }
    }
}