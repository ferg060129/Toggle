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
    class PlayerGhost : Creature
    {
        bool activated = false;
        int lastX;
        int timeAlive;
        public PlayerGhost(int xLocation, int yLocation)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["playerGhost"];
            badGraphic = Textures.textures["playerGhost"];
            timeAlive = 100;
            imageBoundingRectangle = new Rectangle(0, 0, 32, 32);
         
        }

        public override void move()
        {
            if (activated)
            {
                x = lastX + (int)(Math.Sin((timeAlive - 200) * Math.PI / 20) * 8);
                y--;
                spriteAlpha -= 0.01f;
                timeAlive--;
            }
        }

        public override void reportCollision(Object o)
        {
            
        }

        public int getTimeAlive()
        {
            return timeAlive;
        }

        public void activate()
        {
            lastX = x;
            activated = true;
        }

        public bool isActive()
        {
            return activated;
        }

        public void reset()
        {
            activated = false;
            timeAlive = 200;
            spriteAlpha = 1.0f;
        }

       
    }
    
}