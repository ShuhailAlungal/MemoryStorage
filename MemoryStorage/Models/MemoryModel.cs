using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoryStorage.Models
{
    public class MemoryModel
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string NoOfItems { get; set; }
        public string Make { get; set; }
        public string Speed { get; set; }
        public string Processor { get; set; }
    }
}
