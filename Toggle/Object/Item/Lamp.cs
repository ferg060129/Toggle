using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Toggle
{
    class Lamp : Item
    {
        bool batteries = false;
        public Lamp(int xLocation, int yLocation): base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["UnlitLantern"];
            badGraphic = Textures.textures["BustedLantern"];
            width = 32;
            height = 32;
            imageBoundingRectangle = new Rectangle(0, 0, width, height);
            hitBox = new Rectangle(xLocation, yLocation, width, height);
        }

        public override void makeInventoryItem()
        {
            inventoryItem = new LampI();
        }

        public bool hasBatteries()
        {
            return batteries;
        }

        public void setBatteries(bool b)
        {
            batteries = b;
            if(b)
            {
                goodGraphic = Textures.textures["LitLantern"];
                badGraphic = Textures.textures["LitLantern"];
            }
            else
            {
                goodGraphic = Textures.textures["UnlitLantern"];
                badGraphic = Textures.textures["BustedLantern"];
            }
            
        }

        public override InventoryItem pickUpItem()
        {
            return new LampI();
        }
    }
}
