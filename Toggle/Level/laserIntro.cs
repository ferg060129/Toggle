using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Toggle
{
    class LaserIntro : Level
    {
        public LaserIntro()
            : base()
        {
            map = "laserIntro.txt";
            playerStartingX = 12 * 32;
            playerStartingY = 10 * 32;
            playerStartLocation = new Point(playerStartingX, playerStartingY);
        }
        public override void loadLevelObjects()
        {
            //level tiles
            levelTiles.Add(new LevelTile(5 * 32, 5 * 32, "blackBlock", "blackBlock", "hubLevel",new Point(9 * 32, 9 * 32)));
            levelTiles.Add(new LevelTile(27 * 32, 25 * 32, "blackBlock", "blackBlock", "hubLevel", new Point(9 * 32, 9 * 32)));
        }
    }
}
