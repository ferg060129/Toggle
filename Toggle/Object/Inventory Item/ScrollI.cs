using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle 
{
    class ScrollI : InventoryItem
    {
        public ScrollI(string tipG, string tipB)
            : base()
        {
            goodGraphic = Textures.textures["itemblock"];
            badGraphic = Textures.textures["baditemblock"];
            width = 32;
            height = 32;
            itemTipGood = "Avoid the frowns";
            itemTipBad = "He's afraid of the light";
        }
    }
}
