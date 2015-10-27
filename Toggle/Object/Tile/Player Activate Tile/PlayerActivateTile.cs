using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Toggle
{
    class PlayerActivateTile : Tile
    {
        protected bool pressed;
        public PlayerActivateTile(int xLocation, int yLocation, String gGraphic, String bGraphic)
            : base(xLocation, yLocation, gGraphic, bGraphic)
        {



        }

        public override Texture2D getGraphic()
        {
            if (pressed)
                return goodGraphic;
            else
                return badGraphic;
        }

        public void setPressed(bool b)
        {
            pressed = b;
        }
    }
}
