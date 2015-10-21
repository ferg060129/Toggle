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
            playerStartingX = 12 * 32;
            playerStartingY = 10 * 32;
        }
        public override void loadLevelObjects()
        {
            //gates and buttons
            Gate myGate = new Gate(10 * 32, 7 * 32);
            Game1.miscObjects.Add(myGate);
            Button myButton = new Button(12 * 32, 12 * 32, myGate);
            myButton.setHeavyButton(true);
            Game1.miscObjects.Add(myButton);
            myGate = new Gate(13 * 32, 7 * 32);
            Game1.miscObjects.Add(myGate);
            myButton = new Button(12 * 32, 12 * 32, myGate);
            myButton.setHeavyButton(true);
            Game1.miscObjects.Add(myButton);
            //blocks
            VineMoveBlock vb = new VineMoveBlock(7 * 32, 4 * 32);
            vb = new VineMoveBlock(10 * 32, 9 * 32);
            Game1.miscObjects.Add(vb);
            vb = new VineMoveBlock(10 * 32, 8 * 32);
            Game1.miscObjects.Add(vb);
            vb = new VineMoveBlock(10 * 32, 6 * 32);
            Game1.miscObjects.Add(vb);
            vb = new VineMoveBlock(10 * 32, 5 * 32);
            Game1.miscObjects.Add(vb);
            vb = new VineMoveBlock(9 * 32, 8 * 32);
            Game1.miscObjects.Add(vb);
            vb = new VineMoveBlock(9 * 32, 6 * 32);
            Game1.miscObjects.Add(vb);
            vb = new VineMoveBlock(13 * 32, 9 * 32);
            Game1.miscObjects.Add(vb);
            vb = new VineMoveBlock(13 * 32, 8 * 32);
            Game1.miscObjects.Add(vb);
            vb = new VineMoveBlock(13 * 32, 6 * 32);
            Game1.miscObjects.Add(vb);
            vb = new VineMoveBlock(13 * 32, 5 * 32);
            Game1.miscObjects.Add(vb);
            vb = new VineMoveBlock(14 * 32, 8 * 32);
            Game1.miscObjects.Add(vb);
            vb = new VineMoveBlock(14 * 32, 6 * 32);
            Game1.miscObjects.Add(vb);
            vb = new VineMoveBlock(11 * 32, 7 * 32);
            Game1.miscObjects.Add(vb);
            vb = new VineMoveBlock(12 * 32, 7 * 32);
            Game1.miscObjects.Add(vb);
            //level tiles
            levelTiles.Add(new LevelTile(12 * 32, 3 * 32, "blackBlock", "blackBlock", "complex1Level"));
        }
    }
}
