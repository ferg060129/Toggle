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
    class Creature : Object
    {
        protected int velocity = 1;

        public Creature(int xLocation, int yLocation, bool initialState) : base(xLocation, yLocation, initialState)
        {

        }

        public virtual void move(ArrayList collidables)
        {
            if (state)
                goodMove(collidables);
            else
                badMove(collidables);

            hitBox = new Rectangle(x, y, width, height);
        }

        public virtual void goodMove(ArrayList collidables)
        {

        }
        public virtual void badMove(ArrayList collidables)
        {

        }

        public void invertDirection()
        {
            velocity *= -1;
        }
    }
}
