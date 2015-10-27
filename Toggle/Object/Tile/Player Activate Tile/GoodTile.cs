using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class GoodTile : PlayerActivateTile
    {
        public GoodTile(int xLocation, int yLocation, String gGraphic, String bGraphic)
            : base(xLocation, yLocation, gGraphic, bGraphic)
        {
        }
    }
}
