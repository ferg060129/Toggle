using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


//This level has the item you want
namespace Toggle
{
    class MarshFinalLevel : Level
    {

        public MarshFinalLevel()
            : base()
        {
            map = "marshFinal.txt";
            playerStartingX = 9 * 32;
            playerStartingY = 9 * 32;
            playerStartLocation = new Point(playerStartingX, playerStartingY);
        }
        public override void loadLevelObjects()
        {
            //next level
            levelTiles.Add(new LevelTile(10 * 32, 12 * 32, "blackBlock", "blackBlock", "hubLevel", new Point(34 * 32, 20 * 32)));
            //previous
            //levelTiles.Add(new LevelTile(8 * 32, 48 * 32, "blackBlock", "blackBlock", "marsh2Level", new Point(21 * 32, 13 * 32)));
        }

        public override void addInitialLevelItems()
        {
            levelItems.Clear();
            levelItems.Add(new Rose(10 * 32, 15 * 32));
        }
    }
}
