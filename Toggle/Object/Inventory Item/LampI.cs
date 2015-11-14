using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Toggle
{
    class LampI : InventoryItem
    {


        public LampI(Item i)
            : base(i)
        {
            goodGraphic = Textures.textures["UnlitLantern"];
            badGraphic = Textures.textures["BustedLantern"];
            width = 32;
            height = 32;
            itemTipGood = "Batteries not included";
            itemTipBad = "Batteries not included";
        }


        public override bool combineItems(InventoryItem i)
        {
            if(i is BatteryGooI && i.getState())
            {
                ((Lamp)myItem).setBatteries(true);
                itemTipGood = "I am bright as the sun";
                itemTipBad = "I am bright as the sun";
                //goodGraphic = Textures.textures["LitLantern"];
                //badGraphic = Textures.textures["LitLantern"];
                return true;
            }
            return false;
        }

        public override Texture2D getGraphic()
        {
            if(((Lamp)myItem).hasBatteries())
            {
                return Textures.textures["LitLantern"];
            }
            if (state)
            {

                return goodGraphic;
            }
                
            else
                return badGraphic;
        }

        
    }
 
}
