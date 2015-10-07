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
        public static ArrayList creatures = new ArrayList();
        public static ArrayList items = new ArrayList();
        public static ArrayList tiles = new ArrayList();
        public static ArrayList collidableTiles = new ArrayList();
        public static bool worldState = true;
        public static bool[,] wallArray;

        HouseLevel houseLevel;
        SchoolLevel schoolLevel;
        Level currentLevel;

        int width;
        int height;
        float time;
        Player player;
        Song song;
        Song song2;
        Inventory inventory;
        KeyboardState newKeyBoardState, oldKeyBoardState;
        
        public Game1()
        {
            time = 0;
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1400;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
          
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

            inventory = new Inventory(300, 300);
            player = new Player(32*19, 32*5, inventory, this);
            creatures.Add(player);


            song = Content.Load<Song>("whitesky");
            song2 = Content.Load<Song>("climbing_up_the_walls");
            houseLevel = new HouseLevel();
            schoolLevel = new SchoolLevel();

            currentLevel = houseLevel;
            currentLevel.loadLevel();
            
            MediaPlayer.Play(song);
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

            if(newKeyBoardState.IsKeyDown(Keys.X) && !oldKeyBoardState.IsKeyDown(Keys.X))
            {
                if (currentLevel.Equals(houseLevel))
                {
                    houseLevel.unloadLevel();
                    currentLevel = schoolLevel;
                }
                else
                {
                    schoolLevel.unloadLevel();
                    currentLevel = houseLevel;
                }
                currentLevel.loadLevel();
                creatures.Add(player);
            }
            

            




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
            Random rnd = new Random();
            Matrix camMatrix;
            if (time < 0)
                camMatrix = Matrix.CreateTranslation(-player.getX() + width / 2, -player.getY() + height / 2, 0);
            else
            {
                time--;
                int xMod;
                int yMod;
                //int xMod = (int)(100 * Math.Sin((time)));
                //int yMod = (int)(100 * Math.Cos((time)));
                xMod = (int)(rnd.Next(1, 9) * Math.Sin(time));
                yMod = (int)(rnd.Next(1, 8) * Math.Sin(time));
                camMatrix = Matrix.CreateTranslation(-player.getX() + width / 2  + xMod, -player.getY() + yMod + height / 2, 0);
            }
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camMatrix);

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
            time = 10;
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

        public void drawMap(SpriteBatch sb)
        {
            foreach(Tile t in tiles){
                spriteBatch.Draw(t.getGraphic(), new Vector2(t.getX(), t.getY()), new Rectangle(0,0,32,32), Color.White);
            }
        }
        public bool getWorldState()
        {
            return worldState;
        }

         
    }
}
