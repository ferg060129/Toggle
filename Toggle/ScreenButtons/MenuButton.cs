using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Toggle
{
    class MenuButton : ScreenButton
    {
        Texture2D goodGraphic, badGraphic;
        private bool state = true;
        private string action;
        public MenuButton(int xLoc, int yLoc, string graphic, string actionin )
        {
            goodGraphic = Textures.textures[graphic];
            x = xLoc;
            y = yLoc;
            action = actionin;
            imageBoundingRectangle = new Rectangle(0, 0, goodGraphic.Width, goodGraphic.Height);
            clickBox = new Rectangle(x, y, goodGraphic.Width, goodGraphic.Height);
        }

        public override Texture2D getGraphic()
        {
            return goodGraphic;
        }

        public new string onClick()
        {
            return action;
        }
    }
}
