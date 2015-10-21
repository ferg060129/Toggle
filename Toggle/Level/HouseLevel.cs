using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Toggle
{
    class HouseLevel : Level
    {
        public HouseLevel() : base()
        {
            map = "home.txt";
            playerStartingX = 24 * 32;
            playerStartingY = 16 * 32;
        }


        public override void loadLevelObjects()
        {
            
            BatteryGoo battery = new BatteryGoo(32 * 5, 32 * 3);
            Game1.items.Add(battery);

            Strawberry sb = new Strawberry(32 * 8, 32 * 3);
            Game1.miscObjects.Add(sb);

            FlowerTentacles ft = new FlowerTentacles(2 * 32, 8 * 32);
            Game1.creatures.Add(ft);
            ft.setDefendTileGood(2, 8);
            ft.setDefendTileBad(3, 9);

            ft = new FlowerTentacles(1 * 32, 9 * 32);
            Game1.creatures.Add(ft);
            ft.setDefendTileGood(1, 9);
            ft.setDefendTileBad(2, 10);


            ft = new FlowerTentacles(8 * 32, 8 * 32);
            Game1.creatures.Add(ft);
            ft.setDefendTileGood(7, 11);
            ft.setDefendTileBad(8, 8);

            ft = new FlowerTentacles(10 * 32, 9 * 32);
            Game1.creatures.Add(ft);
            ft.setDefendTileGood(17, 8);
            ft.setDefendTileBad(10, 8);

            ft = new FlowerTentacles(9 * 32, 9 * 32);
            Game1.creatures.Add(ft);
            ft.setDefendTileGood(13, 12);
            ft.setDefendTileBad(9, 9);


            Gate myGate = new Gate(32 * 3, 32 * 15);
            Game1.miscObjects.Add(myGate);
            Button myButton = new Button(32 * 9, 32 * 8, myGate);
            Game1.miscObjects.Add(myButton);

            Lamp mylamp = new Lamp(32 * 1, 32 * 8);
            Game1.items.Add(mylamp);

            GreenBlock gb = new GreenBlock(32 * 48, 32 * 11);
            Game1.items.Add(gb);


            /*
            DogBoogieman dbm = new DogBoogieman(32 * 45, 32 * 30);
            Game1.creatures.Add(dbm);
            dbm.setDefendTileGood(45, 30);
            dbm.setAttackTarget(ref player);*/
           // vm = new VineMoveBlock(32 * 18, 32 * 8);
           // Game1.miscObjects.Add(vm);

        }

        public override void addDarkTiles()
        {
            //int darkZone
            int startX = 39;
            int startY = 1;
            int endX = 54;
            int endY = 16;
         

            for(int x = startX; x <= endX; x++)
            {
                for(int y = startY; y <= endY; y++)
                {
                    DarkTile dt = new DarkTile(x*32, y*32, "blackBlock","blackBlock");
                    Game1.darkTiles.Add(dt);
                }
            }

        }

    }
}
