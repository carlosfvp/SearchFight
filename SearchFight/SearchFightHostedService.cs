using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SearchFight.Application.DTO;
using SearchFight.Application.Interfaces;
using SearchFight.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Console
{
    public class SearchFightHostedService : BackgroundService
    {
        private readonly ILogger<SearchFightHostedService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly CommandLineArguments _arguments;

        public SearchFightHostedService(ILogger<SearchFightHostedService> logger, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory, CommandLineArguments arguments)
        {
            _logger = logger;
            _configuration = configuration;
            _serviceScopeFactory = serviceScopeFactory;
            _arguments = arguments;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                try
                {
                    IRankingService rankingService = scope.ServiceProvider.GetService<IRankingService>();

                    if (rankingService != null)
                    {
                        rankingService.SetSearchTerms(_arguments.Arguments);
                        rankingService.ExecuteSearch();

                        List<string> printResults = rankingService.GetResultsPerTermPrint();

                        foreach(var printResult in printResults)
                        {
                            _logger.LogInformation(printResult);
                        }

                        List<SearchAPIResultDTO> enginesWinners = rankingService.GetTermPerEngineWithMostResults();

                        foreach (var engine in enginesWinners)
                        {
                            _logger.LogInformation("{0} winner: {1}", engine.SearchEngineName, engine.SearchTerm);
                        }

                        string termWinner = rankingService.GetTermWithMostResults();

                        _logger.LogInformation("Total winner: {0}", termWinner);

                        //List<SearchTermResult> searchTermResults = new List<SearchTermResult>();

                        //foreach (var searchTerm in _arguments.Arguments)
                        //{
                        //    SearchTermResult result = new SearchTermResult()
                        //    {
                        //        SearchTerm = searchTerm
                        //    };

                        //    var searchTermResults = engines.Select((engine) =>
                        //    {
                        //        decimal totalResults = searchService.TotalResultsFromSearch(engine, searchTerm);

                        //        return new SearchTermEngineResult()
                        //        {
                        //            Engine = engine,
                        //            TotalResults = totalResults
                        //        };
                        //    });

                        //    result.SearchTermEngineResults = searchTermResults.ToList();

                        //    resultsPerSearchTerm.Add(result);

                        //    _logger.LogInformation("{0}: {1}", searchTerm, result.ToString());
                        //}

                        //var engineWinners = termResults.GroupBy(r => r.Engine)
                        //    .Select(group => new
                        //    {
                        //        Engine = group.Key,
                        //        TermResultRank = group.OrderByDescending(x => x.TotalResults)
                        //    });

                        //var totalWinner = termResults.GroupBy(r => r.SearchTerm)
                        //    .Select(group => new
                        //    {
                        //        Term = group.Key,
                        //        SumTotalResults = group.Sum(x => x.TotalResults)
                        //    });

                        //foreach (var engine in engineWinners)
                        //{
                        //    var engineWinner = engine.TermResultRank.First();
                        //    _logger.LogInformation("{0} winner: {1}", engineWinner.Engine, engineWinner.SearchTerm);
                        //}

                        //_logger.LogInformation("Total winner: {1}", totalWinner.First().Term);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Exception: {message}", ex.Message);
                }
            }
        }
    }
}