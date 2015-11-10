using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace Toggle
{
    class ChalkboardTop : Miscellanious
    {
        public ChalkboardTop(int xLocation, int yLocation)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["chalkboardtop"];
            badGraphic = Textures.textures["chalkboardtop"];
            width = 32 * 4;
            height = 33;
            imageBoundingRectangle = new Rectangle(0, 0, width, height);
            hitBox = new Rectangle(xLocation, yLocation, width, height);
        }

    }
}
