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
        int width;
        int height;
        Player player;
        Song song;
        Song song2;
        Inventory inventory; 
        KeyboardState newKeyBoardState, oldKeyBoardState;
        bool worldState = true;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
          
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

            KittenZombie kt = new KittenZombie(400,300,worldState);
            //FlowerTentacles ft = new FlowerTentacles(600, 250, worldState);
            creatures.Add(kt);
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
          
            player = new Player(200, 200, worldState);
            creatures.Add(player);

            /*FlowerTentacles ft = new FlowerTentacles(600, 250, worldState);
            creatures.Add(ft);

            ft = new FlowerTentacles(300, 350, worldState);
            creatures.Add(ft);*/

            GreenBlock b = new GreenBlock(400, 200, worldState);
            items.Add(b);

            song = Content.Load<Song>("whitesky");
            song2 = Content.Load<Song>("climbing_up_the_walls");
            inventory = new Inventory(150, 200);
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
                GraphicsDevice.Clear(Color.Orange);
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            newKeyBoardState = Keyboard.GetState();

            if (newKeyBoardState.IsKeyDown(Keys.T) && oldKeyBoardState != null && !oldKeyBoardState.IsKeyDown(Keys.T))
            {
                switchStates();
            }

            oldKeyBoardState = newKeyBoardState;
            //make arraylist of all collidable things, only check collisions against those
            ArrayList collisions = new ArrayList();
            foreach (Tile t in tiles)
            {
                if (t.getCollision() == true)
                    collisions.Add(t);
            }
         
            
            foreach(Creature c in creatures){
                c.move(collisions);
                if (collision(c.getHitBox()))
                {
                    //c.invertDirection();
                    switchStates();
                    break;
                }
                if (wallBound(c))
                {
                    //c.invertDirection();
                    switchStates();
                }
            }
            for (int ii = 0; ii < items.Count; ii++)
            {
                itemCollision((Item)items[ii]);
            }
             
        }
           
        
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

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
        public void reloadContent()
        {
            
        }


        public bool collision(Rectangle rect)
        {
            foreach (Creature c in creatures)
            {
                if (!c.getHitBox().Equals(rect))
                {
                    Rectangle otherRect = c.getHitBox();
                    if (rect.Intersects(otherRect))
                    {
                        return true;
                    }    
                }
            }
            return false;
        }

        internal void itemCollision(Item i)
        {            
            if (player.getHitBox().Intersects(i.getHitBox()))
            {
                //player.pickUp(i);
                inventory.addInventoryItem(i.pickUpItem());
                items.Remove(i);
            }  
        }

        internal bool wallBound(Creature c)
        {
            Rectangle hitBox = c.getHitBox();
            if (hitBox.Right > width)
            {
                c.setX(width - hitBox.Width);
                return true;
            }
            else if (hitBox.Left < 0)
            {
                c.setX(0);
                return true;
            }
            else if (hitBox.Bottom > height)
            {
                c.setY(height - hitBox.Height);
                return true;
            }
            else if (hitBox.Top < 0)
            {
                c.setY(0);
                return true;
            }
            return false;
        }

        public void switchStates()
        {
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
            foreach (string line in lines)
            {
                foreach (char c in line)
                {
                    string image = Textures.charToFileName[c];
                    string[] images = image.Split(',');
                    bool solid = (images[2].Length > 0);
                    tiles.Add(new Tile(xposition, yposition, worldState, images[0], images[1], solid, c));
                    xposition += 32;
                }
                xposition = 0;
                yposition += 32;
            }
        }

        public void drawMap(SpriteBatch sb)
        {
            foreach(Tile t in tiles){
                spriteBatch.Draw(t.getGraphic(), new Vector2(t.getX(), t.getY()), new Rectangle(0,0,32,32), Color.White);
            }
        }



    }
}
