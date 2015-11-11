using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class RopeI : InventoryItem
    {
        public RopeI(Item i)
            : base(i)
        {
            goodGraphic = Textures.textures["rope"];
            badGraphic = Textures.textures["chain"];
            width = 32;
            height = 32;
            itemTipGood = "This is a strong rope";
            itemTipBad = "I could make a noose from this";
        }


    }
}
