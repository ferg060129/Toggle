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
    //small change
    class FlowerTentacles : Creature
    {
        public FlowerTentacles(int xLocation, int yLocation, bool initialState)
            : base(xLocation, yLocation, initialState)
        {
            goodGraphic = Textures.textures["kitten"];
            badGraphic = Textures.textures["zombie"];
            imageBoundingRectangle = new Rectangle(0, 0, 32, 32);
            width = 32;
            height = 32;
        }

        public override void goodMove(ArrayList collidables)
        {
            bool canMove = true;
            foreach (Creature c in collidables)
            {
                if (c != this)
                {
                    Rectangle otherRect = c.getHitBox();
                    otherRect.Y -= velocity;
                    if (otherRect.Intersects(getHitBox()))
                    {
                        canMove = false;
                    }
                }
            }
            if (canMove)
                y += velocity;
        }

        public override void badMove(ArrayList collidables)
        {
            bool canMove = true;
            foreach (Creature c in collidables)
            {
                if (c != this)
                {
                    Rectangle otherRect = c.getHitBox();
                    otherRect.Y += velocity;
                    if (otherRect.Intersects(getHitBox()))
                    {
                        canMove = false;
                    }
                }
            }
            if (canMove)
                y -= velocity;
        }
    }
}
