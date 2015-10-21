using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        }
        public override void loadLevelObjects()
        {
            Gate myGate = new Gate(32 * 22, 32 * 11);
            Game1.miscObjects.Add(myGate);
            Button myButton = new Button(32 * 24, 32 * 14, myGate);
            Game1.miscObjects.Add(myButton);
            VineMoveBlock block = new VineMoveBlock(32 * 23, 32 * 15);
            Game1.miscObjects.Add(block);
            block = new VineMoveBlock(32 * 22, 32 * 15);
            Game1.miscObjects.Add(block);
        }
    }
}
