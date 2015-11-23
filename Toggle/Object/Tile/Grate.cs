using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class Grate : Wall
    {
        public Grate(int xLocation, int yLocation, String gGraphic, String bGraphic)
            : base(xLocation, yLocation, gGraphic, bGraphic)
        {
            isSolid = true;
            projectileBlocks = false;
        }

    }
}
