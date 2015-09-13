using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Toggle
{
    class Creature : Object
    {
        protected int velocity = 1;

        public Creature(int xLocation, int yLocation, bool initialState) : base(xLocation, yLocation, initialState)
        {

        }

        public virtual void move()
        {
            if (state)
                goodMove();
            else
                badMove();

            hitBox = new Rectangle(x, y, width, height);
        }

        public virtual void goodMove()
        {

        }
        public virtual void badMove()
        {

        }

        public void invertDirection()
        {
            velocity *= -1;
        }
    }
}
