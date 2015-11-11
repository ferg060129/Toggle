using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle 
{
    class ScrollI : InventoryItem
    {
        public ScrollI(Item i, string tipG, string tipB)
            : base(i)
        {
            goodGraphic = Textures.textures["itemblock"];
            badGraphic = Textures.textures["baditemblock"];
            width = 32;
            height = 32;
            itemTipGood = tipG;
            itemTipBad = tipB;
        }
    }
}
