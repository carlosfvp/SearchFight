using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Domain.Repositories
{
    public interface ISearchEngineAPIRepository<TEntity>
    {
        TEntity GetSearchInformation(string searchTerm);
    }
}
