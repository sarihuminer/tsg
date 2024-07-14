using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.DTOs
{
    public class PrioritySettings
    {
        public string EntityType { get; set; }

        public Dictionary<string, List<string>> Priorities { get; set; }
    }
}
