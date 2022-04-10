using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Infrastructure.SerpAPI.Engines
{
    public class SerpAPIEngineBase : ISerpAPIEngine
    {
        protected readonly string _baseURL;
        protected readonly string _token;
        protected readonly string _engine;

        public SerpAPIEngineBase(string baseURL, string token, string engine)
        {
            if (string.IsNullOrEmpty(baseURL)) throw new ArgumentNullException(nameof(baseURL));
            if (string.IsNullOrEmpty(token)) throw new ArgumentNullException(nameof(token));
            if (string.IsNullOrEmpty(engine)) throw new ArgumentNullException(nameof(engine));

            _baseURL = baseURL;
            _token = token;
            _engine = engine;
        }

        public virtual JObject query(string searchTerm)
        {
            return new JObject();
        }

        protected async Task<JObject> MakeAPIRequest(string requestURL)
        {
            HttpClient client = new HttpClient();
            var res = await client.GetAsync(requestURL);

            var responseBody = await res.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<JObject>(responseBody);
        }
    }
}
