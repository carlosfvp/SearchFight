using NUnit.Framework;
using SearchFight.Domain.Models.DTO.SearchEngineAPI;
using System;

namespace SearchFight.Infrastructure.Tests
{
    public class Tests
    {
        private string _baseURL;
        private string _token;
        private string[] _engines;
        private SerpAPIRepository _repository;

        [SetUp]
        public void Setup()
        {
            _baseURL = "https://serpapi.com/search.json";
            _token = "faf25156fbd3f3d46b0639b4cb3ac4f1654fa5967a923b51f7c70e62646e06a7";
            _engines = new string[] { "google", "yahoo" };
        }

        [Test]
        public void TestGoogle()
        {
            string term = "programming language";

            _repository = new SerpAPIRepository(_baseURL, _token, _engines);

            SerpAPIResult result = (SerpAPIResult)_repository.GetSearchInformation("google", term);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result == "OK");

            Assert.Pass();
        }

        [Test]
        public void TestYahoo()
        {
            string term = "programming language";

            _repository = new SerpAPIRepository(_baseURL, _token, _engines);

            SerpAPIResult result = (SerpAPIResult)_repository.GetSearchInformation("yahoo", term);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result == "OK");

            Assert.Pass();
        }

        [Test]
        public void FailWithoutParameters()
        {
            string term = "programming language";

            try
            {
                _repository = new SerpAPIRepository("", "", null);

                SerpAPIResult result = (SerpAPIResult)_repository.GetSearchInformation("yahoo", term);

                Assert.Fail();
            }
            catch (ArgumentNullException e)
            {
                Assert.Pass();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}