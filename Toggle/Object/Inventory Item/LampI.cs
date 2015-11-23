using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Toggle
{
    class LampI : InventoryItem
    {

        private bool batteries = false;
        private bool batteriesBeforeReset = false;
        public LampI()
            : base()
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
                batteries = true;
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
            if (batteries)
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

        public bool hasBatteries()
        {
            return batteries;
        }

        public void setBatteries(bool b)
        {
            batteries = b;
        }

        public bool hadBatteriesBeforeReset()
        {
            return batteriesBeforeReset;
        }

        public void setBatteriesBeforeReset(bool b)
        {
            batteriesBeforeReset = b;
        }

    }
 
}
