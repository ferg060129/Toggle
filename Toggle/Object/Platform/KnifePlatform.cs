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
            itemType = new KnifeI().GetType();
            goodGraphic = Textures.textures["knifePlatform"];
            badGraphic = Textures.textures["knifePlatform"];
        }

        public override void changePlatformGraphic(bool b)
        {
            if(b)
            {
                goodGraphic = Textures.textures["knifePlatformComplete"];
                badGraphic = Textures.textures["knifePlatformComplete"];
            }
            else
            {
                goodGraphic = Textures.textures["knifePlatform"];
                badGraphic = Textures.textures["knifePlatform"];
            }
        }
    }
}
