using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class LockTile : Tile
    {
        public LockTile(int xLocation, int yLocation, bool initialState, String gGraphic, String bGraphic)
            : base(xLocation, yLocation, initialState, gGraphic, bGraphic)
        {
        }
    }
}
