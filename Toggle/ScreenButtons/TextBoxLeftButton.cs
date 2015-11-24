using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Toggle
{
    class TextBoxLeftButton : ScreenButton
    {
        Texture2D graphic, graphicHover;
        private bool state = true;
        TextBoxScreen myScreen;
        public TextBoxLeftButton(int xLoc, int yLoc, string gra1, string gra2,TextBoxScreen tbs)
        {
            graphic = Textures.textures[gra1];
            graphicHover = Textures.textures[gra2];
            x = xLoc;
            y = yLoc;
            imageBoundingRectangle = new Rectangle(0, 0, graphic.Width, graphic.Height);
            clickBox = new Rectangle(x, y, graphic.Width, graphic.Height);
            myScreen = tbs;
        }

        public override Texture2D getGraphic()
        {
            if (isHovered)
            {
                return graphicHover;
            }
            return graphic;
        }

        public override void onClick()
        {
            myScreen.previousString();
        }

        public override void onHover()
        {
            if (!isHovered)
            {
                setHover(true);
            }
        }

    }
}
