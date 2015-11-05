using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
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
        public static Random random = new Random();

        //public static ArrayList collidableTiles = new ArrayList();
        public static ArrayList miscObjects = new ArrayList();
        public static ArrayList playerActivateTiles = new ArrayList();
        public static ArrayList levelTiles = new ArrayList();

        string gameState;


        public ArrayList levels = new ArrayList();

        public static bool worldState = true;
        public static bool[,] wallArray;
        public static double[,] darkTileArray;

        HubLevel hubLevel;
        HouseLevel houseLevel;
        SchoolLevel schoolLevel;
        Level currentLevel;
        GateLevel gateLevel;
        Gate1Level gate1Level;
        Gate2Level gate2Level;
        Complex1 complex1Level;
        int time;
        int width;
        int height;
        Player player;
        //public static List<Game1> game = new List<Game1>();
        Camera cam;
        //Song song;
        //Song song2;
        Inventory inventory;
        KeyboardState newKeyBoardState, oldKeyBoardState;
        MouseState oldMouseState;
        int shiftCooldown = 0;
        int maxShiftCooldown = 10 * 5;

        

        private string currentLevelString;
        private float blackScreenAlpha;
        private bool fadeDirection;
        private Vector2 startButtonPosition;
        private Vector2 exitButtonPosition;

        private Vector2 exitButton2Position;
        private Vector2 restartButtonPosition;

        private Texture2D startButton;
        private Texture2D exitButton;

        Rectangle healthBar = new Rectangle(0, 0, 48, 48);

        private SoundEffectInstance banditKing;
        



        public Game1()
        {
            time = 0;
            graphics = new GraphicsDeviceManager(this);
           // graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //game.Add(this);
            //graphics.PreferredBackBufferWidth = 1400;

            //graphics.PreferredBackBufferHeight = 800;
            //graphics.ApplyChanges();
            
            
          
        }

        protected override void Initialize()
        {
            startButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) + 160, 300);
            exitButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) + 160, 400);
            exitButton2Position = new Vector2((GraphicsDevice.Viewport.Width / 2) + 160, 400);
            restartButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) + 160, 400);
            base.Initialize();
            //width = Window.ClientBounds.Width;
            //height = Window.ClientBounds.Height;
            fadeDirection = false;
            blackScreenAlpha = 0;
            currentLevelString = "hub";
            
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
            gateLevel = new GateLevel();
            gate1Level = new Gate1Level();
            gate2Level = new Gate2Level();
            complex1Level = new Complex1();

            currentLevel = houseLevel;

            inventory = new Inventory();
            //player = new Player(13*32, 25*32, inventory, this);
            player = new Player(30 * 32, 9 * 32, inventory, this);
            cam = new Camera(player, width, height);
            creatures.Add(player);

            sf = Content.Load<SpriteFont>("kooten");



            //song = Content.Load<Song>("banditKing2");

            SoundEffect banditKing_s = Content.Load<SoundEffect>("banditKing2");
            banditKing = banditKing_s.CreateInstance();

            //banditKing.Pitch = -1;

            //song2 = Content.Load<Song>("climbing_up_the_walls");

            currentLevel.loadLevel();
            //add level transfer tiles from current level to the array list of tiles
            tiles.AddRange(currentLevel.getLevelTiles());
            levelTiles.AddRange(currentLevel.getLevelTiles());
            cam.setBounds(currentLevel.getMapSizeX(), currentLevel.getMapSizeY());
            
            //MediaPlayer.Play(song);
            banditKing.Play();
            gameState = "start";
            //MediaPlayer.IsRepeating = true;
        }

        public void reloadLevel()
        {
            setLevel(currentLevelString);
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

                foreach (Miscellanious p in miscObjects)
                {
                    Rectangle hitBoxOther = p.getHitBox();
                    if (c.getHitBox().Intersects(hitBoxOther))
                    {
                        c.reportCollision(p);
                        if (p is Button)
                        {
                            if (((Button)p).isHeavyButton() == false)
                            {
                                p.onTrigger();
                            }
                        }
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

            foreach(PlayerActivateTile t in playerActivateTiles)
            {
                Rectangle hitBox = t.getHitBox();
                if (player.getHitBox().Intersects(hitBox))
                {
                    player.reportCollision(t);
                    t.setPressed(true);
                }
                else
                {
                    t.setPressed(false);
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
                m.onShift();
                //Check if we are in a strawberry and snap into it
                if ((worldState == true) && (m is Strawberry))
                {
                    if (player.checkOverlap(m))
                    {
                        player.setX(m.getX());
                        player.setY(m.getY());
                        player.updateHitbox();
                    }
                }
            }
            /*
            if (worldState)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
            }
            else
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song2);
            }*/

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
                float opacity = 1.0f;
                int xLoc = t.getX();
                int yLoc = t.getY();
                if (((xLoc > player.getX() - width - 32) && (xLoc < player.getX() + width)) &&
                    ((yLoc > player.getY() - height - 32) && (yLoc < player.getY() + height)))
                {
                    LampI lamp = inventory.getLamp();
                    if(lamp!= null && lamp.hasBatteries())
                    {
                        double distance = Math.Sqrt(Math.Pow(player.getX() - xLoc, 2) + Math.Pow(player.getY() - yLoc, 2));
                        t.addLampLight(distance);
                        
                    }
                    opacity = t.getOpacity();
                    spriteBatch.Draw(t.getGraphic(), new Vector2(xLoc, yLoc), new Rectangle(0, 0, 32, 32), new Color(Color.White, opacity));
                }

                darkTileArray[yLoc / 32, xLoc / 32] = 1 - opacity;
            }
        }

        public bool getWorldState()
        {
            return worldState;
        }

        public void setLevel(string level)
        {
            currentLevelString = level;
            levelTiles.Clear();
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
            else if (level.Equals("gate1Level"))
            {
                currentLevel = gate1Level;
            }
            else if (level.Equals("gate2Level"))
            {
                currentLevel = gate2Level;
            }
            else if (level.Equals("complex1Level"))
            {
                currentLevel = complex1Level;
            }
            creatures.Add(player);
            currentLevel.loadLevel();
            //add level transfer tiles from current level to the array list of tiles
            tiles.AddRange(currentLevel.getLevelTiles());
            levelTiles.AddRange(currentLevel.getLevelTiles());
            player.setX(currentLevel.getPlayerStartingX());
            player.setY(currentLevel.getPlayerStartingY());
            
            cam.setBounds(currentLevel.getMapSizeX(), currentLevel.getMapSizeY());
            cam.changeRoom();
        }

        //For each game state
        public void startUpdate()
        {
            MouseState mouseState = Mouse.GetState();
            int mouseX, mouseY;

            //Magic don't remove
            if(graphics.IsFullScreen)
            {
                mouseX = (int)(mouseState.X / GraphicsDevice.Viewport.AspectRatio + 0.5);
                mouseY = (int)(mouseState.Y / GraphicsDevice.Viewport.AspectRatio + 0.5);
            }
            else
            {
                mouseX = mouseState.X;
                mouseY = mouseState.Y;
            }
            if(mouseState.LeftButton == ButtonState.Pressed)
            {

                
                Rectangle startButtonRect = new Rectangle((int)startButtonPosition.X,
                                    (int)startButtonPosition.Y, 160, 64);
                Rectangle exitButtonRect = new Rectangle((int)exitButtonPosition.X,
                                    (int)exitButtonPosition.Y, 160, 64);
                
                if(startButtonRect.Contains(new Vector2(mouseX,mouseY)))
                {
                    gameState = "play";
                }

                else if (exitButtonRect.Contains(new Vector2(mouseX, mouseY)))
                {
                    Exit();
                }
            }
            
           
        }
        public void startDraw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Textures.textures["titleScreen3"], new Vector2(0,0), Color.White);
            spriteBatch.Draw(Textures.textures["start"], startButtonPosition,  Color.White);
            spriteBatch.Draw(Textures.textures["exit"], exitButtonPosition,  Color.White);
            spriteBatch.End();
        }

        public void playUpdate()
        {

            time++;
            newKeyBoardState = Keyboard.GetState();
            IsMouseVisible = false;
            if (worldState)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
            }
            else
            {
                GraphicsDevice.Clear(Color.Black);
            }
            foreach (Miscellanious m in miscObjects)
            {
                m.onUpdate();
            }
            foreach (Creature c in creatures)
            {
                c.move();
            }
            player.moveUpdate();

            checkCollisions();


            cam.update();
            inventoryUpdate();

            if (shiftCooldown > 0)
            {
                shiftCooldown--;
            }
            if (player.isDead())
            {
                gameState = "lost";
                IsMouseVisible = true;
            }

            

            if (newKeyBoardState.IsKeyDown(Keys.P) && !oldKeyBoardState.IsKeyDown(Keys.P))
            {
                gameState = "pause";
            }
            else if (newKeyBoardState.IsKeyDown(Keys.R) && !oldKeyBoardState.IsKeyDown(Keys.R))
            {
                reloadLevel();
            }



            oldKeyBoardState = newKeyBoardState;


            oldKeyBoardState = newKeyBoardState;
        }
        public void playDraw()
        {

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, cam.getMatrix());
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
            spriteBatch.Draw(player.getGraphic(), new Vector2(player.getX(), player.getY()), player.getImageBoundingRectangle(), Color.White);
            if (!worldState)
            drawDarkTiles(spriteBatch);
           


            spriteBatch.DrawString(sf, player.getX() / 32 + " " + player.getY() / 32, new Vector2(player.getX(), player.getY() - 12), Color.Black);

            //spriteBatch.Draw(player.getGraphic(), new Vector2(player.getX(), player.getY()), player.getImageBoundingRectangle(), Color.White);

            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                inventory.drawInventory(spriteBatch);
                Vector2 cursorPosition = new Vector2(mouseState.X + getTopLeft().X, mouseState.Y + getTopLeft().Y);
                foreach (InventoryItem i in inventory.getItems())
                {
                    if (i != null)
                    {
                        Rectangle r = i.getHitBox();

                        //var mousePosition = new Point();
                        if (r.Contains(cursorPosition))
                        {
                            string tip = i.getItemTip();
                            spriteBatch.DrawString(sf, tip, new Vector2(inventory.getX(), inventory.getY() + 70), Color.Black);
                        }
                    }
                }
                spriteBatch.Draw(Textures.textures["cursor"], cursorPosition, new Rectangle(0, 0, 32, 32), Color.White);
            }   
            //rays of light juice and darkness for dark world
            spriteBatch.Draw(Textures.textures["shadowScreen"], new Vector2(-cam.getX() - width / 2, -cam.getY() - (height / 2) + (((float)Math.Sin(time * 3.14529 / 180) + 1.0f) * 40)), new Rectangle(0, 0, 800, 640), Color.White * 0.7f);
            if (worldState)
            {
                if (currentLevel.getIndoors() == false)
                {
                spriteBatch.Draw(Textures.textures["rays"], new Vector2(-cam.getX() - width / 2, -cam.getY() - height / 2), new Rectangle(0, 0, 800, 640), Color.White * ((float)Math.Sin(time/2 * 3.14529 / 180) / 4));
                }
            }
            else
            {
                spriteBatch.Draw(Textures.textures["darkHaze"], new Vector2(-cam.getX() - width / 2, -cam.getY() - height / 2), new Rectangle(0, 0, 800, 640), Color.White * ((float)Math.Sin(time * 3.14529 / 180) / 2f));
            }
            //gui drawn last
            drawShiftCD();
            drawHealthBar();
            spriteBatch.End();
        }

        public void pauseUpdate()
        {
            newKeyBoardState = Keyboard.GetState();
            if(newKeyBoardState.IsKeyDown(Keys.P) && !oldKeyBoardState.IsKeyDown(Keys.P))
            {
                gameState = "play";
            }



            oldKeyBoardState = newKeyBoardState;
        }
        public void pauseDraw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Textures.textures["pause"], new Vector2(0, 0), new Rectangle(0, 0, 800, 640), Color.White);
            spriteBatch.End();
        }
        public void lostUpdate()
        {
            MouseState m = Mouse.GetState();
            if (m.LeftButton == ButtonState.Pressed)
            {
                Rectangle replayButtonRect = new Rectangle(538,381,140,50);
                Rectangle exitButtonRect = new Rectangle(544,483,90,45);
                //Rectangle startButtonRect = new Rectangle((int)startButtonPosition.X,
                 //                   (int)startButtonPosition.Y, 160, 64);
                //Rectangle exitButtonRect = new Rectangle((int)exitButtonPosition.X,
                  //                  (int)exitButtonPosition.Y, 160, 64);
               // Console.Write(m.X+" "+m.Y+"\n");
                int mouseX, mouseY;
                mouseX = (int)(m.X / GraphicsDevice.Viewport.AspectRatio + 0.5);
                mouseY = (int)(m.Y / GraphicsDevice.Viewport.AspectRatio + 0.5);

                if (replayButtonRect.Contains(new Vector2(m.X, m.Y)))
                {
                    //setLevel(hubLevel);
                    gameState = "play";
                    worldState = true;
                    currentLevel.unloadLevel();
                    
                    currentLevel = hubLevel;
                    currentLevel.loadLevel();
                    inventory = new Inventory();
                    player = new Player(13*32, 25*32, inventory, this);
                    creatures.Add(player);
                    cam = new Camera(player, width, height);
                    cam.setBounds(currentLevel.getMapSizeX(), currentLevel.getMapSizeY());
                }
                if (exitButtonRect.Contains(new Vector2(m.X, m.Y)))
                {
                    Exit();
                }
            }


        }
        public void lostDraw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Textures.textures["lostScreen"], new Vector2(0, 0), Color.White);
            spriteBatch.End();
        }

        //these don't work fix them plz
        public void fadeToBlack()
        {
            for (int i = 0; i < 100; i++)
            {
                blackScreenAlpha += 0.05f;
                playDraw();
            }
        }
        public void fadeFromBlack()
        {
            for (int i = 0; i < 100; i++)
            {
                blackScreenAlpha -= 0.05f;
                playDraw();
            }
        }


        public void drawShiftCD()
        {
            int rectWidth = (int)((1 - (double)shiftCooldown / maxShiftCooldown) * 124 + 0.5);
            Vector2 shiftCDLocation = new Vector2(getTopLeft().X + 10, getTopLeft().Y + 10);


            Rectangle r = new Rectangle(0, 0, rectWidth, 12);

            spriteBatch.Draw(Textures.textures["shiftCooldownBar"], new Vector2(shiftCDLocation.X + 2, shiftCDLocation.Y + 2), r,Color.White);
            spriteBatch.Draw(Textures.textures["shiftCooldown"], shiftCDLocation, Color.White);
            if(player.isLocked())
            {
                Vector2 loc = new Vector2(shiftCDLocation.X + 128 / 2 - 8, shiftCDLocation.Y);
                spriteBatch.Draw(Textures.textures["shiftlocked"], loc, Color.White);
            }
        }


        public void drawHealthBar()
        {
            Texture2D rectTop, rectBottom = null;
            Vector2 healthBarLocation = new Vector2(-cam.getX() - width / 2 + 10, -cam.getY() - height/2 + 32);
            int rectHeight = (int)(player.getProportion() * healthBar.Height);

            if (worldState)
            {
                rectTop = Textures.textures["grayblock"];
                rectBottom = Textures.textures["whiteblock"];
            }
            else
            {
                rectTop = Textures.textures["whiteblock"];
                rectBottom = Textures.textures["grayblock"];
            }
            spriteBatch.Draw(rectTop, healthBarLocation, new Rectangle(0, 0, rectBottom.Width, (healthBar.Height - rectHeight)), Color.White);
            spriteBatch.Draw(rectBottom, new Vector2(healthBarLocation.X, healthBarLocation.Y + (healthBar.Height - rectHeight)), new Rectangle(0,0, rectBottom.Width, rectHeight), Color.White);
            
            if ((player.getProportion() < 0.20) && (time % 50 >= 45))
            { 
                spriteBatch.Draw(Textures.textures["hourglass2"], healthBarLocation, Color.White);
            }
            else
            {
                spriteBatch.Draw(Textures.textures["hourglass2"], healthBarLocation, Color.Black);
            }
        }

        public void inventoryUpdate()
        {
            Texture2D tex = Textures.textures["inventory2"];
            inventory.setX(getTopRight().X - tex.Width - 10);
            inventory.setY(getTopLeft().Y + 10);
            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                MouseState mouseState = Mouse.GetState();
                Vector2 inventoryPosition = new Vector2(inventory.getX(), inventory.getY());


                Vector2 cursorPosition = new Vector2(mouseState.X + getTopLeft().X, mouseState.Y + getTopLeft().Y);
                foreach (InventoryItem i in inventory.getItems())
                {
                    if (i != null)
                    {
                        Rectangle r = new Rectangle(i.getX() + (int)(inventoryPosition.X + 0.5), i.getY() + (int)(inventoryPosition.Y + 0.5), 32, 32);

                        if (mouseState.LeftButton == ButtonState.Released)
                        {
                            if(i.isSelected())
                            {
                                inventory.setNewIndex(i);
                            }
                            inventory.setSelectedItem(i, false);
                        }

                        if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton != ButtonState.Pressed)
                        {
                            if (r.Contains(cursorPosition.X, cursorPosition.Y))
                            {
                                inventory.setSelectedItem(i, true);
                            }
                        }
                        if (i.isSelected())
                        {
                            int curX = i.getX();
                            int curY = i.getY();

                            int deltaX = mouseState.X - oldMouseState.X;
                            int deltaY = mouseState.Y - oldMouseState.Y;
                            i.setX(curX + deltaX);
                            i.setY(curY + deltaY);
                        }
                    }
                }
                oldMouseState = mouseState;
            }
        }


        public void setShiftCD()
        {
            shiftCooldown = maxShiftCooldown;
        }
        public int getShiftCD()
        {
            return shiftCooldown;
        }


        public Point getCenter()
        {
            return new Point(-cam.getX(), -cam.getY());
        }

        public Point getTopLeft()
        {
            Point center = getCenter();
            return new Point(center.X - width / 2, center.Y - height / 2);
        }

        public Point getTopRight()
        {
            Point center = getCenter();
            return new Point(center.X + width / 2, center.Y - height / 2);
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
