using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Toggle
{
    class GhostTestLevel : Level
    {

        public GhostTestLevel()
            : base()
        {
            map = "testingLevel.txt";
            playerStartingX = 9 * 32;
            playerStartingY = 9 * 32;
            playerStartLocation = new Point(playerStartingX, playerStartingY);
        }
        public override void loadLevelObjects()
        {
            Game1.miscObjects.Add(new VineMoveBlock(32 * 15, 32 * 10));
            Game1.miscObjects.Add(new VineMoveBlock(32 * 10, 32 * 15));
            Game1.miscObjects.Add(new VineMoveBlock(32 * 5, 32 * 10));
            Game1.miscObjects.Add(new VineMoveBlock(32 * 10, 32 * 5));
            Game1.creatures.Add(new Ghost(32 * 13, 32 * 3, new Point(1, 1), new Point(54, 5)));

            levelTiles.Add(new LevelTile(15 * 32, 8 * 32, "blackBlock", "blackBlock", "hubLevel", new Point(19 * 32, 7 * 32)));
        }

    }
}
