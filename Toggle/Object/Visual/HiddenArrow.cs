using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Toggle
{
    class HiddenArrow : Visual
    {

        public HiddenArrow(int xLocation, int yLocation)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["nothing"];
            badGraphic = Textures.textures["hiddenArrow"];
            imageBoundingRectangle = new Rectangle(0, 0, 32, 32);
        }
    }
}
