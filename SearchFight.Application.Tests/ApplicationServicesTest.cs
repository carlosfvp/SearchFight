using NUnit.Framework;
using SearchFight.Application.DTO;
using SearchFight.Application.Services;
using SearchFight.Domain.Models.DTO.SearchEngineAPI;
using SearchFight.Domain.Repositories;
using System.Linq;
using System.Collections.Generic;

namespace SearchFight.Application.Tests
{
    public class Tests
    {
        private string _baseURL;
        private string _token;
        private string[] _engines;

        [SetUp]
        public void Setup()
        {
            _baseURL = "https://serpapi.com/search.json";
            _token = "faf25156fbd3f3d46b0639b4cb3ac4f1654fa5967a923b51f7c70e62646e06a7";
            _engines = new string[] { "google", "yahoo" };
        }

        [Test]
        public void SerpAPIServiceTest()
        {
            SerpAPIRepositoryDummy _repository = new SerpAPIRepositoryDummy(_engines);

            List<SearchAPIResultBase> results = new List<SearchAPIResultBase>();

            results.Add(new SerpAPIResult()
            {
                Result = "OK",
                SearchTerm = "java",
                SearchEngineName = "google",
                ResultCount = 3800000000,
                TotalTimeTaken = 0.5f
            });

            results.Add(new SerpAPIResult()
            {
                Result = "OK",
                SearchTerm = "java",
                SearchEngineName = "yahoo",
                ResultCount = 39200000,
                TotalTimeTaken = 0.5f
            });

            results.Add(new SerpAPIResult()
            {
                Result = "OK",
                SearchTerm = ".net core",
                SearchEngineName = "yahoo",
                ResultCount = 9050000,
                TotalTimeTaken = 0.5f
            });

            results.Add(new SerpAPIResult()
            {
                Result = "OK",
                SearchTerm = ".net core",
                SearchEngineName = "google",
                ResultCount = 153000000,
                TotalTimeTaken = 0.5f
            });

            _repository.SetDummyResult(results);

            SerpAPIService service = new SerpAPIService(_repository);

            List<SearchAPIResultDTO> dto = service.GetResultsFromSearch("java");
            List<SearchAPIResultDTO> dto2 = service.GetResultsFromSearch(".net core");

            Assert.IsTrue(dto.Count == 2);
            Assert.IsTrue(dto2.Count == 2);

            Assert.Pass();
        }
        
        [Test]
        public void RankingServiceTest()
        {
            SerpAPIRepositoryDummy _repository = new SerpAPIRepositoryDummy(_engines);

            List<SearchAPIResultBase> results = new List<SearchAPIResultBase>();

            results.Add(new SerpAPIResult()
            {
                Result = "OK",
                SearchTerm = "java",
                SearchEngineName = "google",
                ResultCount = 390000,
                TotalTimeTaken = 0.5f
            });

            results.Add(new SerpAPIResult()
            {
                Result = "OK",
                SearchTerm = "java",
                SearchEngineName = "yahoo",
                ResultCount = 38000000,
                TotalTimeTaken = 0.5f
            });

            results.Add(new SerpAPIResult()
            {
                Result = "OK",
                SearchTerm = ".net core",
                SearchEngineName = "yahoo",
                ResultCount = 9050000,
                TotalTimeTaken = 0.5f
            });

            results.Add(new SerpAPIResult()
            {
                Result = "OK",
                SearchTerm = ".net core",
                SearchEngineName = "google",
                ResultCount = 153000000,
                TotalTimeTaken = 0.5f
            });

            _repository.SetDummyResult(results);

            SerpAPIService apiService = new SerpAPIService(_repository);

            RankingService rankingService = new RankingService(apiService);

            rankingService.SetSearchTerms(new string[] { "java", ".net core" });

            rankingService.ExecuteSearch();

            string termMostResults = rankingService.GetTermWithMostResults();

            Assert.IsTrue(termMostResults == ".net core");

            List<SearchAPIResultDTO> detail = rankingService.GetTermPerEngineWithMostResults();

            SearchAPIResultDTO coreResult = detail.Where(x => x.SearchTerm == ".net core").First();
            SearchAPIResultDTO javaResult = detail.Where(x => x.SearchTerm == "java").First();

            Assert.IsTrue(coreResult.ResultCount == 153000000);
            Assert.IsTrue(coreResult.SearchEngineName == "google");
            Assert.IsTrue(javaResult.ResultCount == 38000000);
            Assert.IsTrue(javaResult.SearchEngineName == "yahoo");

            List<string> print = rankingService.GetResultsPerTermPrint();

            Assert.IsTrue(print.Count > 0);

            Assert.Pass();
        }
    }
}