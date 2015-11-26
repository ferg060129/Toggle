using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Diagnostics;
namespace Toggle
{

    public class Game1 : Game
    {
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
        public static ArrayList particles = new ArrayList();

        public static Random random = new Random();

        //public static ArrayList collidableTiles = new ArrayList();
        public static ArrayList miscObjects = new ArrayList();
        public static ArrayList updateMiscObjects = new ArrayList();
        public static ArrayList playerActivateTiles = new ArrayList();
        public static ArrayList levelTiles = new ArrayList();

        string gameState;


        public List<Level> levels = new List<Level>();

        public static bool worldState = true;
        public static bool[,] wallArray;
        public static int[,] wallTileArray;
        public static double[,] darkTileArray;

        private bool continueButtonActive = true;

        TutorialLevel tutorialLevel;
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
        LevelTile hubLevelTile;

        MarshEnterLevel marshEnterLevel;
        Marsh1Level marsh1Level;
        Marsh2Level marsh2Level;
        MarshFinalLevel marshFinalLevel;
        int time;
        int width;
        int height;
        int screenWidth;
        int screenHeight;
        Player player;
        PlayerGhost playerGhost;
        //public static List<Game1> game = new List<Game1>();
        Camera cam;
        //Song song;
        //Song song2;
        Inventory inventory;
        bool showInventory;
        KeyboardState newKeyBoardState, oldKeyBoardState;
        MouseState oldMouseState, oldMouseState2;
        int oldMouseX, oldMouseY;
        int shiftCooldown = 0;
        int maxShiftCooldown = 10 * 5;
        float shiftLockScale = 1.0f;
        float shiftLockAlpha = 0.0f;
        int shiftLockFade = 0;
        int shiftLockJitterTime = 0;
        float fadeTransparency = 0.0f;
        int creditsOffset = 0;
        bool beginningExpositionShown = false;



        private string currentLevelString;
        private float blackScreenAlpha;
        private bool fadeDirection;

        private Vector2 exitButton2Position;
        private Vector2 restartButtonPosition;

        private Texture2D startButton;
        private Texture2D exitButton;
        private Texture2D screenDisplayed;
        private Texture2D laserColor;
        private int titleScreenPhase;

        Rectangle healthBar = new Rectangle(0, 0, 48, 48);

        //private SoundEffectInstance banditKing;
        private SoundEffectInstance zone1good;
        private SoundEffectInstance zone1bad;
        private SoundEffectInstance beep;

        bool draw = true;

        InventoryItem[,] backUpInventory;
        ArrayList backUpItems;
        bool[] backUpPlatformFilled;

        private RoseExpositionScreen roseExpositionScreen;
        private OpeningExpositionScreen openingExpositionScreen;
        private KnifeExpositionScreen knifeExpositionScreen;
        private DiaryExpositionScreen diaryExpositionScreen;
        private PlatformScreen platformScreen;
        private ShiftLockScreen shiftLockScreen;
        private InventoryScreen inventoryScreen;
        private StartScreen startScreen;
        private PauseScreen pauseScreen;
        private AboutScreen aboutScreen;
        private DanceScreen danceScreen;
        private DeadScreen  deadScreen;

        private Screen currentTextBoxScreen;

        public static Dictionary<string, Level> stringToLevel = new Dictionary<string, Level>();
        public static Dictionary<Level, string> levelToString = new Dictionary<Level, string>();



        public Game1()
        {
            titleScreenPhase = 0;
            time = 0;
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            //IsMouseVisible = true;


            //game.Add(this);
            //graphics.PreferredBackBufferWidth = 1400;

            //graphics.PreferredBackBufferHeight = 800;
            //graphics.ApplyChanges();

        }

