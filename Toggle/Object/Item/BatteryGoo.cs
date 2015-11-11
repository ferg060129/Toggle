using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Toggle
{
    class BatteryGoo : Item
    {
        int ctr = 0;
        int image = 0;

        Texture2D goo0 = Textures.textures["GoopFrame1"];
        Texture2D goo1 = Textures.textures["GoopFrame2"];
        Texture2D goo2 = Textures.textures["GoopFrame3"];
        public BatteryGoo(int xLocation, int yLocation): base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["battery"];
            badGraphic = Textures.textures["GoopFrame1"];
            width = 32;
            height = 32;
            imageBoundingRectangle = new Rectangle(0, 0, width, height);
            hitBox = new Rectangle(xLocation, yLocation, width, height);
        }

        public override void makeInventoryItem()
        {
            inventoryItem = new BatteryGooI(this);
        }

        public override Texture2D getGraphic()
        {
            if (state)
                return goodGraphic;
            else
            {
                if(ctr >= 10)
                {
                    switch(image)
                    {
                        case 0:
                            image = 1;
                            badGraphic = goo1;
                            break;
                        case 1:
                            image = 2;
                            badGraphic = goo2;
                            break;
                        case 2:
                            image = 0;
                            badGraphic = goo0;
                            break;
                        default:
                            break;
                    }
                    ctr = 0;
                }
                ctr++;
                return badGraphic;


            }
                
        }

    }
}
