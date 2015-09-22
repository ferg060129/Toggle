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
        protected Texture2D goodGraphic;
        protected Texture2D badGraphic;
        protected Rectangle hitBox;
        protected Rectangle imageBoundingRectangle;

        public Object(int xLocation, int yLocation, bool initialState)
        {
            x = xLocation;
            y = yLocation;
            state = initialState;
        }
        public Object(bool initialState)
        {
            state = initialState;
        }

        public void switchState(){
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

        public bool getState() { return state; }
        public int getX(){return x;}
        public int getY(){return y;}
        public Rectangle getHitBox() { return hitBox; }
        public Rectangle getImageBoundingRectangle() { return imageBoundingRectangle; }
        public Vector2 getPositionVector() { return new Vector2(x, y); }
    }
}
