using SearchFight.Application.Interfaces;

namespace SearchFight.Application.Services
{
    public class SearchEngineBaseService : ISearchEngineService
    {
        
        public SearchEngineBaseService()
        {

        }

        public int TotalResultsFromSearch(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}