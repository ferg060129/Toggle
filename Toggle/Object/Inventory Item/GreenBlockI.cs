using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle 
{
    class GreenBlockI : InventoryItem
    {
        public GreenBlockI(bool initialState) : base(initialState){
            goodGraphic = Textures.textures["greenblock"];
            badGraphic = Textures.textures["badgreenblock"];
            width = 32;
            height = 32;
        }
    }
}
