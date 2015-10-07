using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Collections;
namespace Toggle
{
  
    public class Game1 : Game
    {
        //banana world
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ArrayList creatures = new ArrayList();
        ArrayList items = new ArrayList();
        ArrayList tiles = new ArrayList();
        ArrayList collidableTiles = new ArrayList();
        int width;
        int height;
        float time;
        Player player;
        Camera cam;
        Song song;
        Song song2;
        Inventory inventory;
        KeyboardState newKeyBoardState, oldKeyBoardState;
        bool worldState = true;

        public static bool[,] wallArray;
        
        public Game1()
        {
            time = 0;
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            //graphics.PreferredBackBufferWidth = 1400;
            //graphics.PreferredBackBufferHeight = 800;
            //graphics.ApplyChanges();
          
        }

        protected override void Initialize()
        {   
            base.Initialize();
            //width = Window.ClientBounds.Width;
            //height = Window.ClientBounds.Height;
            
        }

        protected override void LoadContent()
        {
            width = GraphicsDevice.PresentationParameters.Bounds.Width;
            height = GraphicsDevice.PresentationParameters.Bounds.Height;
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Associate names in the dictionary with the graphics
            for (int x = 0; x < Textures.graphicNames.Length; x++)
            {
                Textures.textures.Add(Textures.graphicNames[x], Content.Load<Texture2D>(Textures.graphicNames[x]));
            }
            
            for(int x = 0; x < Textures.tileNames.Length; x++)
            {
                Textures.textures.Add(Textures.tileNames[x], Content.Load<Texture2D>("Tile/" + Textures.tileNames[x]));
            }

            //KittenZombie kt = new KittenZombie(400,300,worldState);
            //FlowerTentacles ft = new FlowerTentacles(600, 250, worldState);
            //creatures.Add(kt);
            /*
            for (int i = 0; i < 5; i++ )
            {
                for (int y2 = 0; y2 < 5; y2++)
                {
                    kt = new KittenZombie(i * 35, 5 + (y2 * 35), worldState);
                    ft = new FlowerTentacles(5 + (i * 35),(y2 * 35) + 230, worldState);
                    creatures.Add(kt);
                    creatures.Add(ft);
                }
            }
             * */
            inventory = new Inventory(300, 300);
            player = new Player(32*19, 32*5, worldState, inventory, this);
            cam = new Camera(player, width, height);
            creatures.Add(player);

            FlowerTentacles ft = new FlowerTentacles(32*11, 32*4, worldState);
            creatures.Add(ft);
            ft.setDefendTileGood(11, 4);
            ft.setDefendTileBad(11, 7);

            ft = new FlowerTentacles(32 * 12, 32 * 4, worldState);
            creatures.Add(ft);
            ft.setDefendTileGood(12, 4);
            ft.setDefendTileBad(12, 6);

            ft = new FlowerTentacles(32 * 12, 32 * 11, worldState);
            creatures.Add(ft);
            ft.setDefendTileGood(12, 11);
            ft.setDefendTileBad(13, 7);

            ft = new FlowerTentacles(32 * 11, 32 * 11, worldState);
            creatures.Add(ft);
            ft.setDefendTileGood(11, 11);
            ft.setDefendTileBad(12, 8);


            //ft = new FlowerTentacles(500, 400, worldState);
            //creatures.Add(ft);

            GreenBlock b = new GreenBlock(32*12, 32*7, worldState);
            items.Add(b);

            song = Content.Load<Song>("whitesky");
            song2 = Content.Load<Song>("climbing_up_the_walls");
            
            MediaPlayer.Play(song);
            makeMapFromFile("home.txt");
            //MediaPlayer.IsRepeating = true;
        }
        protected override void UnloadContent()
        {
          
        }

        protected override void Update(GameTime gameTime)
        {
            if (worldState)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
            }
            else
            {
                GraphicsDevice.Clear(Color.Black);
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            newKeyBoardState = Keyboard.GetState();



            oldKeyBoardState = newKeyBoardState;
            //make arraylist of all collidable things, only check collisions against those

            foreach (Creature c in creatures)
            {
                c.move();
            }

            checkCollisions();
            
             
        }

