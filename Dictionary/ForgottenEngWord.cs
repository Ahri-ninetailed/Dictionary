using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace Dictionary
{
    public class ForgottenEngWord
    {
        public int Id { get; set; }
        [Required]
        public string Word { get; set; }
        public List<RusWord> OtherRusWords { get; set; } = new List<RusWord>();
        public int CountOfRepetitions { get; set; }
    }
}
