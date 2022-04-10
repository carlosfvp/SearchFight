using SearchFight.Application.DTO;
using SearchFight.Domain.Models.DTO.SearchEngineAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Application.Interfaces
{
    public interface ISearchEngineAPIService
    {
        List<SearchAPIResultDTO> GetResultsFromSearch(string searchTerm);
    }
}
