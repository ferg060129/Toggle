using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class Boat : UpdateMiscellanious
    {
        int direction = -1;
        int velocity = 1;
        bool moving = false;
        public Boat(int xLocation, int yLocation)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["boat"];
            badGraphic = Textures.textures["boat"];
        }

        public void setMotion(int dir)
        {
            direction = dir;
            velocity = 1;
            moving = true;
        }

        public int getDirection()
        {
            return direction;
        }

        public int getVelocity()
        {
            return velocity;
        }

        public override void move()
        {
            if(!moving)
            {
                return;
            }
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
            //imageBoundingRectangle = getNextImageRectangle(direction, oldDirection, moving);
           // hitBox = new Rectangle(x, y, width, height);
        }

        public void reportCollision(Object o)
        {
            if(o is Lake)
            {
                
            }
        }

    }

}
