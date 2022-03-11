﻿using System;
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

        int countOfRepetitions;
        public int CountOfRepetitions
        {
            get
            {
                return countOfRepetitions;
            }
            set
            {
                countOfRepetitions = value;
                if (countOfRepetitions == 0)
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.ForgottenEngWords.Remove(this);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
