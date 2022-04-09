using SearchFight.Application.Interfaces;
using SearchFight.Domain.Models.SearchEngineAPI;
using SearchFight.Domain.Repositories;

namespace SearchFight.Application.Services
{
    public class SerpAPIService : ISerpAPIService
    {
        ISerpAPIRepository searchEngineRepository;
        public SerpAPIService(ISerpAPIRepository searchEngineRepository)
        {
            this.searchEngineRepository = searchEngineRepository;
        }

        public int TotalResultsFromSearch(string engine, string searchTerm)
        {
            SerpAPIResult result = searchEngineRepository.GetSearchInformation(engine, searchTerm);
            return result.ResultCount;
        }
    }
}