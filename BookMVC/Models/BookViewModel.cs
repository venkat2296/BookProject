using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMVC.Models
{
    public class BookViewModel
    {
        public int BookNo { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
    }
}
