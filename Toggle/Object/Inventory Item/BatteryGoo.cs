using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class BatteryGoo : InventoryItem
    {
        public BatteryGoo() : base(){
            goodGraphic = Textures.textures["battery"];
            badGraphic = Textures.textures["goo"];
            width = 32;
            height = 32;
            itemTipGood = "It's a battery";
            itemTipBad = "Don't touch this";
        }
    }
}
