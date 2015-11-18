using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Toggle
{
    class StartScreenButton : ScreenButton
    {
        private Texture2D graphic;
        private Texture2D graphicHover;
        private string action;
        public StartScreenButton(int xLoc, int yLoc, string gra1, string gra2, string actionin)
        {
            graphic = Textures.textures[gra1];
            graphicHover = Textures.textures[gra2];
            x = xLoc;
            y = yLoc;
            action = actionin;
            imageBoundingRectangle = new Rectangle(0, 0, graphic.Width, graphic.Height);
            clickBox = new Rectangle(x, y, graphic.Width, graphic.Height);
        }

        public override Texture2D getGraphic()
        {
            if(isHovered)
            {
                return graphicHover;
            }
            return graphic;
        }

        public new string onClick()
        {
            return action;
        }

        public override void onHover()
        {
            setHover(true);
            //play sound?
        }


    }
}
