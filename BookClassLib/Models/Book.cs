using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookClassLib.Models
{
    public partial class Book
    {
        [Key]
        public int BookNo { get; set; }
        [Required]
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
    }
}
