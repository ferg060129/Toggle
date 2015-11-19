using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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
            Gate myGate = new Gate(12 * 32, 9 * 32);
            Game1.miscObjects.Add(myGate);
            Button myButton = new ButtonPlayer(12 * 32, 12 * 32, myGate);
            Game1.miscObjects.Add(myButton);
            Strawberry sb = new Strawberry(15 * 32, 8 * 32);
            Game1.miscObjects.Add(sb);
            sb = new Strawberry(3 * 32, 9 * 32);
            Game1.miscObjects.Add(sb);
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
            FlowerTentacles ft = new FlowerTentacles(9 * 32, 4 * 32);
            ft.setDefendTileGood(9, 4);
            ft.setDefendTileBad(16, 4);
            Game1.creatures.Add(ft);
            //level tiles
            levelTiles.Add(new LevelTile(23 * 32, 12 * 32, "blackBlock", "blackBlock", "hubLevel", new Point(26 * 32, 6 * 32)));
            //levelTiles.Add(new LevelTile(23 * 32, 12 * 32, "blackBlock", "blackBlock", "hubLevel", new Point(19 * 32, 7 * 32)));
        }
    }
}
