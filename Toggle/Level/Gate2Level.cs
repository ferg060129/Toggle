using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class Gate2Level : Level
    {
        public Gate2Level()
            : base()
        {
            map = "gate2Level.txt";
            playerStartingX = 2 * 32;
            playerStartingY = 4 * 32;
        }
        public override void loadLevelObjects()
        {
            Gate myGate = new Gate(12 * 32, 9 * 32);
            Game1.miscObjects.Add(myGate);
            Button myButton = new Button(12 * 32, 12 * 32, myGate);
            Game1.miscObjects.Add(myButton);
            VineMoveBlock vb = new VineMoveBlock(7 * 32, 4 * 32);
            Game1.miscObjects.Add(vb);
            vb = new VineMoveBlock(7 * 32, 9 * 32);
            Game1.miscObjects.Add(vb);
            vb = new VineMoveBlock(15 * 32, 9 * 32);
            Game1.miscObjects.Add(vb);
            vb = new VineMoveBlock(14 * 32, 8 * 32);
            Game1.miscObjects.Add(vb);
            vb = new VineMoveBlock(18 * 32, 2 * 32);
            Game1.miscObjects.Add(vb);
            //mook
        }
    }
}
