using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Dictionary
{
    public class EngWord 
    {
        public int Id { get; set; }
        [Required]
        public string Word { get; set; }
        public List<RusWord> OtherWords { get; set; } = new List<RusWord>();
    }
}
