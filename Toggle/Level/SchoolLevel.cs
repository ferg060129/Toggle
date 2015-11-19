using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Toggle
{
    class SchoolLevel : Level
    {

        public SchoolLevel()
            : base()
        {
            map = "school.txt";
            indoors = true;
            playerStartingX = 15*32;
            playerStartingY = 16*32;
            playerStartLocation = new Point(playerStartingX, playerStartingY);
        }


        public override void loadLevelObjects()
        {
            for(int i = 3; i <= 9; i+=2)
            {
                for(int j = 5; j<= 7; j+=2)
                {
                    Desk d = new Desk(i * 32, j * 32);
                    Game1.miscObjects.Add(d);
                }
            }
            //laser puzzle objects
            Gate laserGate = new Gate(55 * 32, 5 * 32);
            Button bt = new Button(50 * 32, 5 * 32, laserGate);
            bt.setHeavyButton(true);
            Game1.miscObjects.Add(bt);
            Game1.miscObjects.Add(laserGate);


            Gate gate4 = new Gate(40 * 32, 23 * 32);
            Button bt2 = new Button(52 * 32, 16 * 32, gate4);
            bt2.setHeavyButton(true);
            Game1.miscObjects.Add(bt2);
            Game1.miscObjects.Add(gate4);



            bool lasDir = false;
            for (int i = 21; i < 59; i ++)
            {
                for (int j = 2; j < 9; j ++)
                {
                    if (((i % 12) == 0) && ((j - 3) % 4 == 0))
                    {
                        Game1.miscObjects.Add(new LaserBlock(i * 32, j * 32, lasDir));
                        lasDir = !lasDir;
                    }
                }
                if ((i % 12) == 0)
                {
                    lasDir = !lasDir;
                }
            }
            Game1.miscObjects.Add(new LaserBlock(49 * 32, 5 * 32,true));
            Game1.miscObjects.Add(new VineMoveBlock(30 * 32, 5 * 32));
            //end laser puzzle
            Gate myGate = new Gate(2 * 32, 4 * 32);
            Game1.miscObjects.Add(myGate);
            ChalkboardTop ch = new ChalkboardTop(5 * 32, 0 * 32, myGate);
            Game1.miscObjects.Add(ch);

            Gate gate2 = new Gate(2 * 32, 17 * 32);
            Game1.miscObjects.Add(gate2);
            Button myButton = new Button(32 * 2, 32 * 16, gate2);
            myButton.setHeavyButton(true);
            Game1.miscObjects.Add(myButton);

            Gate gate3 = new Gate(16 * 32, 22 * 32);
            Game1.miscObjects.Add(gate3);
            myButton = new Button(32 * 1, 32 * 22, gate3);
            Game1.miscObjects.Add(myButton);

            Game1.miscObjects.Add(new VineMoveBlock(32 * 3, 32 * 18));
            Game1.miscObjects.Add(new VineMoveBlock(32 * 3, 32 * 19));
            Game1.miscObjects.Add(new VineMoveBlock(32 * 10, 32 * 3));
            

            FlowerTentacles ft = new FlowerTentacles(32 * 4, 32 * 6);
            Game1.creatures.Add(ft);
            ft.setDefendTileGood(4, 6);
            ft.setDefendTileBad(3, 4);

            ft = new FlowerTentacles(32 * 6, 32 * 6);
            Game1.creatures.Add(ft);
            ft.setDefendTileGood(6, 6);
            ft.setDefendTileBad(9, 3);

            ft = new FlowerTentacles(32 * 8, 32 * 6);
            Game1.creatures.Add(ft);
            ft.setDefendTileGood(8, 6);
            ft.setDefendTileBad(10, 3);


            ft = new FlowerTentacles(32 * 3, 32 * 25);
            Game1.creatures.Add(ft);
            ft.setDefendTileGood(3, 25);
            ft.setDefendTileBad(3, 22);


        
            Game1.miscObjects.Add(new Strawberry(40 * 32,19 * 32));
            //Game1.miscObjects.Add(new Strawberry(44 * 32, 19 * 32));


            Game1.miscObjects.Add(new VineMoveBlock(44 * 32, 23 * 32));
            Game1.miscObjects.Add(new VineMoveBlock(48 * 32, 23 * 32));
            Game1.miscObjects.Add(new VineMoveBlock(56 * 32, 19 * 32));
            Game1.miscObjects.Add(new VineMoveBlock(56 * 32, 15 * 32));
            Game1.miscObjects.Add(new VineMoveBlock(48 * 32, 19 * 32));
                /*
                ft = new FlowerTentacles(32 * 11, 32 * 11);
                Game1.creatures.Add(ft);
                ft.setDefendTileGood(11, 11);
                ft.setDefendTileBad(12, 8);

                //ft = new FlowerTentacles(500, 400, worldState);
                //creatures.Add(ft);

                VineMoveBlock vm = new VineMoveBlock(32 * 10, 32 * 5);
                Game1.miscObjects.Add(vm);
                */

                //leveltiles
                levelTiles.Add(new LevelTile(34 * 32, 26 * 32, "blackBlock", "blackBlock", "hubLevel", new Point(35 * 32, 9 * 32)));
            
        }

        public override void addInitialLevelItems(){
            levelItems.Clear();
            //Scroll b = new Scroll(32 * 12, 32 * 7, "Bananaphone", "ring ring ring");
            //levelItems.Add(b);
            levelItems.Add(new Scroll(32 * 15, 32 * 25, "The word you seek has eight letters", "Unscramble the word demurrer"));
            levelItems.Add(new Scroll(32 * 56, 32 * 18, "Three syllables", "The third is -er"));
            levelItems.Add(new Scroll(32 * 58, 32 * 5, "It all starts with m", "Don't let the r's touch"));
            levelItems.Add(new Knife(32 * 1, 32 * 8));
            levelItems.Add(new Rope(32 * 1, 32 * 17));
        }
    }
}
