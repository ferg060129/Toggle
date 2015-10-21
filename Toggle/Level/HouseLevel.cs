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
            playerStartingX = 5 * 32;
            playerStartingY = 3 * 32;
        }


        public override void loadLevelObjects()
        {
            FlowerTentacles ft = new FlowerTentacles(32 * 11, 32 * 4);
            Game1.creatures.Add(ft);
            ft.setDefendTileGood(11, 4);
            ft.setDefendTileBad(11, 7);

            ft = new FlowerTentacles(32 * 12, 32 * 4);
            Game1.creatures.Add(ft);
            ft.setDefendTileGood(12, 4);
            ft.setDefendTileBad(12, 6);

            ft = new FlowerTentacles(32 * 12, 32 * 11);
            Game1.creatures.Add(ft);
            ft.setDefendTileGood(12, 11);
            ft.setDefendTileBad(13, 7);

            ft = new FlowerTentacles(32 * 11, 32 * 11);
            Game1.creatures.Add(ft);
            ft.setDefendTileGood(11, 11);
            ft.setDefendTileBad(12, 8);
            
            //ft = new FlowerTentacles(500, 400, worldState);
            //creatures.Add(ft);

            BatteryGoo battery = new BatteryGoo(32 * 2, 32 * 2);
            Game1.items.Add(battery);

            GreenBlock b = new GreenBlock(32 * 12, 32 * 7);
            Game1.items.Add(b);
            VineMoveBlock vm = new VineMoveBlock(32 * 17, 32 * 8);
            Game1.miscObjects.Add(vm);
            vm = new VineMoveBlock(32 * 18, 32 * 8);
            Game1.miscObjects.Add(vm);
            vm = new VineMoveBlock(32 * 51, 32 * 22);
            Game1.miscObjects.Add(vm);
            vm = new VineMoveBlock(32 * 52, 32 * 23);
            Game1.miscObjects.Add(vm);
            vm = new VineMoveBlock(32 * 53, 32 * 24);
            Game1.miscObjects.Add(vm);
            vm = new VineMoveBlock(32 * 54, 32 * 25);
            Game1.miscObjects.Add(vm);
            Strawberry sb = new Strawberry(32 * 8, 32 * 3);
            Game1.miscObjects.Add(sb);
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
            int startY = 22;
            int endX = 54;
            int endY = 36;
         

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
