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
            indoors = true;
            playerStartingX = 24 * 32;
            playerStartingY = 16 * 32;
            playerStartLocation = new Point(playerStartingX, playerStartingY);
        }

        public override void loadLevelObjects()
        {
            Gate myGate = new Gate(32 * 5, 32 * 10);
            Game1.miscObjects.Add(myGate);
            Button myButton = new Button(32 * 9, 32 * 8, myGate);
            Game1.miscObjects.Add(myButton);


            myGate = new Gate(32 * 51, 32 * 13);
            Game1.miscObjects.Add(myGate);

            myButton = new Button(32 * 52, 32 * 7, myGate);
            myButton.setHeavyButton(true);
            Game1.miscObjects.Add(myButton);

            myGate = new Gate(32 * 42, 32 * 15);
            Game1.miscObjects.Add(myGate);
            myButton = new Button(32 * 43, 32 * 15, myGate);
            myButton.setHeavyButton(true);
            Game1.miscObjects.Add(myButton);


           

            Strawberry sb = new Strawberry(32 * 53, 32 * 15);
            Game1.miscObjects.Add(sb);
            sb = new Strawberry(32 * 31, 32 * 7);
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


            




            
            DogBoogieman dbm = new DogBoogieman(32 * 20, 32 * 4, new Point(1,1), new Point(54,5));
            Game1.creatures.Add(dbm);
            dbm.setDefendTileGood(20, 4);
            
             //dbm.setAttackTarget(ref player);
            VineMoveBlock vm = new VineMoveBlock(32 * 41, 32 * 5);
            Game1.miscObjects.Add(vm);
            vm = new VineMoveBlock(32 * 51, 32 * 14);
            Game1.miscObjects.Add(vm);
            vm = new VineMoveBlock(32 * 52, 32 * 11);
            Game1.miscObjects.Add(vm);
            vm = new VineMoveBlock(32 * 52, 32 * 8);
            Game1.miscObjects.Add(vm);

            vm = new VineMoveBlock(32 * 6, 32 * 10);
            Game1.miscObjects.Add(vm);
            vm = new VineMoveBlock(32 * 20, 32 * 10);
            Game1.miscObjects.Add(vm);

            //leveltiles
            levelTiles.Add(new LevelTile(24 * 32, 17 * 32, "blackBlock", "blackBlock", "hubLevel", new Point(9 * 32, 9 * 32)));


            

        }
      

        public override void addDarkTiles()
        {
            //int darkZone
            int startX = 1;
            int startY = 1;
            int endX = 54;
            int endY = 5;

            

            for(int x = startX; x <= endX; x++)
            {
                for(int y = startY; y <= endY; y++)
                {
                    DarkTile dt = new DarkTile(x*32, y*32, "blackBlock","blackBlock");
                    Game1.darkTiles.Add(dt);
                }
            }

        }
         public override void addInitialLevelItems()
        {
            BatteryGoo battery = new BatteryGoo(32 * 53, 32 * 16);
            levelItems.Add(battery);

            Lamp mylamp = new Lamp(32 * 1, 32 * 8);
            levelItems.Add(mylamp);

            Scroll gb = new Scroll(32 * 42, 32 * 16, "cowboys", "aliens");
            levelItems.Add(gb);
        }   

    }
}
