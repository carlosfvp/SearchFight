using SearchFight.Application.DTO;
using SearchFight.Application.Interfaces;
using SearchFight.Domain.Models.DTO.SearchEngineAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Application.Services
{
    public class RankingService : IRankingService
    {
        private readonly ISearchEngineAPIService _searchEngineAPIService;
        private List<string> _searchTerms;
        private List<SearchAPIResultDTO> _searchResults;

        public RankingService(ISearchEngineAPIService searchEngineAPIService)
        {
            _searchTerms = new List<string>();
            _searchResults = new List<SearchAPIResultDTO>();
            _searchEngineAPIService = searchEngineAPIService ?? throw new ArgumentNullException(nameof(searchEngineAPIService));
        }

        public void SetSearchTerms(string[] searchTerms)
        {
            _searchTerms.AddRange(searchTerms);
        }

        public void ExecuteSearch()
        {
            var termsResults = _searchTerms.Select(x => _searchEngineAPIService.GetResultsFromSearch(x));

            foreach(var termResults in termsResults)
            {
                _searchResults.AddRange(termResults);
            }
        }

        public List<string> GetResultsPerTermPrint()
        {
            var resultsPerTerms = _searchResults.GroupBy(r => r.SearchTerm)
                .Select(group => new
                {
                    Term = group.Key,
                    Results = group.ToList()
                });

            var printResults = resultsPerTerms.Select(resultsPerTerm =>
            {
                var searchTerm = resultsPerTerm.Results.Select(x => x.SearchTerm).First();
                var termPrintResults = resultsPerTerm.Results.Select(x => string.Format("{0}: {1}", x.SearchEngineName, x.ResultCount)).ToArray();
                return searchTerm + ": " +string.Join(' ', termPrintResults);
            });

            return printResults.ToList();
        }

        public List<SearchAPIResultDTO> GetTermPerEngineWithMostResults()
        {
            List<SearchAPIResultDTO> results = new List<SearchAPIResultDTO>();

            var engineWinners = _searchResults.GroupBy(r => r.SearchEngineName)
                .SelectMany(group =>
                {
                    return group.OrderByDescending(y => y.ResultCount)
                        .Select((x, i) => new
                        {
                            group.Key,
                            Item = x,
                            Rank = i + 1
                        });
                });

            return engineWinners.Where(x => x.Rank == 1).Select(x => x.Item).ToList();
        }

        public string GetTermWithMostResults()
        {
            var totalResults = _searchResults.GroupBy(r => r.SearchTerm)
                .Select(group => new
                {
                    Term = group.Key,
                    SumTotalResults = group.Sum(x => x.ResultCount)
                }).OrderByDescending(x=>x.SumTotalResults);

            return totalResults.First().Term;
        }
    }
}
