using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class BatteryGooI: InventoryItem
    {
        public BatteryGooI()
            : base()
        {
            goodGraphic = Textures.textures["battery"];
            badGraphic = Textures.textures["goo"];
            width = 32;
            height = 32;
            itemTipGood = "This might be useful";
            itemTipBad = "This smells weird";
        }
    }
}
