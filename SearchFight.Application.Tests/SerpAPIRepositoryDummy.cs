using SearchFight.Domain.Models.DTO.SearchEngineAPI;
using SearchFight.Domain.Repositories;
using SearchFight.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Application.Tests
{
    internal class SerpAPIRepositoryDummy : ISearchEngineAPIRepository
    {
        private readonly string[] _engines;
        private List<SearchAPIResultBase> _dummyResult;

        public SerpAPIRepositoryDummy(string[] engines)
        {
            _engines = engines;
        }

        public void SetDummyResult(List<SearchAPIResultBase>  dummyResult)
        {
            _dummyResult = dummyResult;
        }

        List<SearchAPIResultBase> ISearchEngineAPIRepository.GetResultsFromAllEngines(string searchTerm)
        {
            return _dummyResult.Where(x=>x.SearchTerm == searchTerm).ToList();
        }

        SearchAPIResultBase ISearchEngineAPIRepository.GetSearchInformation(string engine, string searchTerm)
        {
            return new SerpAPIResult()
            {
                Result = "OK",
                ResultCount = Random.Shared.Next(1000000, 10000000),
                SearchEngineName = engine,
                SearchTerm = searchTerm
            };
        }
    }
}
