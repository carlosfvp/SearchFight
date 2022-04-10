using SearchFight.Domain.Models.DTO.SearchEngineAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Domain.Repositories
{
    public interface ISearchEngineAPIRepository
    {
        List<SearchAPIResultBase> GetResultsFromAllEngines(string searchTerm);
        SearchAPIResultBase GetSearchInformation(string engine, string searchTerm);
    }
}
