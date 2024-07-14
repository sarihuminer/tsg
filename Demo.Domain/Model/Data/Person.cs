using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Model.Data
{
    public class Person : BaseEntity 
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int? Age { get; set; }

        public Address Address { get; set; }
    }
}
