using System.Collections.Generic;
namespace Dictionary.Menu
{
    class MenuV01 : AbstractMenu
    {
        public MenuV01() : base()
        {

        }
        public MenuV01(IShowerMenu showerMenu, IChooseCommand chooseCommand) : base(showerMenu, chooseCommand)
        {

        }

        public override IShowerMenu ShowerMenu { get; set; } 
        public override IChooseCommand ChooseCommand { get; set; }
    }
    
}
