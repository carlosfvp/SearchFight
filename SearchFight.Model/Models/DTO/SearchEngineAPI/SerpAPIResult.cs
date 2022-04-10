using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Domain.Models.DTO.SearchEngineAPI
{
    public class SerpAPIResult : SearchAPIResultBase
    {
        public float TotalTimeTaken { get; set; }
    }
}
