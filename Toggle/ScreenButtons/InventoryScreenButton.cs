using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Toggle
{
    class InventoryScreenButton : ScreenButton
    {
        Texture2D goodGraphic, badGraphic;
        private bool state = true;
        private string action;
        private Screen parentScreen;
        public InventoryScreenButton(int xLoc, int yLoc, string graphic, string actionin, Screen pS)
        {
            goodGraphic = Textures.textures[graphic];
            x = xLoc;
            y = yLoc;
            action = actionin;
            imageBoundingRectangle = new Rectangle(0, 0, goodGraphic.Width, goodGraphic.Height);
            clickBox = new Rectangle(x, y, goodGraphic.Width, goodGraphic.Height);
            parentScreen = pS;
        }

        public override Texture2D getGraphic()
        {
            return goodGraphic;
        }

        public new string onClick()
        {
            ((InventoryScreen)parentScreen).nextString();
            //Set parent screen to next thingy
            return action;
            
        }
    }
}
