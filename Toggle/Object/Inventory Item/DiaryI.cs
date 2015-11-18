using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class DiaryI : InventoryItem
    {
        public DiaryI()
            : base()
        {
            goodGraphic = Textures.textures["diary"];
            badGraphic = Textures.textures["diaryBad"];
            width = 32;
            height = 32;
            itemTipGood = "So many words.";
            itemTipBad = "Mortem.";
        }
    }
}