        public void checkCollisions()
        {
            foreach(Creature c in creatures)
            {
                Rectangle hitBox = c.getHitBox();
                foreach (Creature d in creatures)
                {
                    if (!c.Equals(d))
                    {
                        Rectangle hitBoxOther = d.getHitBox();
                        if(c.getHitBox().Intersects(d.getHitBox()))
                        {
                            c.reportCollision(d);
                        }
                    }
                }
                foreach (Tile t in collidableTiles)
                {
                    Rectangle hitBoxOther = t.getHitBox();
                    
                    if (c.getHitBox().Intersects(t.getHitBox()))
                    {
                        c.reportCollision(t);
                    }
                }
            }

            for (int ii = items.Count - 1; ii >= 0; ii--)
            {
                Rectangle hitBox = ((Item)items[ii]).getHitBox();
                if (player.getHitBox().Intersects(hitBox))
                {
                    //If inventory is not full
                    player.reportCollision((Item)items[ii]);
                    items.RemoveAt(ii);
                }
            }
        }

        
        protected override void Draw(GameTime gameTime)
        {
            cam.update();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, cam.getMatrix());

            drawMap(spriteBatch);

            foreach (Item i in items)
            {
                spriteBatch.Draw(i.getGraphic(), new Vector2(i.getX(), i.getY()), i.getImageBoundingRectangle(), Color.White);
            }

            foreach (Creature c in creatures)
            {
                spriteBatch.Draw(c.getGraphic(), new Vector2(c.getX(), c.getY()), c.getImageBoundingRectangle(), Color.White);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                inventory.drawInventory(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);

        }

        public void switchStates()
        {
            cam.shake(10);
            worldState = !worldState;

            foreach (Creature c in creatures)
            {
                c.setState(worldState);
            }
            foreach (Item i in items)
            {
                i.setState(worldState);
            }
            if (worldState)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
            }
            else
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song2);
            }

            InventoryItem[,] inventoryItems = inventory.getItems();
            for(int x = 0; x < inventoryItems.GetLength(0);x++)
            {
                for (int y = 0; y < inventoryItems.GetLength(1);y++ )
                {
                    if(inventoryItems[x,y] != null)
                    inventoryItems[x, y].switchState();
                }
            }
            foreach (Tile t in tiles)
            {
                t.setState(worldState);
            }
        }

        public void makeMapFromFile(string filename)
        {
            int xposition = 0;
            int yposition = 0;
            
            //This directory navigation might have to change for the final product, or even sooner
            string[] lines = System.IO.File.ReadAllLines(@"../../../Map Files/" + filename);

            wallArray = new bool[lines.GetLength(0), lines[0].Length];
            foreach (string line in lines)
            {
                
                foreach (char c in line)
                {
                    string image = Textures.charToFileName[c];
                    string[] images = image.Split(',');
                    bool solid = (images[2].Length > 0);
                    if (c == 'f')
                    {
                        BadTile t = new BadTile(xposition, yposition, worldState, images[0], images[1]);
                        collidableTiles.Add(t);
                        tiles.Add(t);
                    }
                    else if (c == 's')
                    {
                        GoodTile t = new GoodTile(xposition, yposition, worldState, images[0], images[1]);
                        collidableTiles.Add(t);
                        tiles.Add(t);
                    }
                    else if (c == 'l')
                    {
                        LockTile t = new LockTile(xposition, yposition, worldState, images[0], images[1]);
                        collidableTiles.Add(t);
                        tiles.Add(t);
                    }
                    else if (c == 'u')
                    {
                        UnlockTile t = new UnlockTile(xposition, yposition, worldState, images[0], images[1]);
                        collidableTiles.Add(t);
                        tiles.Add(t);
                    }
                    else if (!solid)
                    {
                        Tile t = new Tile(xposition, yposition, worldState, images[0], images[1]);
                        tiles.Add(t);
                    }
                    else
                    {
                        Wall w = new Wall(xposition, yposition, worldState, images[0], images[1]);
                        collidableTiles.Add(w);
                        tiles.Add(w);
                        wallArray[yposition / 32, xposition / 32] = true;
                    }
                    xposition += 32;
                }
                cam.setBounds(xposition, yposition);
                xposition = 0;
                yposition += 32;
            }
        }

        public void drawMap(SpriteBatch sb)
        {
            foreach(Tile t in tiles){
                int xLoc = t.getX();
                int yLoc = t.getY();
                if (((xLoc > player.getX() - width - 32) && (xLoc < player.getX() + width)) &&
                    ((yLoc > player.getY() - height - 32) && (yLoc < player.getY() + height)))
                {
                    spriteBatch.Draw(t.getGraphic(), new Vector2(xLoc, yLoc), new Rectangle(0, 0, 32, 32), Color.White);
                }
            }
        }
        public bool getWorldState()
        {
            return worldState;
        }
    }
}
