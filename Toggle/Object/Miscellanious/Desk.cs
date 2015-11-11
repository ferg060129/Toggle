using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class Desk : Miscellanious
    {
        public Desk(int xLocation, int yLocation)
            : base(xLocation, yLocation)
        {

            
            isSolid = true;
            collidable = true;
            goodGraphic = Textures.textures["desk"];
            badGraphic = Textures.textures["deskBad"];

        }



    }
}
