using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SearchFight.Domain.Models.DTO.SearchEngineAPI;
using SearchFight.Domain.Repositories;
using SearchFight.Infrastructure.SerpAPI.Engines;
using System.Net;

namespace SearchFight.Infrastructure
{
    public class SerpAPIRepository : ISearchEngineAPIRepository
    {
        protected readonly string _baseURL;
        protected readonly string _token;
        protected readonly string[] _engines;

        public SerpAPIRepository(string baseURL, string token, string[] engines)
        {
            if (string.IsNullOrEmpty(baseURL)) throw new ArgumentNullException(nameof(baseURL));
            if (string.IsNullOrEmpty(token)) throw new ArgumentNullException(nameof(token));
            if (engines == null || engines.Length == 0) throw new ArgumentNullException(nameof(engines));

            _baseURL = baseURL;
            _token = token;
            _engines = engines;
        }

        public List<SearchAPIResultBase> GetResultsFromAllEngines(string searchTerm)
        {
            return _engines.Select(engine => GetSearchInformation(engine, searchTerm)).ToList();
        }

        public SearchAPIResultBase GetSearchInformation(string engine, string searchTerm)
        {
            SerpAPIResult result = new SerpAPIResult()
            {
                Result = "NOK",
                ResultCount = 0,
                SearchEngineName = engine,
                TotalTimeTaken = 0f
            };

            SerpAPIEngineBase engineAPI;

            if (engine == "google")
            {
                engineAPI = new SerpAPIGoogle(_baseURL, _token, engine);
                JObject jsonResponse = engineAPI.query(searchTerm);

                result = new SerpAPIResult()
                {
                    Result = "OK",
                    ResultCount = ulong.Parse(jsonResponse["search_information"]["total_results"].ToString()),
                    SearchEngineName = engine,
                    TotalTimeTaken = float.Parse(jsonResponse["search_metadata"]["total_time_taken"].ToString()),
                    SearchTerm = searchTerm
                };
            }
            else if (engine == "yahoo")
            {
                engineAPI = new SerpAPIYahoo(_baseURL, _token, engine);
                JObject jsonResponse = engineAPI.query(searchTerm);
                result = new SerpAPIResult()
                {
                    Result = "OK",
                    ResultCount = ulong.Parse(jsonResponse["search_information"]["total_results"].ToString()),
                    SearchEngineName = engine,
                    TotalTimeTaken = float.Parse(jsonResponse["search_metadata"]["total_time_taken"].ToString()),
                    SearchTerm = searchTerm
                };
            }
            else
            {
                throw new Exception("Engine not implemented");
            }

            return result;
        }
    }
}