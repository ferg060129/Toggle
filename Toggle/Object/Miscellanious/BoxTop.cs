using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace Toggle
{
    class BoxTop : Miscellanious
    {
        Box box;
        public BoxTop(int xLocation, int yLocation)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["ItemBox"];
            badGraphic = Textures.textures["ItemBox"];
            box = new Box(0, 0);
        }
        public Box getBox()
        {
            return box;
        }


    }
}
