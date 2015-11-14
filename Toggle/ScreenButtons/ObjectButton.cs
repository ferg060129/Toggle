using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Toggle
{
    class ObjectButton : ScreenButton
    {
        Texture2D goodGraphic, badGraphic;
        private bool state = true;
        private string goodDescription;
        private string badDescription;
        public ObjectButton(int xLoc, int yLoc, string gGraphic, string bGraphic, string goodDesc, string badDesc)
        {
            goodGraphic = Textures.textures[gGraphic];
            badGraphic = Textures.textures[bGraphic];
            x = xLoc;
            y = yLoc;
            
            imageBoundingRectangle = new Rectangle(0,0,32,32);
            clickBox = new Rectangle(x, y, goodGraphic.Width, goodGraphic.Height);
        }

        public override Texture2D getGraphic()
        {
            if(state)
            {
                return goodGraphic;
            }
            return badGraphic;
        }

        public string getDescription()
        {
            if(state)
            {
                return goodDescription;
            }
            return badDescription;
        }


    }
}
