using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Infrastructure.SerpAPI.Engines
{
    public class SerpAPIGoogle : SerpAPIEngineBase
    {
        public SerpAPIGoogle(string baseURL, string token, string engine) : base(baseURL, token, engine)
        {
        }

        public override JObject query(string searchTerm)
        {
            string requestURL = string.Format(_baseURL + "?engine={0}&q={1}&api_key={2}", _engine, searchTerm, _token);
            return MakeAPIRequest(requestURL).Result;
        }
    }
}
