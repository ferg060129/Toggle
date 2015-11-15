using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class Fence : Miscellanious
    {
        public Fence(int xLocation, int yLocation, string graphicName)
            : base(xLocation, yLocation)
        {


            isSolid = true;
            collidable = true;
            goodGraphic = Textures.textures["fence"];
            badGraphic = Textures.textures[graphicName];

        }



    }
}
