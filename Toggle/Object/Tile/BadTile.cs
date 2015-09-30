using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class BadTile : Tile
    {
        public BadTile(int xLocation, int yLocation, bool initialState, String gGraphic, String bGraphic)
            : base(xLocation, yLocation, initialState, gGraphic, bGraphic)
        {
        }
    }
}
