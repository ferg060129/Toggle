using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class ButtonPlayer : Button
    {
        public ButtonPlayer(int xLocation, int yLocation, Miscellanious linked)
            : base(xLocation, yLocation, linked)
        {
            goodGraphic = Textures.textures["buttonUp"];
            badGraphic = Textures.textures["buttonUp"];
            link = linked;
            heavyButton = false;
        }


    }
}
