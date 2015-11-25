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
            Game1.miscObjects.Add(new VineMoveBlock(20 * 32, 20 * 32));
            Gate daGate = new Gate(22 * 32, 20 * 32);
            Game1.miscObjects.Add(daGate);
            Game1.miscObjects.Add(new ButtonShadow(22 * 32, 25 * 32,daGate,true));
            Game1.miscObjects.Add(new LaserBlock(26 * 32, 20 * 32, true));

            levelTiles.Add(new LevelTile(15 * 32, 8 * 32, "blackBlock", "blackBlock", "hubLevel", new Point(19 * 32, 7 * 32)));
        }

    }
}
