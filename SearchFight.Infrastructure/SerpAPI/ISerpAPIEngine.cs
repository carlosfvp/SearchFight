using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Infrastructure.SerpAPI
{
    internal interface ISerpAPIEngine
    {
        JObject query(string searchTerm);
    }
}
