using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class ButtonHeavy: Button
    {
        public ButtonHeavy(int xLocation, int yLocation, Miscellanious linked)
            : base(xLocation, yLocation, linked)
        {
            goodGraphic = Textures.textures["buttonHUp"];
            badGraphic = Textures.textures["buttonHUp"];
            link = linked;
            heavyButton = true;
        }
    }


}
