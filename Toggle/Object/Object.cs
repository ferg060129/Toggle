using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
namespace Toggle
{
    class Object
    {
        protected int x, y;
        protected int width, height;
        protected bool state;           //true good, false bad
        protected bool collidable;       //can things collide with this
        protected Texture2D goodGraphic;
        protected Texture2D badGraphic;
        protected Rectangle hitBox;
        protected Rectangle imageBoundingRectangle;

        public Object(int xLocation, int yLocation)
        {
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

        public virtual void switchState(){
            state = !state;
        }

        public Texture2D getGraphic()
        {
            if (state)
                return goodGraphic;
            else
                return badGraphic;
        }

        public void setState(bool st)
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

        public bool getCollision() {return collidable;}
        public bool getState() { return state; }
        public int getX(){return x;}
        public int getY(){return y;}
        public Rectangle getHitBox() { return hitBox; }
        public Rectangle getImageBoundingRectangle() { return imageBoundingRectangle; }
        public Vector2 getPositionVector() { return new Vector2(x, y); }
        public Vector2 getPosition(){return new Vector2(x, y); }

    }
}
