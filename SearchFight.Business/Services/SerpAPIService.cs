using SearchFight.Application.DTO;
using SearchFight.Application.Interfaces;
using SearchFight.Domain.Models.DTO.SearchEngineAPI;
using SearchFight.Domain.Repositories;

namespace SearchFight.Application.Services
{
    public class SerpAPIService : ISerpAPIService
    {
        ISearchEngineAPIRepository _searchEngineRepository;
        public SerpAPIService(ISearchEngineAPIRepository searchEngineRepository)
        {
            _searchEngineRepository = searchEngineRepository;
        }

        public List<SearchAPIResultDTO> GetResultsFromSearch(string searchTerm)
        {
            List<SearchAPIResultBase> baseResults = _searchEngineRepository.GetResultsFromAllEngines(searchTerm);

            List<SearchAPIResultDTO> results = new List<SearchAPIResultDTO>();

            foreach (SerpAPIResult result in baseResults)
            {
                results.Add(new SearchAPIResultDTO()
                {
                    Result = result.Result,
                    ResultCount = result.ResultCount,
                    SearchEngineName = result.SearchEngineName,
                    SearchTerm = result.SearchTerm,
                    TotalTimeTaken = result.TotalTimeTaken
                });
            }

            return results;
        }

    }
}