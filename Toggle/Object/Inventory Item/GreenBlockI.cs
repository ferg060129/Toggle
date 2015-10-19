using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle 
{
    class GreenBlockI : InventoryItem
    {
        public GreenBlockI() : base(){
            goodGraphic = Textures.textures["itemblock"];
            badGraphic = Textures.textures["baditemblock"];
            width = 32;
            height = 32;
            itemTip = "This is a green block";
        }
    }
}