        protected override void Initialize()
        {
            exitButton2Position = new Vector2((GraphicsDevice.Viewport.Width / 2) + 160, 400);
            restartButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) + 160, 400);
            base.Initialize();
            //width = Window.ClientBounds.Width;
            //height = Window.ClientBounds.Height;
            fadeDirection = false;
            blackScreenAlpha = 0;
            currentLevelString = "hub";
            lastEnteredLevelTile = new LevelTile(0, 0, "blackBlock", "blackBlock", "hubLevel", new Point(13 * 32, 25 * 32));
            hubLevelTile = new LevelTile(0, 0, "blackBlock", "blackBlock", "hubLevel", new Point(13 * 32, 25 * 32));
            showInventory = false;
            AllowAccessibilityShortcutKeys(false);
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }

        protected override void LoadContent()
        {

            startButton = Content.Load<Texture2D>(@"start");
            exitButton = Content.Load<Texture2D>(@"exit");


            width = GraphicsDevice.PresentationParameters.Bounds.Width;
            height = GraphicsDevice.PresentationParameters.Bounds.Height;
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Associate names in the dictionary with the graphics
            for (int x = 0; x < Textures.graphicNames.Length; x++)
            {
                Textures.textures.Add(Textures.graphicNames[x], Content.Load<Texture2D>(Textures.graphicNames[x]));
            }

            for (int x = 0; x < Textures.tileNames.Length; x++)
            {
                Textures.textures.Add(Textures.tileNames[x], Content.Load<Texture2D>("Tile/" + Textures.tileNames[x]));
            }
            screenDisplayed = Textures.textures["titleScreen3"];
            for (int x = 0; x < Textures.spritefonts.Length; x++)
            {
                Textures.fonts.Add(Textures.spritefonts[x], Content.Load<SpriteFont>(Textures.spritefonts[x]));
            }

            for (int x = 0; x < Textures.soundsNames.Length; x++)
            {
                Textures.sounds.Add(Textures.soundsNames[x], Content.Load<SoundEffect>(Textures.soundsNames[x]).CreateInstance());
            }

            levels.Add(tutorialLevel = new TutorialLevel());
            levels.Add(hubLevel = new HubLevel());
            levels.Add(houseLevel = new HouseLevel());
            levels.Add(schoolLevel = new SchoolLevel());
            levels.Add(gateLevel = new GateLevel());
            levels.Add(gate1Level = new Gate1Level());
            levels.Add(gate2Level = new Gate2Level());
            levels.Add(complex1Level = new Complex1());
            levels.Add(ghostTestLevel = new GhostTestLevel());
            levels.Add(laserTestLevel = new LaserTestLevel());
            levels.Add(laserIntroLevel = new LaserIntro());
            levels.Add(marshEnterLevel = new MarshEnterLevel());
            levels.Add(marsh1Level = new Marsh1Level());
            levels.Add(marsh2Level = new Marsh2Level());
            levels.Add(marshFinalLevel = new MarshFinalLevel());

            //normal is hubLevel, change only to test
            currentLevel = hubLevel;

            stringToLevel.Add("hubLevel", hubLevel);
            stringToLevel.Add("houseLevel", houseLevel);
            stringToLevel.Add("schoolLevel", schoolLevel);
            stringToLevel.Add("gateLevel", gateLevel);
            stringToLevel.Add("gate1Level", gate1Level);
            stringToLevel.Add("gate2Level", gate2Level);
            stringToLevel.Add("complex1Level", complex1Level);
            stringToLevel.Add("ghostTestLevel", ghostTestLevel);
            stringToLevel.Add("laserTestLevel", laserTestLevel);
            stringToLevel.Add("laserIntroLevel", laserIntroLevel);
            stringToLevel.Add("marshEnterLevel", marshEnterLevel);
            stringToLevel.Add("marsh1Level", marsh1Level);
            stringToLevel.Add("marsh2Level", marsh2Level);
            stringToLevel.Add("marshFinalLevel", marshFinalLevel);
            stringToLevel.Add("tutorialLevel", tutorialLevel);


            levelToString.Add(hubLevel, "hubLevel");
            levelToString.Add(houseLevel, "houseLevel");
            levelToString.Add(schoolLevel, "schoolLevel");
            levelToString.Add(gateLevel, "gateLevel");
            levelToString.Add(gate1Level, "gate1Level");
            levelToString.Add(gate2Level, "gate2Level");
            levelToString.Add(complex1Level, "complex1Level");
            levelToString.Add(ghostTestLevel, "ghostTestLevel");
            levelToString.Add(laserTestLevel, "laserTestLevel");
            levelToString.Add(laserIntroLevel, "laserIntroLevel");
            levelToString.Add(marshEnterLevel, "marshEnterLevel");
            levelToString.Add(marsh1Level, "marsh1Level");
            levelToString.Add(marsh2Level, "marsh2Level");
            levelToString.Add(marshFinalLevel, "marshFinalLevel");
            levelToString.Add(tutorialLevel, "tutorialLevel");
            //normal is hubLevel, change only to test
            // currentLevel = marshFinalLevel;
            currentLevel = hubLevel;
            inventory = new Inventory(this);
            //13 x 25 for hub level start
            //beep = Content.Load<SoundEffect>("beep").CreateInstance();
            //player = new Player(5 * 32, 32 * 5, inventory, this);
            //hub start
            player = new Player(13 * 32, 25 * 32, inventory, this);

            playerGhost = new PlayerGhost(0, 0);


            roseExpositionScreen = new RoseExpositionScreen(this);
            openingExpositionScreen = new OpeningExpositionScreen(this);
            knifeExpositionScreen = new KnifeExpositionScreen(this);
            diaryExpositionScreen = new DiaryExpositionScreen(this);
            platformScreen = new PlatformScreen(this);
            shiftLockScreen = new ShiftLockScreen(this);
            inventoryScreen = new InventoryScreen(this);
            startScreen = new StartScreen(this);
            aboutScreen = new AboutScreen(this);
            danceScreen = new DanceScreen(this, player);
            pauseScreen = new PauseScreen(this);
            deadScreen = new DeadScreen(this);
            //player = new Player(15*32, 2*32, inventory, this);
            //player = new Player(30 * 32, 9 * 32, inventory, this);
            cam = new Camera(player, width, height);
            creatures.Add(player);
            cam.update();



            //song = Content.Load<Song>("banditKing2");

            //SoundEffect banditKing_s = Content.Load<SoundEffect>("banditKing2");
            //banditKing = banditKing_s.CreateInstance();

            zone1good = Content.Load<SoundEffect>("zone4good").CreateInstance();
            zone1bad = Content.Load<SoundEffect>("zone4bad").CreateInstance();


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
            //Check whether there is a continue point
            continueButtonActive = saveFileExists();
            //MediaPlayer.IsRepeating = true;
            zone1good.Volume = .7f;
            zone1bad.Volume = 0;


            backUpInventory = inventory.getItemsCopy();
            backUpItems = (ArrayList)(currentLevel.getLevelItems()).Clone();
            backUpPlatformFilled = new bool[2];
            if (currentLevel.Equals(hubLevel))
                loadBackupPlatforms();
        }

        public void reloadLevel()
        {
            player.initialize();
            playerGhost.reset();
            worldState = true;
            //currentLevel.addInitialLevelItems();

            /*
            inventory.setInventoryItems(backUpInventory);
            currentLevel.setLevelItems(backUpItems);
            if(currentLevel.Equals(hubLevel))
            {
                for (int x = 0; x < backUpPlatformFilled.Length; x++)
                {
                    ((Platform)platforms[x]).setItemOnPlatform(backUpPlatformFilled[x]);
                    //remove boat if item is now missing from platform
                    if (!backUpPlatformFilled[x])
                    {
                        boatSpawned = false;
                    }
                }
            }
            
            setLevel(lastEnteredLevelTile);
            syncStates();
             * */

            inventory.clearInventory();
            currentLevel.reloadLevelItems();
            continueGame();
            syncStates();


            


        }
        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && gameState.Equals("credits"))
            {
                Exit();
            }
            switch (gameState)
            {
                case "start":
                case "help":
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
                case "about":
                    aboutUpdate();
                    break;
                case "textbox":
                    textBoxScreenUpdate();
                    break;
            }
            base.Update(gameTime);

        }

        public void checkCollisions()
        {
            foreach (Creature c in creatures)
            {
                Rectangle hitBox = c.getHitBox();
                foreach (Creature d in creatures)
                {
                    if (!c.Equals(d))
                    {
                        Rectangle hitBoxOther = d.getHitBox();
                        if (c.getHitBox().Intersects(hitBoxOther))
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
                                Console.WriteLine("buttonpressed");
                            }
                        }
                    }
                }
                foreach (Visual v in visuals)
                {
                    Rectangle hitBoxOther = v.getHitBox();
                    if (v is Lake)
                    {
                        if (c.getHitBox().Intersects(hitBoxOther))
                        {
                            c.reportCollision(v);
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

            foreach (PlayerActivateTile t in playerActivateTiles)
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
                    if (u is Boat)
                        gameState = "winfade";
                }
            }
            foreach (Platform p in platforms)
            {
                Rectangle hitBox = p.getHitBox();
                if (player.getHitBox().Intersects(hitBox))
                {
                    player.reportCollision(p);
                }
            }
            checkLevelTileCollision();
        }


        public void checkLevelTileCollision()
        {
            for (int x = 0; x < levelTiles.Count; x++)
            {

                Tile t = (Tile)(levelTiles[x]);
                Rectangle hitBox = t.getHitBox();
                if (player.getHitBox().Intersects(hitBox))
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
                case "tutorial":
                    break;
                case "start":
                case "help":
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
                case "about":
                    aboutDraw();
                    break;
                case "textbox":
                    textBoxScreenDraw();
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
            foreach (Platform p in platforms)
            {
                p.setState(worldState);
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
            for (int x = 0; x < inventoryItems.GetLength(0); x++)
            {
                for (int y = 0; y < inventoryItems.GetLength(1); y++)
                {
                    if (inventoryItems[x, y] != null)
                        inventoryItems[x, y].setState(worldState);
                }
            }
            foreach (Tile t in tiles)
            {
                t.setState(worldState);
            }
        }

        public void syncStates()
        {
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
            foreach (Platform p in platforms)
            {
                p.setState(worldState);
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
            for (int x = 0; x < inventoryItems.GetLength(0); x++)
            {
                for (int y = 0; y < inventoryItems.GetLength(1); y++)
                {
                    if (inventoryItems[x, y] != null)
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
            if (!draw)
            {
                draw = true;
                return;
            }
            foreach (Tile t in tiles)
            {
                int xLoc = t.getX();
                int yLoc = t.getY();
                if (((xLoc > player.getX() - width - 32) && (xLoc < player.getX() + width)) &&
                    ((yLoc > player.getY() - height - 32) && (yLoc < player.getY() + height)))
                {
                    if (isOnScreen(t.getHitBox()))
                    {
                        spriteBatch.Draw(t.getGraphic(), new Vector2(xLoc, yLoc), new Rectangle(0, 0, 32, 32), Color.White);
                        if (t is PlayerActivateTile || t is Grate)
                            aboutScreen.addSeenObject(t);
                    }

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
                    if (lamp != null && lamp.hasBatteries())
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

            player.setLocked(false);
            backUpInventory = inventory.getItemsCopy();

            lastEnteredLevelTile = level;
            Console.WriteLine(level.getLevel());
            currentLevelString = level.getLevel();
            //Eventually turn level strings into global constants
            if (currentLevel != null)
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
            else if (currentLevelString.Equals("marshEnterLevel"))
            {
                currentLevel = marshEnterLevel;
            }
            else if (currentLevelString.Equals("marsh1Level"))
            {
                currentLevel = marsh1Level;
            }
            else if (currentLevelString.Equals("marsh2Level"))
            {
                currentLevel = marsh2Level;
            }
            else if (currentLevelString.Equals("marshFinalLevel"))
            {
                currentLevel = marshFinalLevel;
            }
            creatures.Add(player);
            currentLevel.loadLevel();

            backUpItems = (ArrayList)(currentLevel.getLevelItems()).Clone();

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
            if (level.getLevel().Equals("hubLevel"))
                loadBackupPlatforms();
            saveGame(level);
            syncStates();
        }

        //For each game state
        public void startUpdate()
        {
            newKeyBoardState = Keyboard.GetState();
            if (titleScreenPhase == 0)
            {
                MouseState mouseState = Mouse.GetState();
                int mouseX, mouseY;

                Point p = convertCursorLocation(mouseState);
                mouseX = p.X;
                mouseY = p.Y;

                startScreen.checkButtonHovers();
                startScreen.checkButtonClicks();
                //if we set state to start from a button
                /*
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
                 * */
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
                || (newKeyBoardState.IsKeyDown(Keys.RightShift) && oldKeyBoardState != null && !oldKeyBoardState.IsKeyDown(Keys.RightShift))){
                    screenDisplayed = Textures.textures["hourglassinstruction"];
                    titleScreenPhase++;
                    oldKeyBoardState = newKeyBoardState;
                }
            }
            if (titleScreenPhase == 3)
            {
                if ((newKeyBoardState.IsKeyDown(Keys.LeftShift) && oldKeyBoardState != null && !oldKeyBoardState.IsKeyDown(Keys.LeftShift))|| 
                    (newKeyBoardState.IsKeyDown(Keys.RightShift) && oldKeyBoardState != null && !oldKeyBoardState.IsKeyDown(Keys.RightShift)))
                {
                    if(!beginningExpositionShown)
                    {
                        setState("textbox", "beginningExposition");
                        beginningExpositionShown = true;
                    }
                    else
                    {
                        gameState = "play";
                    }

                    LevelTile lv = new LevelTile(0, 0, "blackBlock", "blackBlock", "hubLevel", new Point(13 * 32, 25 * 32));
                    saveGame(lv);
                    //screenDisplayed = Textures.textures["inventorytutorial"];
                }
            }
            if (newKeyBoardState.IsKeyUp(Keys.LeftShift) && newKeyBoardState.IsKeyUp(Keys.RightShift))
            {
                oldKeyBoardState = new KeyboardState();
            }
        }

        public void startDraw()
        {
            MouseState mouseState = Mouse.GetState();
            int mouseX, mouseY;
            Point p = convertCursorLocation(mouseState);
            mouseX = p.X;
            mouseY = p.Y;
            Vector2 cursorPosition = new Vector2(mouseX, mouseY);

            spriteBatch.Begin();
            spriteBatch.Draw(screenDisplayed, new Vector2(0, 0), Color.White);
            if (titleScreenPhase == 0)
            {
                startScreen.drawScreen(spriteBatch);
            }

            spriteBatch.Draw(Textures.textures["cursor"], cursorPosition, new Rectangle(0, 0, 16, 16), Color.White);
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
            foreach (UpdateMiscellanious i in updateMiscObjects)
            {
                i.move();
            }
            //update particles and remove dead ones
            ArrayList particlesToRemove = new ArrayList();
            foreach (Particle p in particles)
            {
                p.update();
                if (p.getLifetime() <= 0)
                {
                    particlesToRemove.Add(p);
                }
            }
            foreach (Particle q in particlesToRemove)
            {
                particles.Remove(q);
            }
            player.moveUpdate();

            checkCollisions();


            cam.update();
            //boxUpdate();
            inventoryUpdate();

            if (shiftCooldown > 0)
            {
                shiftCooldown--;
            }
            if (player.isDead())
            {
                if (!playerGhost.isActive())
                {
                    playerGhost.setX(player.getX());
                    playerGhost.setY(player.getY());
                    playerGhost.activate();
                }
                playerGhost.move();
                //uncomment these two for standard no-animation death
                //gameState = "lost";
                //IsMouseVisible = true;
            }

            if (playerGhost.getTimeAlive() <= 0)
            {
                gameState = "lost";
                //game cursor works now so should be fine
                //IsMouseVisible = true;
                playerGhost.reset();
            }


            if (((newKeyBoardState.IsKeyDown(Keys.Escape) && !oldKeyBoardState.IsKeyDown(Keys.Escape)) || (newKeyBoardState.IsKeyDown(Keys.P) && !oldKeyBoardState.IsKeyDown(Keys.P))) && !player.isReadingChalkboard())
            {
                gameState = "pause";
            }
            else if (newKeyBoardState.IsKeyDown(Keys.A) && !oldKeyBoardState.IsKeyDown(Keys.A) && !player.isReadingChalkboard())
            {
                gameState = "about";
            }
            else if (newKeyBoardState.IsKeyDown(Keys.R) && !oldKeyBoardState.IsKeyDown(Keys.R) && !player.isReadingChalkboard())
            {
                reloadLevel();
            }
            else if (newKeyBoardState.IsKeyDown(Keys.I) && !oldKeyBoardState.IsKeyDown(Keys.I) && !player.isReadingChalkboard())
            {
                showInventory = !showInventory;
            }

            if (winCondition() && !boatSpawned())
            {

                Boat boat = new Boat(40 * 32, 28 * 32, new Point(28 * 32, 28 * 32));
                Game1.updateMiscObjects.Add(boat);
                //hubLevel.addLevelItem(boat);
            }



            oldKeyBoardState = newKeyBoardState;
        }

        public void playLaserDraw()
        {
            int[] tempArray = new int[4];
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
                                (float)Math.PI / 2, new Vector2(0, 0), SpriteEffects.None, 0);
                        spriteBatch.Draw(laserColor,
                            new Rectangle(l.getX() + 8, l.getY(), Math.Abs(tempArray[2] - l.getY()), 16),
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
            Point po = convertCursorLocation(mouseState);
            int mouseX = po.X;
            int mouseY = po.Y;

            drawMap(spriteBatch);

            foreach (Platform p in platforms)
            {
                if (isOnScreen(p.getHitBox()))
                {
                    spriteBatch.Draw(p.getGraphic(), new Vector2(p.getX(), p.getY()), p.getImageBoundingRectangle(), Color.White);
                    aboutScreen.addSeenObject(p);
                }


            }

            foreach (Item i in items)
            {
                spriteBatch.Draw(i.getGraphic(), new Vector2(i.getX(), i.getY()), i.getImageBoundingRectangle(), Color.White);
            }
            foreach (Creature c in creatures)
            {
                Rectangle r = new Rectangle(c.getX(), c.getY(), c.getGraphic().Width, c.getGraphic().Height);
                if (isOnScreen(r))
                {
                    spriteBatch.Draw(c.getGraphic(), new Vector2(c.getX(), c.getY()), c.getImageBoundingRectangle(), Color.White * c.getAlpha());
                    aboutScreen.addSeenObject(c);
                }

            }
            foreach (Miscellanious m in miscObjects)
            {

                if (isOnScreen(m.getHitBox()))
                {
                    spriteBatch.Draw(m.getGraphic(), new Vector2(m.getX(), m.getY()), m.getImageBoundingRectangle(), Color.White);
                    aboutScreen.addSeenObject(m);
                }
            }
            playLaserDraw();
            foreach (Visual v in visuals)
            {
                spriteBatch.Draw(v.getGraphic(), new Vector2(v.getX(), v.getY()), v.getImageBoundingRectangle(), Color.White);
            }
            foreach (Particle p in particles)
            {
                spriteBatch.Draw(p.getGraphic(),
                                 p.getPosition(), p.getImageBoundingRectangle(),
                                 p.getColor() * p.getAlpha(),
                                 p.getRotation(), new Vector2(16, 16), p.getScale(), SpriteEffects.None, 0);
            }


            //Duct tape to draw boat under player
            foreach (UpdateMiscellanious i in updateMiscObjects)
            {
                if (i is Boat)
                {
                    spriteBatch.Draw(i.getGraphic(), new Vector2(i.getX(), i.getY()), i.getImageBoundingRectangle(), Color.White);
                }
            }

            spriteBatch.Draw(player.getGraphic(), new Vector2(player.getX(), player.getY()), player.getImageBoundingRectangle(), Color.White);


            
            if (!worldState)
                drawDarkTiles(spriteBatch);

            //Debug, draw player coords
            //spriteBatch.DrawString(Textures.fonts["mistral16"], player.getX() / 32 + " " + player.getY() / 32, new Vector2(player.getX(), player.getY() - 12), Color.Black);
            //spriteBatch.Draw(player.getGraphic(), new Vector2(player.getX(), player.getY()), player.getImageBoundingRectangle(), Color.White);
            Vector2 cursorPosition = new Vector2(mouseX + getTopLeft().X, mouseY + getTopLeft().Y);
            if (showInventory && !player.isReadingChalkboard())
            {

                if (inventory.getInventoryRectangle().Contains(cursorPosition) || inventory.getSelectedItem()!= null)
                {
                    inventory.setTransparent(false);
                }
                else
                {
                    inventory.setTransparent(true);
                }
                

                inventory.drawInventory(spriteBatch);



                foreach (InventoryItem i in inventory.getItems())
                {
                    if (i != null)
                    {
                        Rectangle r = i.getHitBox();

                        //var mousePosition = new Point();
                        if (r.Contains(cursorPosition) && (inventory.getSelectedItem() == null || inventory.getSelectedItem().Equals(i)))
                        {
                            string tip = i.getItemTip();
                            spriteBatch.DrawString(inventory.getFont(), tip, new Vector2(inventory.getX() + 5, inventory.getY() + 75), Color.White);
                        }
                    }
                }

            }
            foreach (UpdateMiscellanious i in updateMiscObjects)
            {
                //Duct tape to draw boat under player
                if (i is Boat) continue;
                if (i is Chalkboard)
                {
                    if (player.isReadingChalkboard())
                    {
                        Chalkboard ch = (Chalkboard)i;
                        spriteBatch.Draw(i.getGraphic(), new Vector2(getCenter().X - i.getImageBoundingRectangle().Width / 2, getCenter().Y - i.getImageBoundingRectangle().Height / 2), i.getImageBoundingRectangle(), Color.White);
                        spriteBatch.DrawString(ch.getFont(), ch.getAnswer(), new Vector2(getCenter().X - ch.getAnswerWidth() / 2, getCenter().Y), Color.White);
                    }
                }
                else
                {
                    spriteBatch.Draw(i.getGraphic(), new Vector2(i.getX(), i.getY()), i.getImageBoundingRectangle(), Color.White);
                }
            }
            if (showInventory && !player.isReadingChalkboard())
            {
                spriteBatch.Draw(Textures.textures["cursor"], cursorPosition, new Rectangle(0, 0, 16, 16), Color.White);
            }
            //draw player ghost over most things on death
            if (playerGhost.isActive())
                spriteBatch.Draw(playerGhost.getGraphic(), new Vector2(playerGhost.getX(), playerGhost.getY()), new Rectangle(0, 0, 32, 32), Color.White * playerGhost.getAlpha());

            //rays of light juice and darkness for dark world/any other effects to draw over everything
            spriteBatch.Draw(Textures.textures["shadowScreen"], new Vector2(-cam.getX() - width / 2, -cam.getY() - (height / 2) + (((float)Math.Sin(time * 3.14529 / 180) + 1.0f) * 40)), new Rectangle(0, 0, 800, 640), Color.White * 0.7f);
            if (worldState)
            {
                if (currentLevel.getIndoors() == false)
                {
                    spriteBatch.Draw(Textures.textures["rays"], new Vector2(-cam.getX() - width / 2, -cam.getY() - height / 2), new Rectangle(0, 0, 800, 640), Color.White * ((float)Math.Sin(time / 2 * 3.14529 / 180) / 4));
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
            pauseScreen.checkButtonHovers();
            pauseScreen.checkButtonClicks();
            if ((newKeyBoardState.IsKeyDown(Keys.Escape) && !oldKeyBoardState.IsKeyDown(Keys.Escape)) || (newKeyBoardState.IsKeyDown(Keys.P) && !oldKeyBoardState.IsKeyDown(Keys.P)))
            {
                gameState = "play";
            }
            oldKeyBoardState = newKeyBoardState;
        }
        public void pauseDraw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Textures.textures["pause"], new Vector2(0, 0), new Rectangle(0, 0, 800, 640), Color.White);
            pauseScreen.drawScreen(spriteBatch);
            spriteBatch.End();
        }
        public void aboutUpdate()
        {
            newKeyBoardState = Keyboard.GetState();
            aboutScreen.checkButtonHovers();
            aboutScreen.checkButtonClicks();
            if (newKeyBoardState.IsKeyDown(Keys.A) && !oldKeyBoardState.IsKeyDown(Keys.A))
            {
                gameState = "play";
            }
            oldKeyBoardState = newKeyBoardState;
        }
        public void aboutDraw()
        {
            spriteBatch.Begin();
            aboutScreen.drawScreen(spriteBatch);
            spriteBatch.End();
        }


        public void lostUpdate()
        {
            MouseState mouseState = Mouse.GetState();
            int mouseX, mouseY;

            Point p = convertCursorLocation(mouseState);
            mouseX = p.X;
            mouseY = p.Y;

            deadScreen.checkButtonHovers();
            deadScreen.checkButtonClicks();


            newKeyBoardState = Keyboard.GetState();
            if (newKeyBoardState.IsKeyDown(Keys.R) && !oldKeyBoardState.IsKeyDown(Keys.R))
            {
                reloadLevel();
                gameState = "play";
            }
            MouseState m = Mouse.GetState();
            /*
            if (m.LeftButton == ButtonState.Pressed)
            {
                Rectangle replayButtonRect = new Rectangle(538, 381, 140, 50);
                Rectangle exitButtonRect = new Rectangle(544, 483, 90, 45);
                //Rectangle startButtonRect = new Rectangle((int)startButtonPosition.X,
                //                   (int)startButtonPosition.Y, 160, 64);
                //Rectangle exitButtonRect = new Rectangle((int)exitButtonPosition.X,
                //                  (int)exitButtonPosition.Y, 160, 64);
                // Console.Write(m.X+" "+m.Y+"\n");
                MouseState mouseState = Mouse.GetState();
                int mouseX, mouseY;
                Point p = convertCursorLocation(mouseState);
                mouseX = p.X;
                mouseY = p.Y;


                if (replayButtonRect.Contains(new Vector2(m.X, m.Y)))
                {
                    //setLevel(hubLevel);
                    gameState = "play";
                    worldState = true;
                    currentLevel.unloadLevel();

                    currentLevel = hubLevel;
                    currentLevel.loadLevel();
                    inventory = new Inventory(this);
                    player = new Player(13 * 32, 25 * 32, inventory, this);
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
             * */


        }
        public void lostDraw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Textures.textures["deadScreen"], new Vector2(0, 0), Color.White);
            deadScreen.drawScreen(spriteBatch);
            spriteBatch.End();
        }

        public void textBoxScreenUpdate()
        {
            newKeyBoardState = Keyboard.GetState();
            currentTextBoxScreen.checkButtonHovers();
            currentTextBoxScreen.checkButtonClicks();
            if ((newKeyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState != null && !oldKeyBoardState.IsKeyDown(Keys.Enter)))
            {
                ((TextBoxScreen)currentTextBoxScreen).nextString();
            }
            oldKeyBoardState = newKeyBoardState;
            
        }

        public void textBoxScreenDraw()
        {
            playDraw();

            spriteBatch.Begin();
            spriteBatch.Draw(screenDisplayed, new Vector2(0, 0), Color.White);
            currentTextBoxScreen.drawScreen(spriteBatch);
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

            spriteBatch.Draw(Textures.textures["shiftCooldownBar"], new Vector2(shiftCDLocation.X + 2, shiftCDLocation.Y + 2), r, Color.White);
            spriteBatch.Draw(Textures.textures["shiftCooldown"], shiftCDLocation, Color.White);
            if (player.isLocked())
            {
                //Vector2 loc = new Vector2(shiftCDLocation.X + 128 / 2 - 8, shiftCDLocation.Y);
                //spriteBatch.Draw(Textures.textures["shiftlocked"], loc, Color.White);
                shiftLockFade = 0;
                drawShiftCDLock(true);
            }
            else
            {
                if (shiftLockFade < 20)
                {
                    drawShiftCDLock(false);
                    shiftLockFade += 2;
                }
                else if (shiftLockFade == 20)
                {
                    shiftLockFade += 2;
                    shiftLockJitterTime = 0;
                    shiftLockScale = 5.0f;
                    shiftLockAlpha = 0.0f;
                }
            }
        }

        public void drawShiftCDLock(bool fadein)
        {
            if (fadein)
            {
                if (shiftLockAlpha < 1.0)
                {
                    shiftLockAlpha += 0.04f;
                }
            }
            else
            {
                shiftLockAlpha -= 0.1f;
            }

            if (shiftLockScale > 1.0)
            {
                shiftLockScale -= 0.5f;
            }
            else
                shiftLockScale = 1.0f;
            Vector2 shiftCDLocation = new Vector2(getTopLeft().X + 10, getTopLeft().Y + 10);
            int jitterX = 0;
            int jitterY = 0;
            if (shiftLockJitterTime > 0)
            {
                shiftLockJitterTime--;
                jitterX = random.Next(5) - 2;
                jitterY = random.Next(5) - 2;
            }
            Vector2 loc;
            if (player.isLocked())
                loc = new Vector2(shiftCDLocation.X + 128 / 2 + jitterX, shiftCDLocation.Y + 8 + jitterY);
            else
                loc = new Vector2(shiftCDLocation.X + 128 / 2, shiftCDLocation.Y + 8 + shiftLockFade);
            //spriteBatch.Draw(Textures.textures["shiftlocked"], loc, Color.White);
            spriteBatch.Draw(Textures.textures["shiftlocked"],
                            loc, null,
                                Color.White * shiftLockAlpha,
                                0, new Vector2(14, 14), shiftLockScale, SpriteEffects.None, 0);

        }

        //called in player, to jitter on keypress
        public void jitterLock()
        {
            if (player.isLocked())
            {
                shiftLockJitterTime = 15;
            }

        }

        public void winFadeUpdate()
        {
            playUpdate();

            if (fadeTransparency < 1)
            {
                fadeTransparency += 0.01f;
            }
            else
            {
                gameState = "credits";
                player.setOnBoat(false);
                player.setPosition(new Vector2((width / 64) * 32, (height / 64) * 32));
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
            player.playerOtherMove();
            if (zone1good.State != SoundState.Playing && zone1bad.State != SoundState.Playing)
            {
                zone1good.Play();
                zone1bad.Play();
            }
        }

        public void creditsDraw()
        {

            int length;
            string str;
            SpriteFont sf = Textures.fonts["mistral16"];

            spriteBatch.Begin();

            danceScreen.drawDanceSreen(spriteBatch);
            spriteBatch.Draw(player.getGraphic(), new Vector2(player.getX(), player.getY()), player.getImageBoundingRectangle(), Color.White);
            str = "Isaac Dickson";
            length = str.Length * 12;
            spriteBatch.DrawString(sf, str, new Vector2(width / 2 - length / 2, height / 2 - creditsOffset), Color.Blue);
            str = "Merle Ferguson";
            length = str.Length * 12;
            spriteBatch.DrawString(sf, str, new Vector2(width / 2 - length / 2, height / 2 - creditsOffset + 400), Color.Blue);
            str = "Kevin Spies";
            length = str.Length * 12;
            spriteBatch.DrawString(sf, str, new Vector2(width / 2 - length / 2, height / 2 - creditsOffset + 800), Color.Blue);
            str = "Mayris Rios";
            length = str.Length * 12;
            spriteBatch.DrawString(sf, str, new Vector2(width / 2 - length / 2, height / 2 - creditsOffset + 1200), Color.Blue);
            str = "Dreamshift";
            length = str.Length * 12;
            spriteBatch.DrawString(sf, str, new Vector2(width / 2 - length / 2, height / 2 - creditsOffset + 1800), Color.Blue);
            creditsOffset++;


            spriteBatch.End();
        }


        public void drawHealthBar()
        {
            int rectHeight;
            Color rectColor = Color.White;
            Texture2D rectTop, rectBottom = null;
            Vector2 healthBarLocation = new Vector2(-cam.getX() - width / 2 + 10, -cam.getY() - height / 2 + 32);
            if (!player.isDead())
                rectHeight = (int)(player.getProportion() * healthBar.Height);
            else
            {
                rectHeight = 0;
                rectColor = Color.Red;
            }

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
            spriteBatch.Draw(rectTop, healthBarLocation, new Rectangle(0, 0, rectBottom.Width, (healthBar.Height - rectHeight)), rectColor);
            spriteBatch.Draw(rectBottom, new Vector2(healthBarLocation.X, healthBarLocation.Y + (healthBar.Height - rectHeight)), new Rectangle(0, 0, rectBottom.Width, rectHeight), rectColor);

            if ((player.getProportion() < 0.20) && (time % 50 >= 45) && (!player.isDead()))
            {
                spriteBatch.Draw(Textures.textures["hourglass2"], healthBarLocation, Color.White);
            }
            else
            {
                spriteBatch.Draw(Textures.textures["hourglass2"], healthBarLocation, Color.Black);
            }
        }
        /*
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

        }*/

        public void inventoryUpdate()
        {
            Texture2D tex = Textures.textures["inventory2"];
            inventory.setX(getTopRight().X - tex.Width - 10);
            inventory.setY(getTopLeft().Y + 10);
            if (showInventory && !player.isReadingChalkboard())
            {
                MouseState mouseState = Mouse.GetState();
                Point p = convertCursorLocation(mouseState);
                int mouseX = p.X;
                int mouseY = p.Y;


                Vector2 inventoryPosition = new Vector2(inventory.getX(), inventory.getY());


                Vector2 cursorPosition = new Vector2(mouseX + getTopLeft().X, mouseY + getTopLeft().Y);
                foreach (InventoryItem i in inventory.getItems())
                {
                    if (i != null)
                    {
                        Rectangle r = new Rectangle(i.getX() + (int)(inventoryPosition.X + 0.5), i.getY() + (int)(inventoryPosition.Y + 0.5), 32, 32);
                        //Rectangle boxRect = new Rectangle(i.getX() + (int)(inventoryPosition.X + 0.5), i.getY() + (int)(inventoryPosition.Y + 0.5), 32, 32);
                        if (mouseState.LeftButton == ButtonState.Released)
                        {
                            if (i.isSelected())
                            {
                                if (player.isAccessingBox())
                                {
                                    Rectangle rhitbox = new Rectangle(player.getBox().getX(), player.getBox().getY(), 100, 100);
                                    if (rhitbox.Contains(cursorPosition.X, cursorPosition.Y))
                                    {
                                        if (player.getBox().setStoredItem(i))
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

                            int deltaX = mouseX - oldMouseX;
                            int deltaY = mouseY - oldMouseY;


                            i.setX(curX + deltaX);
                            i.setY(curY + deltaY);

                        }
                    }
                }
                oldMouseState = mouseState;
                oldMouseX = mouseX;
                oldMouseY = mouseY;
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
            if (s.Equals("IM A BANANA"))
            {
                Console.WriteLine("It's true! I am a banana!");
            }
        }
        /*
        public void addItemToCurrentLevel(Item i)
        {
            currentLevel.addLevelItem(i);

        }*/

        public bool winCondition()
        {
            if (platforms.Count <= 0)
            {
                return false;
            }
            foreach (Platform p in platforms)
            {
                if (!p.isItemOnPlatform())
                {
                    return false;
                }
            }
            return true;
        }

        public Point convertCursorLocation(MouseState m)
        {
            int xLoc, yLoc;
            if (graphics.IsFullScreen)
            {
                xLoc = (int)(m.X / GraphicsDevice.Viewport.AspectRatio + 0.5);
                yLoc = (int)(m.Y / GraphicsDevice.Viewport.AspectRatio + 0.5);
            }
            else
            {
                xLoc = (int)(m.X + 0.5);
                yLoc = (int)(m.Y + 0.5);
            }

            return new Point(xLoc, yLoc);
        }

        public bool isOnScreen(Rectangle r)
        {

            Rectangle camRectangle = new Rectangle(getTopLeft().X, getTopLeft().Y, width, height);
            if (r.Intersects(camRectangle))
            {
                return true;
            }
            return false;


        }

        public int getScreenWidth()
        {
            return width;
        }
        public int getScreenHeight()
        {
            return height;
        }

        public void setState(string state, string arg2)
        {
            gameState = state;
            if (gameState == "start")
            {
                titleScreenPhase = 0;
                screenDisplayed = Textures.textures["titleScreen3"];
            }
            if (gameState == "help")
            {
                titleScreenPhase = 2;
            }

            if (gameState == "textbox")
            {
               switch(arg2)
               {
                   case "inventory":
                       currentTextBoxScreen = inventoryScreen;
                       showInventory = true;
                       break;
                   case "shiftLock":
                       currentTextBoxScreen = shiftLockScreen;
                       break;
                   case "platform":
                       currentTextBoxScreen = platformScreen;
                       break;
                   case "beginningExposition":
                       currentTextBoxScreen = openingExpositionScreen;
                       break;
                   case "diaryExposition":
                       currentTextBoxScreen = diaryExpositionScreen;
                       break;
                   case "knifeExposition":
                       currentTextBoxScreen = knifeExpositionScreen;
                       break;
                   case "roseExposition":
                       currentTextBoxScreen = roseExpositionScreen;
                       break;
               }
               screenDisplayed = Textures.textures["inventorytutorial"];
                
            }
        }

        public void saveGame(LevelTile level)
        {
            StreamWriter saveFile = new StreamWriter("bananas.txt");

            //Remove specified items from particular levels
            foreach (Level l in levels)
            {
                saveFile.WriteLine("L:" + levelToString[l]);

                string[] itemStrings = l.getRemovedLevelItems();

                for (int x = 0; x < itemStrings.Length; x++)
                {
                    saveFile.WriteLine("I:" + itemStrings[x]);
                }

                foreach (Platform p in platforms)
                {
                    string str = "F:" + p.GetType().Name;
                    if (((Platform)p).isItemOnPlatform())
                    {
                        str += "|T";
                    }
                    else
                    {
                        str += "|F";
                    }
                }
            }

            //Load which objects have been seen and which haven't
            ArrayList seenObjects = aboutScreen.getSeenObjects();
            for (int x = 0; x < seenObjects.Count; x++)
            {
                saveFile.WriteLine("S:" + seenObjects[x]);
            }

            //Load the inventory
            for (int x = 0; x < backUpInventory.GetLength(0); x++)
            {
                for (int y = 0; y < backUpInventory.GetLength(1); y++)
                {
                    if (backUpInventory[x, y] == null) continue;

                    string str = backUpInventory[x, y].GetType().Name;
                    str += "|" + x + "|" + y;

                    if (backUpInventory[x, y] is ScrollI)
                    {
                        str += "|" + ((ScrollI)backUpInventory[x, y]).getGoodItemTip() + "|" + ((ScrollI)backUpInventory[x, y]).getBadItemTip();
                    }
                    else if (backUpInventory[x, y] is LampI)
                    {
                        if (((LampI)backUpInventory[x, y]).hasBatteries())
                        {
                            str += "|T";
                        }
                        else
                        {
                            str += "|F";
                        }
                    }
                    saveFile.WriteLine("N:" + str);
                }
            }

            saveFile.WriteLine(levelToString[currentLevel]);

            saveFile.WriteLine("P:" + level.getPlayerStart().X + "|" + level.getPlayerStart().Y);

            saveFile.WriteLine("C:" + levelToString[currentLevel]);

            // if (level.Equals("hubLevel"))
            // {
            foreach (Platform p in platforms)
            {
                string str = "F:"+p.GetType().Name;
                if (((Platform)p).isItemOnPlatform())
                {
                    str += "|T";
                }
                else
                {
                    str += "|F";
                }
                saveFile.WriteLine(str);
            }
            //  }




            saveFile.Close();
        }

        public void continueGame()
        {
            StreamReader saveFile = new StreamReader("bananas.txt");
            string line;
            Level currentReadLevel = null;
            string tempLine = "";
            int px = 0, py = 0;
            string loadLevel = "";
            while ((line = saveFile.ReadLine()) != null)
            {
                tempLine = line.Substring(2, line.Length - 2);
                string[] args = tempLine.Split('|');
                //complete
                if (line[0] == 'L')
                {
                    currentReadLevel = stringToLevel[tempLine];
                }
                if (line[0] == 'I')
                {
                    currentReadLevel.removeLevelItem(tempLine);
                }
                if (line[0] == 'P')
                {
                    //Add player at location
                    px = Int32.Parse(args[0]);
                    py = Int32.Parse(args[1]);
                }
                if (line[0] == 'N')
                {
                    int itemX = Int32.Parse(args[1]);
                    int itemY = Int32.Parse(args[2]);
                    switch (args[0])
                    {
                        case "BatteryGooI":
                            inventory.setInventoryItem(new BatteryGooI(), itemX, itemY);
                            break;
                        case "KnifeI":
                            inventory.setInventoryItem(new KnifeI(), itemX, itemY);
                            break;
                        case "DiaryI":
                            inventory.setInventoryItem(new DiaryI(), itemX, itemY);
                            break;
                        case "LampI":
                            LampI lamp = new LampI();
                            if (args[3].Equals("T"))
                            {
                                lamp.setBatteries(true);
                            }
                            inventory.setInventoryItem(lamp, itemX, itemY);
                            break;
                        case "RopeI":
                            inventory.setInventoryItem(new RopeI(), itemX, itemY);
                            break;
                        case "RoseI":
                            inventory.setInventoryItem(new RoseI(), itemX, itemY);
                            break;
                        case "ScrollI":
                            inventory.setInventoryItem(new ScrollI(args[3], args[4]), itemX, itemY);
                            break;
                    }
                }
                if (line[0] == 'S')
                {
                    aboutScreen.addSeenObject(tempLine);
                }
                //complete
                if (line[0] == 'F')
                {
                    bool platformComplete = (args[1].Equals("T"));

                    findPlatformGivenString(args[0]).setItemOnPlatform(platformComplete);
                }
                if (line[0] == 'C')
                {
                    loadLevel = tempLine;
                }
            }
            LevelTile lv = new LevelTile(0, 0, "blackBlock", "blackBlock", loadLevel, new Point(px, py));
            saveFile.Close();
            gameState = "play";

            setLevel(lv);

        }

        public void loadBackupPlatforms()
        {
            for (int x = 0; x < backUpPlatformFilled.Length; x++)
            {
                if (((Platform)platforms[x]).isItemOnPlatform())
                {
                    backUpPlatformFilled[x] = true;
                }
                else
                {
                    backUpPlatformFilled[x] = false;
                }
            }
        }

        public void buttonCommand(string command)
        {
            if (command.Equals("play"))
            {
                //make sure level is back to start
                currentLevel = hubLevel;
                currentLevelString = "hubLevel";
                inventory.clearInventory();
                setLevel(hubLevelTile);
                reloadLevel();
                setState("start", "");
                titleScreenPhase = 1;
                screenDisplayed = Textures.textures["controls1"];
            }
            if (command.Equals("help"))
            {
                setState("start", "");
                titleScreenPhase = 1;
                screenDisplayed = Textures.textures["controls1"];
            }
            if (command.Equals("continue"))
            {
                //Do a try catch block to return to start screen if it fails
                if (continueButtonPressable())
                {
                    if(inventory!= null)
                    inventory.clearInventory();
                    if (currentLevel!=null)
                    currentLevel.reloadLevelItems();
                    continueGame();
                }

            }
            if (command.Equals("exit"))
            {
                Exit();
            }
            if (command.Equals("reload"))
            {
                reloadLevel();
                gameState = "play";
            }
            if (command.Equals("startscreen"))
            {
                refreshAllItems();
                //reloadLevel();
                gameState = "start";
                titleScreenPhase = 0;
                screenDisplayed = Textures.textures["titleScreen3"];
                continueButtonActive = saveFileExists();
            }
        }

        public Platform findPlatformGivenString(string str)
        {
            foreach (Platform p in platforms)
            {
                if (p.GetType().Name.Equals(str))
                {
                    return p;
                }
            }
            //shouldn't happen
            return null;
        }

        public bool continueButtonPressable()
        {
            return continueButtonActive;
        }

        public bool saveFileExists()
        {
            try
            {
                StreamReader saveFile = new StreamReader("bananas.txt");
                string line = saveFile.ReadLine();
                saveFile.Close();
                if (line.Equals("")) return false;
            }
            catch (Exception e)
            {
                //Console.WriteLine("fail");
                return false;
            }

            return true;
        }

        public void setShowInventory(bool b)
        {
            showInventory = b;
        }

        public void refreshAllItems()
        {
            foreach(Level l in levels)
            {
                l.reloadLevelItems();
               // l.addInitialLevelItems();
            }
            

        }

        public static bool boatSpawned()
        {
            foreach (Object o in updateMiscObjects)
            {
                if (o is Boat) return true;
            }
            return false;
        }







        //--------------------------------------STICKY KEYS BLOCK--------------------------------------------
        //Sticky keys disabling code. Credit goes to user x4000 at http://stackoverflow.com/questions/734618/disabling-accessibility-shortcuts-in-net-application
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", SetLastError = false)]
        private static extern bool SystemParametersInfo(uint action, uint param,
            ref SKEY vparam, uint init);
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", SetLastError = false)]
        private static extern bool SystemParametersInfo(uint action, uint param,
            ref FILTERKEY vparam, uint init);
        private const uint SPI_GETSTICKYKEYS = 0x003A;
        private const uint SPI_SETSTICKYKEYS = 0x003B;
        private static bool StartupAccessibilitySet = false;
        private static SKEY StartupStickyKeys;
        private const uint SKF_STICKYKEYSON = 0x00000001;
        private const uint SKF_CONFIRMHOTKEY = 0x00000008;
        private const uint SKF_HOTKEYACTIVE = 0x00000004;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SKEY
        {
            public uint cbSize;
            public uint dwFlags;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct FILTERKEY
        {
            public uint cbSize;
            public uint dwFlags;
            public uint iWaitMSec;
            public uint iDelayMSec;
            public uint iRepeatMSec;
            public uint iBounceMSec;
        }
        private static uint SKEYSize = sizeof(uint) * 2;
        public static void AllowAccessibilityShortcutKeys(bool bAllowKeys)
        {
            if (!StartupAccessibilitySet)
            {
                StartupStickyKeys.cbSize = SKEYSize;
                SystemParametersInfo(SPI_GETSTICKYKEYS, SKEYSize, ref StartupStickyKeys, 0);
                StartupAccessibilitySet = true;
            }
            if (bAllowKeys)
            {
                SystemParametersInfo(SPI_SETSTICKYKEYS, SKEYSize, ref StartupStickyKeys, 0);
            }
            else
            {
                SKEY skOff = StartupStickyKeys;
                if ((skOff.dwFlags & SKF_STICKYKEYSON) == 0)
                {
                    skOff.dwFlags &= ~SKF_HOTKEYACTIVE;
                    skOff.dwFlags &= ~SKF_CONFIRMHOTKEY;
                    SystemParametersInfo(SPI_SETSTICKYKEYS, SKEYSize, ref skOff, 0);
                }
            }
        }
        static void OnProcessExit(object sender, EventArgs e)
        {
            AllowAccessibilityShortcutKeys(true);
        }
        //--------------------------------------END STICKY KEYS BLOCK--------------------------------------------


    }



    enum GameState
    {
        start,
        play,
        pause,
        lost
    }




}
