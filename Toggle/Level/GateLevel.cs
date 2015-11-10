using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Toggle
{
    class GateLevel : Level
    {
        public GateLevel()
            : base()
        {
            map = "gatelevel.txt";
            playerStartingX = 21 * 32;
            playerStartingY = 21 * 32;
            playerStartLocation = new Point(playerStartingX, playerStartingY);
        }
        public override void loadLevelObjects()
        {
            Gate myGate = new Gate(32 * 22, 32 * 11);
            Game1.miscObjects.Add(myGate);
            Button myButton = new Button(32 * 24, 32 * 12, myGate);
            Game1.miscObjects.Add(myButton);
            VineMoveBlock block = new VineMoveBlock(32 * 23, 32 * 15);
            Game1.miscObjects.Add(block);
            block = new VineMoveBlock(32 * 22, 32 * 15);
            Game1.miscObjects.Add(block);
        }
    }
}
