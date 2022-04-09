using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SearchFight.Application.Services;
using SearchFight.Application.Interfaces;
using System;

namespace SearchFight // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            ISearchEngineService searchService = host.Services.GetService<ISearchEngineService>();

            searchService.TotalResultsFromSearch("java");

            host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(Builder =>
                {
                    Builder.AddJsonFile("appsettings.json")
                        .AddCommandLine(args);
                })
                .ConfigureServices((_, services) =>
                    services//.AddHostedService<Worker>()
                            .AddTransient<ISearchEngineService, SearchEngineBaseService>());
    }
}