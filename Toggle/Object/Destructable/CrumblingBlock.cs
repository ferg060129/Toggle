using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Toggle
{
    class CrumblingBlock : Destructable
    {
        public CrumblingBlock(int xLocation, int yLocation) : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["crumblingblock"];
            badGraphic = Textures.textures["crumblingblock"];
            width = 32;
            height = 32;
            imageBoundingRectangle = new Rectangle(0, 0, width, height);
            hitBox = new Rectangle(xLocation, yLocation, width, height);
        }
    }
}
