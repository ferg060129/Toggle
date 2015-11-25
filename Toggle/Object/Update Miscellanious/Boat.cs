using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
namespace Toggle
{
    class Boat : UpdateMiscellanious
    {
        int direction = -1;
        int velocity = 1;
        bool moving = false;
        Point stopLocation;
        public Boat(int xLocation, int yLocation, Point p)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["boat"];
            badGraphic = Textures.textures["boat"];
            stopLocation = p;
            setMotion(0);
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
                hitBox = new Rectangle(x, y, width, height);
                return;
            }

            if (direction == 0 && x == stopLocation.X && y == stopLocation.Y)
            {
                moving = false;
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
