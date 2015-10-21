using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class Complex1 : Level
    {
        public Complex1()
            : base()
        {
            map = "complex1.txt";
            playerStartingX = 2 * 32;
            playerStartingY = 4 * 32;
        }
        public override void loadLevelObjects()
        {
            Gate myGate = new Gate(32 * 1, 32 * 3);
            Game1.miscObjects.Add(myGate);
            Button myButton = new Button(32 * 14, 32 * 13, myGate);
            Game1.miscObjects.Add(myButton);
            Strawberry sb = new Strawberry(9 * 32, 7 * 32);
            Game1.miscObjects.Add(sb);
            sb = new Strawberry(10 * 32, 7 * 32);
            Game1.miscObjects.Add(sb);
            sb = new Strawberry(11 * 32, 7 * 32);
            Game1.miscObjects.Add(sb);
            sb = new Strawberry(12 * 32, 7 * 32);
            Game1.miscObjects.Add(sb);

        }
    }
}
