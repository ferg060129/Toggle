using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class KnifePlatform : Platform
    {
        
        public KnifePlatform(int xLocation, int yLocation)
            : base(xLocation, yLocation)
        {
            itemType = new Knife(0, 0).GetType();
            goodGraphic = Textures.textures["knifePlatform"];
            badGraphic = Textures.textures["knifePlatform"];
        }

    }
}
