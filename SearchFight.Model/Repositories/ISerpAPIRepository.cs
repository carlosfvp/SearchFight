using SearchFight.Domain.Models.SearchEngineAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Domain.Repositories
{
    public interface ISerpAPIRepository : ISearchEngineAPIRepository<SerpAPIResult>
    {
        
    }
}
