using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dictionary
{
    public abstract class LanguageWord
    {
        public int Id { get; set; }
        [Required]
        public string Word { get; set; }
        public List<LanguageWord> OtherWords { get; set; } = new List<LanguageWord>();
    }
}
