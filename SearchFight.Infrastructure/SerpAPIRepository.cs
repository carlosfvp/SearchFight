using SearchFight.Domain.Models.SearchEngineAPI;
using SearchFight.Domain.Repositories;


namespace SearchFight.Infrastructure
{
    public class SerpAPIRepository : ISerpAPIRepository
    {
        private readonly string baseURL;
        private readonly string token;
        public SerpAPIRepository(string baseURL, string token)
        {
            this.baseURL = baseURL;
            this.token = token;
        }

        public SerpAPIResult GetSearchInformation(string engine, string searchTerm)
        {
            // TODO: call API and get results

            return new SerpAPIResult()
            {
                ResultCount = 100000,
                SearchEngineName = engine,
                TotalTimeTaken = float.Parse("0.4")
            };
        }
    }
}