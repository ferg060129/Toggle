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
            Gate myGate = new Gate(2 * 32, 4 * 32);
            Game1.miscObjects.Add(myGate);
            ChalkboardTop ch = new ChalkboardTop(5 * 32, 0 * 32, myGate);
            Game1.miscObjects.Add(ch);

            Gate gate2 = new Gate(2 * 32, 16 * 32);
            Game1.miscObjects.Add(gate2);
            Button myButton = new Button(32 * 2, 32 * 15, gate2);
            myButton.setHeavyButton(true);
            Game1.miscObjects.Add(myButton);

            Gate gate3 = new Gate(16 * 32, 21 * 32);
            Game1.miscObjects.Add(gate3);
            myButton = new Button(32 * 1, 32 * 21, gate3);
            Game1.miscObjects.Add(myButton);

            Game1.miscObjects.Add(new VineMoveBlock(32 * 3, 32 * 17));
            Game1.miscObjects.Add(new VineMoveBlock(32 * 3, 32 * 18));
            

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


            ft = new FlowerTentacles(32 * 3, 32 * 24);
            Game1.creatures.Add(ft);
            ft.setDefendTileGood(3, 24);
            ft.setDefendTileBad(3, 21);


            for (int i = 38; i <= 59; i++ )
            {
                for(int j = 15; j<=24; j++)
                {
                    if(i!=59 || j!=24)
                    {
                        Game1.miscObjects.Add(new Strawberry(i * 32, j * 32));
                    }
                }
            }


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
                levelTiles.Add(new LevelTile(34 * 32, 25 * 32, "blackBlock", "blackBlock", "hubLevel", new Point(9 * 32, 9 * 32)));
            
        }

        public override void addInitialLevelItems(){
            levelItems.Clear();
            //Scroll b = new Scroll(32 * 12, 32 * 7, "Bananaphone", "ring ring ring");
            //levelItems.Add(b);
            levelItems.Add(new Scroll(32 * 15, 32 * 24, "banana", "notbanana"));
            levelItems.Add(new Scroll(32 * 59, 32 * 24, "banana", "notbanana"));
        }
    }
}
