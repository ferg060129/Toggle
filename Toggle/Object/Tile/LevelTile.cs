using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class LevelTile : Tile
    {
        string level;
        public LevelTile(int xLocation, int yLocation, String gGraphic, String bGraphic, string level)
            : base(xLocation, yLocation, gGraphic, bGraphic)
        {
            this.level = level;
        }

        public string getLevel()
        {
            return level;
        }

    }
}
