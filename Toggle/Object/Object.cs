using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
namespace Toggle
{
    public class Object
    {
        protected int x, y;
        protected int width, height;
        //animation properties
        //[0] is column height, [1] is frame count, [2] is animation speed??
        protected Dictionary<string, int[]> animations = new Dictionary<string, int[]>();
        protected string currentAnimation;
        protected int frame;
        protected int frameTick; //after x ticks, advance a frame so FPS
        protected bool animationPlay;
        //
        protected bool state;           //true good, false bad
        protected bool collidable;       //can things collide with this
        protected Texture2D goodGraphic;
        protected Texture2D badGraphic;
        protected Rectangle hitBox;
        protected Rectangle imageBoundingRectangle;
        protected bool isSolid = false;
        protected bool projectileBlocks = true;

        public Object(int xLocation, int yLocation)
        {
            frame = 0;
            frameTick = 0;
            x = xLocation;
            y = yLocation;
            state = Game1.worldState;
            collidable = false;

            imageBoundingRectangle = new Rectangle(0, 0, 32, 32);

            width = 32;
            height = 32;
            hitBox = new Rectangle(xLocation, yLocation, width, height);
        }

        public Object()
        {
            state = Game1.worldState;
            collidable = false;

            imageBoundingRectangle = new Rectangle(0, 0, 32, 32);

            width = 32;
            height = 32;
            //hitBox = new Rectangle(xLocation, yLocation, width, height);

        }
        public void animate()
        {
            frameTick++;
            int columnLoc = animations[currentAnimation][0];
            int frameCount = animations[currentAnimation][1];
            if (frame < frameCount - 1)
            {
                if (frameTick % 10 == 0)
                {
                    frame++;
                }
            }
            else
            {
                if (frameTick % 10 == 0)
                {
                    frame = 0;
                }
            }
            imageBoundingRectangle = new Rectangle(frame * 32, columnLoc * 32, 32, 32);
        }
        public void setAnimation(string name)
        {
            if (name != currentAnimation)
            {
                frame = 0;
            }
            currentAnimation = name;
        }

        public virtual void switchState(){
            state = !state;
        }

        public virtual Texture2D getGraphic()
        {
            if (state)
                return goodGraphic;
            else
                return badGraphic;
        }

        public virtual void setState(bool st)
        {
            state = st;
        }

        public void setX(int xLocation)
        {
            x = xLocation;
        }

        public void setY(int yLocation)
        {
            y = yLocation;
        }

        public void setPosition(Vector2 v)
        {
            x = (int)(v.X + 0.5);
            y = (int)(v.Y + 0.5);
            hitBox = new Rectangle(x, y, width, height);
        }

        //return true is this is overlapping m
        public bool checkOverlap(Object m)
        {
            bool isOverlap = false;
            if (((x > (m.getX()) - 32) && (x < (m.getX() + 32))) &&
               ((y > (m.getY()) - 32) && (y < (m.getY() + 32))))
            {
                isOverlap = true;
            }
            return isOverlap;
        }

        public bool getCollision() {return collidable;}

        public bool blocksProjectiles(){ return projectileBlocks;}
        public bool getState() { return state; }
        public int getX(){return x;}
        public int getY(){return y;}
        public Rectangle getHitBox() { return hitBox; }
        public Rectangle getImageBoundingRectangle() { return imageBoundingRectangle; }
        public Vector2 getPositionVector() { return new Vector2(x, y); }
        public Vector2 getPosition(){return new Vector2(x, y); }
        public virtual bool getSolid() { return isSolid; }
        public Vector2 getCenter() { return new Vector2(x + width/2.0f, y + height/2.0f); }

    }
}
