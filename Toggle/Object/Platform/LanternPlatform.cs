using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class LanternPlatform : Platform
    {
        public LanternPlatform(int xLocation, int yLocation)
            : base(xLocation, yLocation)
        {
          itemType = new Lamp(0, 0).GetType();
          goodGraphic = Textures.textures["lanternPlatform"];
          badGraphic = Textures.textures["lanternPlatform"];
        }
    }
}
