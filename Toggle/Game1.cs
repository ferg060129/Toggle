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
        private bool paused = false;
        private bool pauseKeyDown = false;
        private bool pausedForGuide = false;

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
            
            for (int i = 0; i < width; i+=32) { 
                for (int z = 0; z < height; z+=32)
                {
                    
                    //get a random #, if below a certain threshhold, draw a flower
                    Tile t = new Tile(i, z, worldState);
                    tiles.Add(t);
                }
            }
            KittenZombie kt = new KittenZombie(400,300,worldState);
            creatures.Add(kt);
            kt = new KittenZombie(100, 100, worldState);
            creatures.Add(kt);
            player = new Player(500, 300, worldState);
            creatures.Add(player);

            FlowerTentacles ft = new FlowerTentacles(600, 250, worldState);
            creatures.Add(ft);

            ft = new FlowerTentacles(300, 350, worldState);
            creatures.Add(ft);

            GreenBlock b = new GreenBlock(250, 300, worldState);
            items.Add(b);

            song = Content.Load<Song>("whitesky");
            song2 = Content.Load<Song>("climbing_up_the_walls");
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

            foreach (Creature c in creatures)
            {
                c.move();
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

            foreach (Tile t in tiles)
            {
                spriteBatch.Draw(t.getGraphic(), new Vector2(t.getX(), t.getY()), t.getImageBoundingRectangle(), Color.White);
            }

            foreach (Creature c in creatures)
            {
                spriteBatch.Draw(c.getGraphic(), new Vector2(c.getX(), c.getY()), c.getImageBoundingRectangle(), Color.White);
            }

            foreach (Item i in items)
            {
                spriteBatch.Draw(i.getGraphic(), new Vector2(i.getX(), i.getY()), i.getImageBoundingRectangle(), Color.White);
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
                player.pickUp(i);
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
        }
    }
}
