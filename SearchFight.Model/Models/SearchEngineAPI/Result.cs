using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Domain.Models.SearchEngineAPI
{
    public class Result
    {
        public string SearchEngineName { get; set; }
        public int ResultCount { get; set; }
        public float TotalTimeTaken { get; set; }
    }
}
