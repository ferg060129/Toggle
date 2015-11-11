using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Toggle
{
    class Player : Creature
    {
        KeyboardState oldKeyBoardState;
        Inventory inventory;
        Game1 engine;
        bool stateLocked = false;
        bool currentlyMove = false;
        bool onBoat = false;
        int distanceTraveled = 0;
        double proportion = 0;
        Boat myBoat = null;
        int hitInvulnTime = 0;
        int hitInvulnMax = 120;
        //prevent shifting for a short while at start of game
        int initialShiftCD = 10;


        bool readingChalkboard = false;
        Chalkboard playerChalkboard;
        ChalkboardTop collideChalkboard;

        bool accessingBox = false;
        Box playerBox;
        BoxTop collideBoxtop;

        public Player(int xLocation, int yLocation, Inventory i, Game1 eng) : base(xLocation, yLocation)
        {
            animations.Add("idleDown",  new int[] { 0, 2 });
            animations.Add("idleUp",    new int[] { 1, 2 });
            animations.Add("idleRight", new int[] { 2, 2 });
            animations.Add("idleLeft",  new int[] { 3, 2 });
            animations.Add("moveDown",  new int[] { 4, 4 });
            animations.Add("moveUp",    new int[] { 5, 4 });
            animations.Add("moveRight", new int[] { 6, 2 });
            animations.Add("moveLeft",  new int[] { 7, 2 });
            goodGraphic = Textures.textures["protagsheet"];
            badGraphic = Textures.textures["protagsheet"];

            row = 7;
            imageBoundingRectangle = new Rectangle(32 * row, 32, 32, 32);
            
            width = 32;
            height = 32;
            velocity = 4;
            collidable = false;
            inventory = i;
            engine = eng;
            proportion = 0.5;
        }


        public override void move()
        {
            if (readingChalkboard && !hitBox.Intersects(collideChalkboard.getHitBox()))
            {
                readingChalkboard = false;
                Game1.updateMiscObjects.Remove(playerChalkboard);
            }

            if (accessingBox && !hitBox.Intersects(collideBoxtop.getHitBox()))
            {
                accessingBox = false;
                Game1.updateMiscObjects.Remove(playerBox);
            }


            previousHitBox = new Rectangle(x, y, width, height);
            KeyboardState newKeyBoardState = Keyboard.GetState();

            //Variables to keep track of animation sprite.
            int oldDirection = direction; 
            moving = true;

            if(onBoat)
            {
                myBoat.setX(myBoat.getX() + myBoat.getVelocity());
                direction = myBoat.getDirection();
                velocity = myBoat.getVelocity();
                return;
            }

            if (currentlyMove == false)
            {
                distanceTraveled = 0;
                
                if (newKeyBoardState.IsKeyDown(Keys.Up))
                {
                    direction = 1;
                    setAnimation("moveUp");
                    currentlyMove = true;
                }
                else if (newKeyBoardState.IsKeyDown(Keys.Down))
                {
                    direction = 3;
                    setAnimation("moveDown");
                    currentlyMove = true;
                }
                else if (newKeyBoardState.IsKeyDown(Keys.Left))
                {
                    direction = 0;
                    setAnimation("moveLeft");
                    currentlyMove = true;
                }
                else if (newKeyBoardState.IsKeyDown(Keys.Right))
                {
                    direction = 2;
                    setAnimation("moveRight");
                    currentlyMove = true;
                }
                else
                {
                    moving = false;
                }
            }
            if ((moving == false) && (currentlyMove == false))
            {
                switch (direction)
                {
                    default:
                        break;
                    case 0:
                        setAnimation("idleLeft");
                        break;
                    case 1:
                        setAnimation("idleUp");
                        break;
                    case 2:
                        setAnimation("idleRight");
                        break;
                    case 3:
                        setAnimation("idleDown");
                        break;

                }
            }
            if (initialShiftCD > 0)
            {
                initialShiftCD--;
            }
            else if ((newKeyBoardState.IsKeyDown(Keys.LeftShift) && oldKeyBoardState != null && !oldKeyBoardState.IsKeyDown(Keys.LeftShift))
                || (newKeyBoardState.IsKeyDown(Keys.RightShift) && oldKeyBoardState != null && !oldKeyBoardState.IsKeyDown(Keys.RightShift)))
            {
                if(!stateLocked && engine.getShiftCD() == 0)
                {
                    engine.setShiftCD();
                    engine.switchStates();
                }
                    
            }
            oldKeyBoardState = newKeyBoardState;

            //Get next image for sprite
            //imageBoundingRectangle = getNextImageRectangle(direction, oldDirection, moving);
            animate();
            hitBox = new Rectangle(x, y, width, height);

            //used for "health".  Hourglass decrements faster if its more than half
            if (proportion > 0.5)
            {
                proportion -= 0.001;
            }
            proportion -= 0.0001;

            if (hitInvulnTime > 0)
            {
                hitInvulnTime--;
                //flash the player while in hit invuln
                if (hitInvulnTime % 6 >= 3)
                {
                    imageBoundingRectangle = new Rectangle(0, 0, 0, 0);
                }
            }
        }

        public void moveUpdate()
        {
            int oldDirection = direction;

            if(onBoat)
            {
                this.x = myBoat.getX();
                this.y = myBoat.getY();
                return;
            }

            if (currentlyMove)
            {
                switch (direction)
                {
                    default:
                        break;
                    case 0:
                        x -= velocity;
                        break;
                    case 1:
                        y -= velocity;
                        break;
                    case 2:
                        x += velocity;
                        break;
                    case 3:
                        y += velocity;
                        break;
                }
                distanceTraveled += velocity;
            }
            //snapped to tile
            if ((x % 32 == 0) && (y % 32 == 0))
            {
                currentlyMove = false;
            }
            //failsafe
            if (distanceTraveled >= 32)
            {
                currentlyMove = false;
            }
            hitBox = new Rectangle(x, y, width, height);
        }

        public override void zap()
        {   
            //instakill for now
            damageProportion(1, 40);
        }
        
        public void pickUp(Item i)
        {
            inventory.addInventoryItem(i.pickUpItem());
        }
        public override void reportCollision(Object o)
        {
            if (o is Boat && !onBoat)
            {
                onBoat = true;
                myBoat = (Boat)o;
                myBoat.setMotion(2);
            }

            if (o is DogBoogieman)
            {
                if (!o.getState())
                {
                    proportion = 0;
                }
            }
            if ((o is Ghost) && (!o.getState()))
            {
                damageProportion(0.3);
            }
            if(o is ChalkboardTop)
            {
                
                if(!readingChalkboard)
                {
                    collideChalkboard = (ChalkboardTop)o;
                    playerChalkboard = new Chalkboard(0, 0, collideChalkboard.getGate());
                    Game1.updateMiscObjects.Add(playerChalkboard);
                }
                readingChalkboard = true;
                
            }
            if(o is BoxTop)
            {
                if(!accessingBox)
                {
                    playerBox = ((BoxTop)o).getBox();
                    Game1.updateMiscObjects.Add(playerBox);
                    collideBoxtop = (BoxTop)o;
                    
                }
                accessingBox = true;
            }



            if(o.getSolid())
            {
                //currentlyMove = false;
            }

            base.reportCollision(o);
            if(o is FlowerTentacles && !o.getState())
            {
                proportion -= 0.001;
            }
            if (o is Item)
            {
                pickUp((Item)o);
            }
            if (o is GoodTile)
            {
                if (!state)
                {
                    engine.switchStates();
                }
            }
            if(o is BadTile)
            {
                if (state)
                {
                    engine.switchStates();
                }
            }
            if (o is LockTile)
            {
                stateLocked = true;
            }
            if(o is UnlockTile)
            {
                stateLocked = false;
            }
            if(o is LevelTile)
            {
                if(this.x == o.getX() && this.y == o.getY())
                {
                    engine.setLevel(((LevelTile)o));
                }
            }

          
        }


        public double getProportion()
        {
            return proportion;
        }

        public void setPropotion(double input)
        {
            proportion = input;
        }

        public void damageProportion(double damage)
        {
            if (hitInvulnTime == 0)
            {
                proportion -= damage;
                hitInvulnTime = hitInvulnMax;
            }
            
        }

        public void damageProportion(double damage, int setInvuln)
        {
            if (hitInvulnTime == 0)
            {
                proportion -= damage;
                hitInvulnTime = setInvuln;
            }

        }


        public override void switchState()
        {
            
        }


        public override void setState(bool st)
        {
            base.setState(st);
            proportion = 1 - proportion;
        }

        public bool isDead()
        {
            return proportion <= 0;
        }

        public bool isLocked()
        {
            return stateLocked;
        }

        public bool isOnBoat()
        {
            return onBoat;
        }

        public bool isReadingChalkboard()
        {
            return readingChalkboard;
        }

        public bool isAccessingBox()
        {
            return accessingBox;
        }

        public Box getBox()
        {
            return playerBox;
        }
        
    }
}
