using SearchFight.Application.DTO;
using SearchFight.Domain.Models.DTO.SearchEngineAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Application.Interfaces
{
    public interface IRankingService
    {
        public void SetSearchTerms(string[] searchTerms);
        public void ExecuteSearch();

        public List<string> GetResultsPerTermPrint();

        public List<SearchAPIResultDTO> GetTermPerEngineWithMostResults();

        public string GetTermWithMostResults();
    }
}
