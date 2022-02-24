using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dictionary
{
    //абстракция таблиц, которые будут хранить слова и переводы слов
    public interface ILanguageWord
    {
        //основной ключ
        public int Id { get; set; }
        //слово или фраза
        [Required]
        public string Word { get; set; }
        //возможные варианты перевода
        public List<ILanguageWord> OtherWords { get; set; }
    }
}
