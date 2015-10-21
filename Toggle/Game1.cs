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

        SpriteFont sf;

        //SpriteFont
        //Make sure all arrays are cleared in Level.unloadLevel
        public static ArrayList creatures = new ArrayList();
        public static ArrayList items = new ArrayList();
        public static ArrayList tiles = new ArrayList();
        public static ArrayList solidTiles = new ArrayList();
        public static ArrayList darkTiles = new ArrayList();

        //public static ArrayList collidableTiles = new ArrayList();
        public static ArrayList miscObjects = new ArrayList();
        public static ArrayList playerActivateTiles = new ArrayList();
        public static ArrayList levelTiles = new ArrayList();

        string gameState;


        public ArrayList levels = new ArrayList();

        public static bool worldState = true;
        public static bool[,] wallArray;

        HubLevel hubLevel;
        HouseLevel houseLevel;
        SchoolLevel schoolLevel;
        Level currentLevel;
        int time;
        int width;
        int height;
        Player player;
        Camera cam;
        Song song;
        Song song2;
        Inventory inventory;
        KeyboardState newKeyBoardState, oldKeyBoardState;
        MouseState oldMouseState;

        private Vector2 startButtonPosition;
        private Vector2 exitButtonPosition;
        private Texture2D startButton;
        private Texture2D exitButton;


        public Game1()
        {
            time = 0;
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //graphics.PreferredBackBufferWidth = 1400;
            //graphics.PreferredBackBufferHeight = 800;
           // graphics.ApplyChanges();
            
          
        }

        protected override void Initialize()
        {
            startButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 80, 200);
            exitButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 80, 300);
            base.Initialize();
            //width = Window.ClientBounds.Width;
            //height = Window.ClientBounds.Height;
            
        }

        protected override void LoadContent()
        {

            startButton = Content.Load<Texture2D>(@"start");
            exitButton = Content.Load<Texture2D>(@"exit");


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
            hubLevel = new HubLevel();
            houseLevel = new HouseLevel();
            schoolLevel = new SchoolLevel();

            currentLevel = hubLevel;

            inventory = new Inventory(300, 300);
            player = new Player(currentLevel.getPlayerStartingX(), currentLevel.getPlayerStartingY(), inventory, this);
            cam = new Camera(player, width, height);
            creatures.Add(player);

            sf = Content.Load<SpriteFont>("kooten");

            song = Content.Load<Song>("whitesky");
            song2 = Content.Load<Song>("climbing_up_the_walls");

            currentLevel.loadLevel();
            cam.setBounds(currentLevel.getMapSizeX(), currentLevel.getMapSizeY());
            
            MediaPlayer.Play(song);
            gameState = "start";
            //MediaPlayer.IsRepeating = true;
        }
        protected override void UnloadContent()
        {
          
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            switch(gameState)
            {
                case "start":
                    startUpdate();
                    break;
                case "play":
                    playUpdate();
                    break;
                case "pause":
                    pauseUpdate();
                    break;
                case "lost":
                    lostUpdate();
                    break;
            }
            base.Update(gameTime);

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
                        if(c.getHitBox().Intersects(hitBoxOther))
                        {
                            c.reportCollision(d);
                        }
                    }
                }
                foreach (Tile t in solidTiles)
                {
                    Rectangle hitBoxOther = t.getHitBox();
                    
                    if (c.getHitBox().Intersects(hitBoxOther))
                    {
                        c.reportCollision(t);
                    }
                }

                foreach (Pushable p in miscObjects)
                {
                    Rectangle hitBoxOther = p.getHitBox();
                    if (c.getHitBox().Intersects(hitBoxOther))
                    {
                        c.reportCollision(p);
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

            foreach(Tile t in playerActivateTiles)
            {
                Rectangle hitBox = t.getHitBox();
                if(player.getHitBox().Intersects(hitBox))
                {
                    player.reportCollision(t);
                }
            }
            checkLevelTileCollision();
        }


        public void checkLevelTileCollision()
        {
            for(int x = 0; x < levelTiles.Count; x++)
            {

                Tile t = (Tile)(levelTiles[x]);
                Rectangle hitBox = t.getHitBox();
                if(player.getHitBox().Intersects(hitBox))
                {
                    player.reportCollision(t);
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);
            
            switch (gameState)
            {
                case "start":
                    startDraw();
                    break;
                case "play":
                    playDraw();
                    break;
                case "pause":
                    pauseDraw();
                    break;
                case "lost":
                    lostDraw();
                    break;
            }
          
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
            foreach (Miscellanious m in miscObjects)
            {
                m.setState(worldState);
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
                int xLoc = t.getX();
                int yLoc = t.getY();
                if (((xLoc > player.getX() - width - 32) && (xLoc < player.getX() + width)) &&
                    ((yLoc > player.getY() - height - 32) && (yLoc < player.getY() + height)))
                {
                    spriteBatch.Draw(t.getGraphic(), new Vector2(xLoc, yLoc), new Rectangle(0, 0, 32, 32), Color.White);
                }
            }
        }
        public void drawDarkTiles(SpriteBatch sb)
        {
            foreach (DarkTile t in darkTiles)
            {

                int xLoc = t.getX();
                int yLoc = t.getY();
                if (((xLoc > player.getX() - width - 32) && (xLoc < player.getX() + width)) &&
                    ((yLoc > player.getY() - height - 32) && (yLoc < player.getY() + height)))
                {

                    double distance = Math.Sqrt(Math.Pow(player.getX() - xLoc, 2) + Math.Pow(player.getY() - yLoc, 2));
                    t.addLampLight(distance);
                    spriteBatch.Draw(t.getGraphic(), new Vector2(xLoc, yLoc), new Rectangle(0, 0, 32, 32), new Color(Color.White, t.getOpacity()));
                }
            }
        }

        public bool getWorldState()
        {
            return worldState;
        }

        public void setLevel(string level)
        {
            //Eventually turn level strings into global constants
            if(currentLevel != null)
            {
                currentLevel.unloadLevel();
            }

            if(level.Equals("hubLevel"))
            {
                currentLevel = hubLevel;
            }
            else if(level.Equals("houseLevel"))
            {
                currentLevel = houseLevel;
            }
            else if(level.Equals("schoolLevel"))
            {
                currentLevel = schoolLevel;
            }

            currentLevel.loadLevel();
            player.setX(currentLevel.getPlayerStartingX());
            player.setY(currentLevel.getPlayerStartingY());
            creatures.Add(player);
            cam.setBounds(currentLevel.getMapSizeX(), currentLevel.getMapSizeY());
            cam.changeRoom();
        }

        //For each game state
        public void startUpdate()
        {
            MouseState mouseState = Mouse.GetState();

            if(mouseState.LeftButton == ButtonState.Pressed)
            {
                Rectangle startButtonRect = new Rectangle((int)startButtonPosition.X,
                                    (int)startButtonPosition.Y, 160, 64);
                Rectangle exitButtonRect = new Rectangle((int)exitButtonPosition.X,
                                    (int)exitButtonPosition.Y, 160, 64);

                if(startButtonRect.Contains(new Vector2(mouseState.X,mouseState.Y)))
                {
                    gameState = "play";
                }

                else if (exitButtonRect.Contains(new Vector2(mouseState.X, mouseState.Y)))
                {
                    Exit();
                }
            }
           
        }
        public void startDraw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Textures.textures["start"], startButtonPosition,  Color.White);
            spriteBatch.Draw(Textures.textures["exit"], exitButtonPosition,  Color.White);
            spriteBatch.End();
        }

        public void playUpdate()
        {
            IsMouseVisible = false;
            if (worldState)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
            }
            else
            {
                GraphicsDevice.Clear(Color.Black);
            }

           

            foreach (Creature c in creatures)
            {
                c.move();
            }

            checkCollisions();
        }
        public void playDraw()
        {

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, cam.getMatrix());
           
            cam.update();
            MouseState mouseState = Mouse.GetState();
            
            drawMap(spriteBatch);

            foreach (Item i in items)
            {
                spriteBatch.Draw(i.getGraphic(), new Vector2(i.getX(), i.getY()), i.getImageBoundingRectangle(), Color.White);
            }

            foreach (Creature c in creatures)
            {
                spriteBatch.Draw(c.getGraphic(), new Vector2(c.getX(), c.getY()), c.getImageBoundingRectangle(), Color.White);
            }

            foreach (Miscellanious m in miscObjects)
            {
                spriteBatch.Draw(m.getGraphic(), new Vector2(m.getX(), m.getY()), m.getImageBoundingRectangle(), Color.White);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                inventory.drawInventory(spriteBatch, -cam.getX(), -cam.getY());
                Vector2 cursorPosition = new Vector2(mouseState.X - cam.getX() - width / 2, mouseState.Y - cam.getY() - height / 2);
                foreach (InventoryItem i in inventory.getItems())
                {
                    if (i != null)
                    {
                        Rectangle r = i.getHitBox();

                        //var mousePosition = new Point();
                        if (r.Contains(cursorPosition))
                        {
                            string tip = i.getItemTip();
                            spriteBatch.DrawString(sf, tip, new Vector2(r.X, r.Y + 70), Color.Black);

                        }
                    }
                }
                spriteBatch.Draw(Textures.textures["cursor"], cursorPosition, new Rectangle(0, 0, 32, 32), Color.White);
            }

            spriteBatch.DrawString(sf, player.getX() / 32 + " " + player.getY() / 32, new Vector2(player.getX(), player.getY() - 12), Color.Black);
            if (!worldState)
                drawDarkTiles(spriteBatch);
            spriteBatch.End();
        }

        public void pauseUpdate()
        {

        }
        public void pauseDraw()
        {

        }
        public void lostUpdate()
        {

        }
        public void lostDraw()
        {

        }    
    }

    enum GameState
    {
        start,
        play,
        pause,
        lost
    }
}
