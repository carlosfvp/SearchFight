using SearchFight.Domain.Models.SearchEngineAPI;
using SearchFight.Domain.Repositories;


namespace SearchFight.Infrastructure
{
    public class SearchEngineAPIRepository : ISearchEngineAPIRepository<Result>
    {
        private readonly string BaseURL;
        private readonly string Token;
        public SearchEngineAPIRepository(string BaseURL, string Token)
        {
            this.BaseURL = BaseURL;
            this.Token = Token;
        }

        public Result GetSearchInformation(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}