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

        //Make sure all arrays are cleared in Level.unloadLevel
        public static ArrayList creatures = new ArrayList();
        public static ArrayList items = new ArrayList();
        public static ArrayList tiles = new ArrayList();
        public static ArrayList solidTiles = new ArrayList();
        public static ArrayList darkTiles = new ArrayList();
        public static ArrayList visuals = new ArrayList();
        public static ArrayList boats = new ArrayList();
        public static ArrayList platforms = new ArrayList();

        public static Random random = new Random();

        //public static ArrayList collidableTiles = new ArrayList();
        public static ArrayList miscObjects = new ArrayList();
        public static ArrayList updateMiscObjects = new ArrayList();
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
        GhostTestLevel ghostTestLevel;
        LaserTestLevel laserTestLevel;
        LaserIntro laserIntroLevel;
        LevelTile lastEnteredLevelTile;
        int time;
        int width;
        int height;
        Player player;
        //public static List<Game1> game = new List<Game1>();
        Camera cam;
        //Song song;
        //Song song2;
        Inventory inventory;
        bool showInventory;
        KeyboardState newKeyBoardState, oldKeyBoardState;
        MouseState oldMouseState, oldMouseState2;
        int shiftCooldown = 0;
        int maxShiftCooldown = 10 * 5;
        float fadeTransparency = 0.0f;
        int creditsOffset = 0;

        public static bool boatSpawned = false;

        

        private string currentLevelString;
        private float blackScreenAlpha;
        private bool fadeDirection;
        private Vector2 startButtonPosition;
        private Vector2 exitButtonPosition;

        private Vector2 exitButton2Position;
        private Vector2 restartButtonPosition;

        private Texture2D startButton;
        private Texture2D exitButton;
        private Texture2D screenDisplayed;
        private Texture2D laserColor;
        private int titleScreenPhase;

        Rectangle healthBar = new Rectangle(0, 0, 48, 48);

        private SoundEffectInstance banditKing;
        private SoundEffectInstance zone1good;
        private SoundEffectInstance zone1bad;

        bool draw = true;

        InventoryItem[,] backUpInventory;
        



        public Game1()
        {
            titleScreenPhase = 0;
            time = 0;
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
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
            lastEnteredLevelTile = new LevelTile(0, 0, "blackBlock", "blackBlock", "hubLevel", new Point(13 * 32, 25 * 32));
            showInventory = false;
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
            screenDisplayed = Textures.textures["titleScreen3"];
            for (int x = 0; x < Textures.spritefonts.Length; x++)
            {
                Textures.fonts.Add(Textures.spritefonts[x], Content.Load<SpriteFont>(Textures.spritefonts[x]));
            }

            hubLevel = new HubLevel();
            houseLevel = new HouseLevel();
            schoolLevel = new SchoolLevel();
            gateLevel = new GateLevel();
            gate1Level = new Gate1Level();
            gate2Level = new Gate2Level();
            complex1Level = new Complex1();
            ghostTestLevel = new GhostTestLevel();
            laserTestLevel = new LaserTestLevel();
            laserIntroLevel = new LaserIntro();

            //normal is hubLevel, change only to test
            currentLevel = hubLevel;

            inventory = new Inventory(this);
            //13 x 25 for hub level start
            player = new Player(13*32, 25*32, inventory, this);
            //player = new Player(15*32, 2*32, inventory, this);
            //player = new Player(30 * 32, 9 * 32, inventory, this);
            cam = new Camera(player, width, height);
            creatures.Add(player);

           

            //song = Content.Load<Song>("banditKing2");

            SoundEffect banditKing_s = Content.Load<SoundEffect>("banditKing2");
            banditKing = banditKing_s.CreateInstance();

            zone1good = Content.Load<SoundEffect>("zone1good").CreateInstance();
            zone1bad = Content.Load<SoundEffect>("zone1bad").CreateInstance();

            //banditKing.Pitch = -1;

            //song2 = Content.Load<Song>("climbing_up_the_walls");

            currentLevel.loadLevel();
            //add level transfer tiles from current level to the array list of tiles
            tiles.AddRange(currentLevel.getLevelTiles());
            levelTiles.AddRange(currentLevel.getLevelTiles());
            cam.setBounds(currentLevel.getMapSizeX(), currentLevel.getMapSizeY());
            
            //MediaPlayer.Play(song);
            //banditKing.Play();

            gameState = "start";
            //MediaPlayer.IsRepeating = true;
            zone1bad.Volume = 0;
        }

        public void reloadLevel()
        {
            player.setLocked(false);

            //currentLevel.addInitialLevelItems();
            inventory.setInventoryItems(backUpInventory);
            setLevel(lastEnteredLevelTile);
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
                case "winfade":
                    winFadeUpdate();
                    break;
                case "credits":
                    creditsUpdate();
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
                    if (((Item)items[ii]).canPickUp())
                    {
                        currentLevel.removeItem((Item)items[ii]);
                        items.RemoveAt(ii);
                    }
                  
                    
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
            foreach (UpdateMiscellanious u in updateMiscObjects)
            {
                Rectangle hitBox = u.getHitBox();
                if (player.getHitBox().Intersects(hitBox))
                {
                    player.reportCollision(u);
                    if(u is Boat)
                    gameState = "winfade";
                }
            }
            foreach(Platform p in platforms)
            {
                Rectangle hitBox = p.getHitBox();
                foreach(Item i in items)
                {
                    if(hitBox.Intersects(i.getHitBox()))
                    {
                        p.reportCollision(i);
                    }
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
            GraphicsDevice.Clear(Color.Black);
            
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
                case "winfade":
                    winFadeDraw();
                    break;
                case "credits":
                    creditsDraw();
                    break;
            }
            base.Draw(gameTime);
        }
        public void switchStates()
        {
            cam.shake(10);
            worldState = !worldState;
            if (worldState)
            {
                zone1bad.Volume = 0;
                zone1good.Volume = .7f;
            }
            else
            {
                zone1good.Volume = 0;
                zone1bad.Volume = 1;
            }

            foreach (Creature c in creatures)
            {
                c.setState(worldState);
                c.onShift();
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
            foreach (Visual v in visuals)
            {
                v.setState(worldState);
            }
            foreach (UpdateMiscellanious um in updateMiscObjects)
            {
                um.setState(worldState);
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
                    inventoryItems[x, y].setState(worldState);
                }
            }
            foreach (Tile t in tiles)
            {
                t.setState(worldState);
            }
        }
        public void drawMap(SpriteBatch sb)
        {
            if(!draw)
            {
                draw = true;
                return;
            }
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

        public void setLevel(LevelTile level)
        {
            backUpInventory = inventory.getItemsCopy();
            lastEnteredLevelTile = level;
            Console.WriteLine(level.getLevel());
            currentLevelString = level.getLevel();
            //Eventually turn level strings into global constants
            if(currentLevel != null)
            {
                currentLevel.unloadLevel();
            }

            if (currentLevelString.Equals("hubLevel"))
            {
                currentLevel = hubLevel;
            }
            else if (currentLevelString.Equals("houseLevel"))
            {
                currentLevel = houseLevel;
            }
            else if (currentLevelString.Equals("schoolLevel"))
            {
                currentLevel = schoolLevel;
            }
            else if (currentLevelString.Equals("gate1Level"))
            {
                currentLevel = gate1Level;
            }
            else if (currentLevelString.Equals("gate2Level"))
            {
                currentLevel = gate2Level;
            }
            else if (currentLevelString.Equals("complex1Level"))
            {
                currentLevel = complex1Level;
            }
            else if (currentLevelString.Equals("ghostTestLevel"))
            {
                currentLevel = ghostTestLevel;
            }
            else if (currentLevelString.Equals("laserTestLevel"))
            {
                currentLevel = laserTestLevel;
            }
            else if (currentLevelString.Equals("laserIntroLevel"))
            {
                currentLevel = laserIntroLevel;
            }
            creatures.Add(player);
            currentLevel.loadLevel();
            currentLevel.setPlayerStart(level.getPlayerStart());
            //add level transfer tiles from current level to the array list of tiles
            levelTiles.Clear();
            tiles.AddRange(currentLevel.getLevelTiles());
            levelTiles.AddRange(currentLevel.getLevelTiles());
            player.setX(currentLevel.getPlayerStartingX());
            player.setY(currentLevel.getPlayerStartingY());
            
            cam.setBounds(currentLevel.getMapSizeX(), currentLevel.getMapSizeY());
            cam.changeRoom();
            cam.update();
        }

        //For each game state
        public void startUpdate()
        {
            newKeyBoardState = Keyboard.GetState();
            if (titleScreenPhase == 0)
            {
                MouseState mouseState = Mouse.GetState();
                int mouseX, mouseY;

                //Magic don't remove
                if (graphics.IsFullScreen)
                {
                    mouseX = (int)(mouseState.X / GraphicsDevice.Viewport.AspectRatio + 0.5);
                    mouseY = (int)(mouseState.Y / GraphicsDevice.Viewport.AspectRatio + 0.5);
                }
                else
                {
                    mouseX = mouseState.X;
                    mouseY = mouseState.Y;
                }
                if (mouseState.LeftButton == ButtonState.Pressed)
                {


                    Rectangle startButtonRect = new Rectangle((int)startButtonPosition.X,
                                        (int)startButtonPosition.Y, 160, 64);
                    Rectangle exitButtonRect = new Rectangle((int)exitButtonPosition.X,
                                        (int)exitButtonPosition.Y, 160, 64);

                    if (startButtonRect.Contains(new Vector2(mouseX, mouseY)))
                    {
                        titleScreenPhase++;
                        screenDisplayed = Textures.textures["controls1"];
                    }

                    else if (exitButtonRect.Contains(new Vector2(mouseX, mouseY)))
                    {
                        Exit();
                    }
                }
            }
            if (titleScreenPhase == 1)
            {
                if ((newKeyBoardState.IsKeyDown(Keys.LeftShift) && oldKeyBoardState != null && !oldKeyBoardState.IsKeyDown(Keys.LeftShift))
                || (newKeyBoardState.IsKeyDown(Keys.RightShift) && oldKeyBoardState != null && !oldKeyBoardState.IsKeyDown(Keys.RightShift)))
                {
                    screenDisplayed = Textures.textures["controls2"];
                    titleScreenPhase++;
                    oldKeyBoardState = newKeyBoardState;
                }
            }
            if (titleScreenPhase == 2)
            {
                if ((newKeyBoardState.IsKeyDown(Keys.LeftShift) && oldKeyBoardState != null && !oldKeyBoardState.IsKeyDown(Keys.LeftShift))
                || (newKeyBoardState.IsKeyDown(Keys.RightShift) && oldKeyBoardState != null && !oldKeyBoardState.IsKeyDown(Keys.RightShift)))
                {
                    gameState = "play";
                }
            }
            if (newKeyBoardState.IsKeyUp(Keys.LeftShift) && newKeyBoardState.IsKeyUp(Keys.RightShift))
            {
                oldKeyBoardState = new KeyboardState();
            }
        }

        public void startDraw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(screenDisplayed, new Vector2(0,0), Color.White);
            if (titleScreenPhase == 0)
            {
                spriteBatch.Draw(Textures.textures["start"], startButtonPosition, Color.White);
                spriteBatch.Draw(Textures.textures["exit"], exitButtonPosition, Color.White);
            }
            spriteBatch.End();
        }

        public void playUpdate()
        {
            if (zone1good.State != SoundState.Playing && zone1bad.State != SoundState.Playing)
            {
                zone1good.Play();
                zone1bad.Play();
            }
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
            foreach(UpdateMiscellanious i in updateMiscObjects)
            {
                i.move();
            }
            player.moveUpdate();

            checkCollisions();


            cam.update();
            boxUpdate();
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



            if (newKeyBoardState.IsKeyDown(Keys.P) && !oldKeyBoardState.IsKeyDown(Keys.P) && !player.isReadingChalkboard())
            {
                gameState = "pause";
            }
            else if (newKeyBoardState.IsKeyDown(Keys.R) && !oldKeyBoardState.IsKeyDown(Keys.R) && !player.isReadingChalkboard())
            {
                reloadLevel();
            }
            else if (newKeyBoardState.IsKeyDown(Keys.I) && !oldKeyBoardState.IsKeyDown(Keys.I) && !player.isReadingChalkboard())
            {
                showInventory =! showInventory;
            }

            if (winCondition() && !boatSpawned)
            {
                boatSpawned = true;

                Boat boat = new Boat(28 * 32, 28 * 32);
                Game1.updateMiscObjects.Add(boat);
                //hubLevel.addLevelItem(boat);
            }



            oldKeyBoardState = newKeyBoardState;
        }

        public void playLaserDraw()
        {
            int [] tempArray = new int[4];
            int po = 0;
            if (!worldState)
            {
                laserColor = Textures.textures["laser"];
            }
            else
            {
                laserColor = Textures.textures["laserB"];
            }
            foreach (Miscellanious l in miscObjects)
            {
                if (l is LaserBlock)
                {
                    po = ((LaserBlock)l).getPhaseOffset();
                    tempArray = ((LaserBlock)l).getLaserEnds();
                    //this would look really nice in a for loop
                    if (((LaserBlock)l).getDirection() == true)
                    {
                        spriteBatch.Draw(laserColor,
                            new Rectangle(l.getX(), l.getY() + 24, Math.Abs(tempArray[0] - l.getX()), 16),
                                null, Color.White * (1.5f - Math.Abs((float)Math.Sin((time + po) * 3.14529 / 180))),
                                (float)Math.PI, new Vector2(0, 0), SpriteEffects.None, 0);
                        spriteBatch.Draw(laserColor,
                            new Rectangle(l.getX() + 32, l.getY() + 8, Math.Abs(tempArray[1] - l.getX()), 16), null,
                                Color.White * (1.5f - Math.Abs((float)Math.Sin((time + 40 + po) * 3.14529 / 180))),
                                0, new Vector2(0, 0), SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(laserColor,
                            new Rectangle(l.getX() + 24, l.getY() + 32, Math.Abs(tempArray[3] - l.getY()), 16),
                                null, Color.White * (1.5f - Math.Abs((float)Math.Sin((time + po) * 3.14529 / 180))),
                                (float)Math.PI / 2, new Vector2(0, 0), SpriteEffects.None,0);
                        spriteBatch.Draw(laserColor,
                            new Rectangle(l.getX() + 8, l.getY(), Math.Abs(tempArray[2] - l.getY()),16 ),
                                null, Color.White * (1.5f - Math.Abs((float)Math.Sin((time + 40 + po) * 3.14529 / 180))),
                                (float)Math.PI * 3 / 2, new Vector2(0, 0), SpriteEffects.None, 0);
                    }
                }
            }
        }

        public void playDraw()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, cam.getMatrix());
            MouseState mouseState = Mouse.GetState();
            drawMap(spriteBatch);
            
            foreach(Platform p in platforms)
            {
                spriteBatch.Draw(p.getGraphic(), new Vector2(p.getX(), p.getY()), p.getImageBoundingRectangle(), Color.White);
                
            }

            foreach (Item i in items)
            {
                spriteBatch.Draw(i.getGraphic(), new Vector2(i.getX(), i.getY()), i.getImageBoundingRectangle(), Color.White);
            }
            foreach (Creature c in creatures)
            {
                spriteBatch.Draw(c.getGraphic(), new Vector2(c.getX(), c.getY()), c.getImageBoundingRectangle(), Color.White * c.getAlpha());
            }
            foreach (Miscellanious m in miscObjects)
            {
                spriteBatch.Draw(m.getGraphic(), new Vector2(m.getX(), m.getY()), m.getImageBoundingRectangle(), Color.White);
            }
            playLaserDraw();
            foreach (Visual v in visuals)
            {
                spriteBatch.Draw(v.getGraphic(), new Vector2(v.getX(), v.getY()), v.getImageBoundingRectangle(), Color.White);
            }
            

           

            spriteBatch.Draw(player.getGraphic(), new Vector2(player.getX(), player.getY()), player.getImageBoundingRectangle(), Color.White);
            if (!worldState)
            drawDarkTiles(spriteBatch);


            //Debug, draw player coords
            spriteBatch.DrawString(Textures.fonts["mistral16"], player.getX() / 32 + " " + player.getY() / 32, new Vector2(player.getX(), player.getY() - 12), Color.Black);
            //spriteBatch.Draw(player.getGraphic(), new Vector2(player.getX(), player.getY()), player.getImageBoundingRectangle(), Color.White);
            Vector2 cursorPosition = new Vector2(mouseState.X + getTopLeft().X, mouseState.Y + getTopLeft().Y);
            if (showInventory && !player.isReadingChalkboard())
            {
                inventory.drawInventory(spriteBatch);
                
                foreach (InventoryItem i in inventory.getItems())
                {
                    if (i != null)
                    {
                        Rectangle r = i.getHitBox();

                        //var mousePosition = new Point();
                        if (r.Contains(cursorPosition) && (inventory.getSelectedItem() == null ||inventory.getSelectedItem().Equals(i)))
                        {
                            string tip = i.getItemTip();
                            spriteBatch.DrawString(inventory.getFont(), tip, new Vector2(inventory.getX(), inventory.getY() + 70), Color.Black);
                        }
                    }
                }
               
            }
            foreach (UpdateMiscellanious i in updateMiscObjects)
            {
                if (i is Chalkboard)
                {
                    if (player.isReadingChalkboard())
                    {
                        Chalkboard ch = (Chalkboard)i;
                        spriteBatch.Draw(i.getGraphic(), new Vector2(getCenter().X - i.getImageBoundingRectangle().Width / 2, getCenter().Y - i.getImageBoundingRectangle().Height / 2), i.getImageBoundingRectangle(), Color.White);
                        spriteBatch.DrawString(ch.getFont(), ch.getAnswer(), new Vector2(getCenter().X - ch.getAnswerWidth() / 2, getCenter().Y), Color.White);
                    }
                }
                else if (i is Box)
                {
                    Box box = (Box)i;
                    box.setX(getCenter().X - i.getImageBoundingRectangle().Width / 2);
                    box.setY(getCenter().Y - i.getImageBoundingRectangle().Height / 2 - 64);

                    box.drawBox(spriteBatch);



                    
                    //spriteBatch.DrawString(box.getFont(), box.getAnswer(), new Vector2(getCenter().X - box.getAnswerWidth() / 2, getCenter().Y), Color.White);
                }
                else
                {
                    spriteBatch.Draw(i.getGraphic(), new Vector2(i.getX(), i.getY()), i.getImageBoundingRectangle(), Color.White);
                }
            }
            if (showInventory && !player.isReadingChalkboard())
            {
                spriteBatch.Draw(Textures.textures["cursor"], cursorPosition, new Rectangle(0, 0, 32, 32), Color.White);
            }

            //rays of light juice and darkness for dark world/any other effects to draw over everything
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
            newKeyBoardState = Keyboard.GetState();
            if (newKeyBoardState.IsKeyDown(Keys.R) && !oldKeyBoardState.IsKeyDown(Keys.R))
            {
                reloadLevel();
                player.setPropotion(0.5);
                gameState = "play";
            }
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
                    inventory = new Inventory(this);
                    player = new Player(13*32, 25*32, inventory, this);
                    creatures.Add(player);
                    cam = new Camera(player, width, height);
                    cam.setBounds(currentLevel.getMapSizeX(), currentLevel.getMapSizeY());
                    currentLevel.unloadLevel();
                    player.setPropotion(0.5);
                    gameState = "play";
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

        public void winFadeUpdate()
        {
            playUpdate();
            
            if(fadeTransparency < 1)
            {
                fadeTransparency += 0.01f;
            }
            else
            {
                gameState = "credits";
            }
            
        }

        public void winFadeDraw()
        {
            
            playDraw();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, cam.getMatrix());
            spriteBatch.Draw(Textures.textures["whiteScreen"], new Vector2(getTopLeft().X, getTopLeft().Y), new Rectangle(0, 0, width, height), new Color(Color.Black, fadeTransparency));
            spriteBatch.End();
        }

        public void creditsUpdate()
        {
            creditsOffset++;
        }

        public void creditsDraw()
        {
            int length;
            string str;
            SpriteFont sf = Textures.fonts["mistral16"];

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, cam.getMatrix());
            str = "Isaac";
            length = str.Length * 12;
            spriteBatch.DrawString(sf, str, new Vector2(getCenter().X - length/2, getCenter().Y - creditsOffset), Color.Blue);
            str = "Merle";
            length = str.Length * 12;
            spriteBatch.DrawString(sf, str, new Vector2(getCenter().X - length / 2, getCenter().Y - creditsOffset + 15), Color.Blue);
            str = "Kevin";
            length = str.Length * 12;
            spriteBatch.DrawString(sf, str, new Vector2(getCenter().X - length / 2, getCenter().Y - creditsOffset + 30), Color.Blue);
            str = "Mayris";
            length = str.Length * 12;
            spriteBatch.DrawString(sf, str, new Vector2(getCenter().X - length / 2, getCenter().Y - creditsOffset + 39), Color.Blue);
            str = "Dreamshift";
            length = str.Length * 12;
            spriteBatch.DrawString(sf, str, new Vector2(getCenter().X - length / 2, getCenter().Y - creditsOffset + 300), Color.Blue);
            spriteBatch.End();
            creditsOffset++;
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

        public void boxUpdate()
        {
            MouseState mouseState = Mouse.GetState();

            if(player.isAccessingBox())
            {
                Box box = player.getBox();

                if (box.getStoredItem() == null) return;

                Vector2 cursorPosition = new Vector2(mouseState.X + getTopLeft().X, mouseState.Y + getTopLeft().Y);
                if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState2.LeftButton != ButtonState.Pressed)
                {
                    if (box.getStoredItem().getHitBox().Contains(cursorPosition.X, cursorPosition.Y))
                    {
                        box.setSelectedItem(true);
                    }
                }

                if (mouseState.LeftButton == ButtonState.Released)
                {
                    if (box.isItemSelected() && showInventory && !player.isReadingChalkboard())
                    {
                        if (inventory.addItemFromBox(box.getStoredItem()))
                        {
                            box.setStoredItem(null);
                        }
                        else
                        {
                            box.returnItemToSlot();
                        }
                    }

                    box.setSelectedItem(false);
                }

                if(box.isItemSelected())
                {
                    int curX = box.getStoredItem().getX();
                    int curY = box.getStoredItem().getY();

                    int deltaX = mouseState.X - oldMouseState2.X;
                    int deltaY = mouseState.Y - oldMouseState2.Y;
                    box.getStoredItem().setX(curX + deltaX);
                    box.getStoredItem().setY(curY + deltaY);
                }

             }
             oldMouseState2 = mouseState;

        }

        public void inventoryUpdate()
        {
            Texture2D tex = Textures.textures["inventory2"];
            inventory.setX(getTopRight().X - tex.Width - 10);
            inventory.setY(getTopLeft().Y + 10);
            if (showInventory && !player.isReadingChalkboard())
            {
                MouseState mouseState = Mouse.GetState();
                Vector2 inventoryPosition = new Vector2(inventory.getX(), inventory.getY());


                Vector2 cursorPosition = new Vector2(mouseState.X + getTopLeft().X, mouseState.Y + getTopLeft().Y);
                foreach (InventoryItem i in inventory.getItems())
                {
                    if (i != null)
                    {
                        Rectangle r = new Rectangle(i.getX() + (int)(inventoryPosition.X + 0.5), i.getY() + (int)(inventoryPosition.Y + 0.5), 32, 32);
                        //Rectangle boxRect = new Rectangle(i.getX() + (int)(inventoryPosition.X + 0.5), i.getY() + (int)(inventoryPosition.Y + 0.5), 32, 32);
                        if (mouseState.LeftButton == ButtonState.Released)
                        {
                            if(i.isSelected())
                            {
                                if(player.isAccessingBox())
                                {
                                    Rectangle rhitbox = new Rectangle(player.getBox().getX(), player.getBox().getY(), 100, 100);
                                    if(rhitbox.Contains(cursorPosition.X, cursorPosition.Y))
                                    {
                                        if(player.getBox().setStoredItem(i))
                                        {
                                            inventory.removeItem(i);
                                        }
                                        else
                                        {
                                            inventory.setNewIndex(i);
                                        }
                                        
                                    }
                                    else
                                    {
                                        inventory.setNewIndex(i);
                                    }
                                }
                                else
                                {
                                    inventory.setNewIndex(i);
                                }
                               
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

        public static void chalkboardCommand(string s)
        {
            if(s.Equals("IM A BANANA"))
            {
                Console.WriteLine("It's true! I am a banana!");
            }
        }
        public void addItemToCurrentLevel(Item i)
        {
            currentLevel.addLevelItem(i);

        }

        public bool winCondition()
        {
            if(platforms.Count <= 0)
            {
                return false;
            }
            foreach(Platform p in platforms)
            {
                if(!p.isItemOnPlatform())
                {
                    return false;
                }
            }
            return true;
        }

        public static bool isBoatSpawned()
        {
            return boatSpawned;
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
