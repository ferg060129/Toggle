using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class LevelTile : Tile
    {
        string level;
        int enterDirection;
        public LevelTile(int xLocation, int yLocation, String gGraphic, String bGraphic, string level, int enterDirection)
            : base(xLocation, yLocation, gGraphic, bGraphic)
        {
            this.level = level;
            this.enterDirection = enterDirection;
        }




        public int getEnterDirection()
        {
            return enterDirection;
        }

        public string getLevel()
        {
            return level;
        }

    }
}
