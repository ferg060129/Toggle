using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class KnifeI : InventoryItem
    {

        public KnifeI(Item i)
            : base(i)
        {
            goodGraphic = Textures.textures["knife"];
            badGraphic = Textures.textures["knife"];
            width = 32;
            height = 32;
            itemTipGood = "This is sharp";
            itemTipBad = "Whose blood is this?";
        }
    }
}
