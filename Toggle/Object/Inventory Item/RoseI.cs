using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class RoseI : InventoryItem
    {

        public RoseI()
            : base()
        {
            goodGraphic = Textures.textures["rose"];
            badGraphic = Textures.textures["rose"];
            width = 32;
            height = 32;
            itemTipGood = "It's quite beautiful";
            itemTipBad = "It's quite sharp";
        }
    }
}
