using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class Button : Miscellanious
    {
        //When you press the button, link does something
        Miscellanious link;
         public Button(int xLocation, int yLocation, Miscellanious linked)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["buttonUp"];
            badGraphic = Textures.textures["buttonUp"];
            link = linked;
        }
    
        public override void onTrigger()
        {
            goodGraphic = Textures.textures["buttonDown"];
            badGraphic = Textures.textures["buttonDown"];
            link.onButton();
        }
    }
}
