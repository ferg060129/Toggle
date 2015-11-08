using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Toggle
{
    class House : Visual
    {
        int ctr = 0;
        bool blink = false;
        Texture2D blinkGraphic = Textures.textures["housedarkNoEyes"];
        public House(int xLocation, int yLocation) : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["house"];
            badGraphic = Textures.textures["housedark"];
            imageBoundingRectangle = new Rectangle(0, 0, 224, 192);
        }

        public override Texture2D getGraphic()
        {
            ctr++;
            if (blink)
            {
                if(ctr >= 15)
                {
                    blink = false;
                    ctr = 0;
                }
            }
            else
            {
                if(ctr >= 200)
                {
                    blink = true;
                    ctr = 0;
                }
            }

            if (state)
                return goodGraphic;
            else if(blink)
            {
                return blinkGraphic;
            }
            else
            {
                return badGraphic;
            }
            
            
                
        }




    }
}
