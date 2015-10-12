using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class VineMoveBlock : Pushable
    {
        public VineMoveBlock(int xLocation, int yLocation) : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["moveableVineBlock"];
            badGraphic = Textures.textures["vineBlock"];
        }
    }
}
