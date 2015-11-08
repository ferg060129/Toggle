using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Toggle
{
    class School : Visual
    {

        public School(int xLocation, int yLocation)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["school"];
            badGraphic = Textures.textures["schooldark"];
            imageBoundingRectangle = new Rectangle(0, 0, 320, 192);
        }
    }
}
