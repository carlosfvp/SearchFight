using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Application.Interfaces
{
    public interface ISerpAPIService
    {
        int TotalResultsFromSearch(string engine, string searchTerm);
    }
}
