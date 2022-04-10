using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Application.DTO
{
    public class SearchAPIResultDTO
    {
        public string Result { get; set; }
        public string SearchEngineName { get; set; }
        public string SearchTerm { get; set; }
        public decimal ResultCount { get; set; }
        public float TotalTimeTaken { get; set; }
    }
}
