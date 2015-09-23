using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Toggle
{
    class Tile : Object
    {

        public Tile(int xLocation, int yLocation, bool initialState) : base(xLocation, yLocation, initialState)
        {
            goodGraphic = Textures.textures["BigPicture"];
            badGraphic = Textures.textures["BigPicture"];
            width = 32;
            height = 32;
            imageBoundingRectangle = new Rectangle(64, 0, width, height);
            hitBox = new Rectangle(xLocation, yLocation, width, height);

        }

    }
}
