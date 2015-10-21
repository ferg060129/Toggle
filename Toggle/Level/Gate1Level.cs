using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class Gate1Level : Level
    {
        public Gate1Level()
            : base()
        {
            map = "gate1level.txt";
            playerStartingX = 20 * 32;
            playerStartingY = 10 * 32;
        }
        public override void loadLevelObjects()
        {
            Gate myGate = new Gate(32 * 2, 32 * 6);
            Game1.miscObjects.Add(myGate);
            Button myButton = new Button(32 * 14, 32 * 13, myGate);
            Game1.miscObjects.Add(myButton);
            FlowerTentacles ft = new FlowerTentacles(7 * 32, 4 * 32);
            ft.setDefendTileGood(7, 4);
            ft.setDefendTileBad(7, 12);
            Game1.creatures.Add(ft);
            ft = new FlowerTentacles(14 * 32, 4 * 32);
            ft.setDefendTileGood(14, 4);
            ft.setDefendTileBad(14, 12);
            Game1.creatures.Add(ft);
            Strawberry sb = new Strawberry(9 * 32, 7 * 32);
            Game1.miscObjects.Add(sb);
            sb = new Strawberry(10 * 32, 7 * 32);
            Game1.miscObjects.Add(sb);
            sb = new Strawberry(11 * 32, 7 * 32);
            Game1.miscObjects.Add(sb);
            sb = new Strawberry(12 * 32, 7 * 32);
            Game1.miscObjects.Add(sb);
            //level tiles
            levelTiles.Add(new LevelTile(1 * 32, 1 * 32, "blackBlock", "blackBlock", "gate2Level"));
        }
    }
}
