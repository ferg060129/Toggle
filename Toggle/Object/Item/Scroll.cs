using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Toggle
{
    class Scroll : Item 
    {
        private string itemTipGood;
        private string itemTipBad;
        public Scroll(int xLocation, int yLocation, string tipG, string tipB): base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["itemblock"];
            badGraphic = Textures.textures["baditemblock"];
            width = 32;
            height = 32;
            imageBoundingRectangle = new Rectangle(0, 0, width, height);
            hitBox = new Rectangle(xLocation, yLocation, width, height);

            tipG = itemTipGood;
            tipB = itemTipBad;
        }

        public override void makeInventoryItem()
        {
            inventoryItem = new ScrollI(itemTipGood, itemTipBad);
        }


    }
}
