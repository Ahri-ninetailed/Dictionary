using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Dictionary
{
    public class RusWord
    {
        public int Id { get; set; }
        [Required]
        public string Word { get; set; }
        public List<EngWord> OtherWords { get; set; } = new List<EngWord>();
    }
}
