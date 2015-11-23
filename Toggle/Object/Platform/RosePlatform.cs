using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class RosePlatform : Platform
    {

        public RosePlatform(int xLocation, int yLocation)
            : base(xLocation, yLocation)
        {
            itemType = new RoseI().GetType();
            goodGraphic = Textures.textures["rosePlatform"];
            badGraphic = Textures.textures["rosePlatform"];
        }

        public override void changePlatformGraphic(bool b)
        {
            if(b)
            {
                goodGraphic = Textures.textures["rosePlatformComplete"];
                badGraphic = Textures.textures["rosePlatformComplete"];
            }
            else
            {
                goodGraphic = Textures.textures["rosePlatform"];
                badGraphic = Textures.textures["rosePlatform"];
            }
        }
    }
}
