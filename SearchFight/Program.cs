using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SearchFight.Application.Services;
using SearchFight.Application.Interfaces;
using System;
using SearchFight.Domain.Repositories;
using SearchFight.Infrastructure;

namespace SearchFight
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            ISerpAPIService? searchService = host.Services.GetService<ISerpAPIService>();

            int totalResults = searchService.TotalResultsFromSearch("google", "java");

            Console.WriteLine(totalResults);

            host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(Builder =>
                {
                    Builder.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((_, services) =>
                {
                    services.AddTransient<ISerpAPIService, SerpAPIService>();
                    services.AddTransient<ISerpAPIRepository, SerpAPIRepository>(sp => {
                        string baseURL = _.Configuration["SearchEngineAPI:SerpApi:BaseURL"];
                        string token = _.Configuration["SearchEngineAPI:SerpApi:Token"];

                        return new SerpAPIRepository(baseURL, token);
                    });
                });
                    
    }
}