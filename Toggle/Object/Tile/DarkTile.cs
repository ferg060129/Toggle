using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class DarkTile : Tile
    {
        float baseOpacity;
        float currentOpacity;
        bool lit;
        public DarkTile(int xLocation, int yLocation, String gGraphic, String bGraphic)
            : base(xLocation, yLocation, gGraphic, bGraphic)
        {
            baseOpacity = 1.0f;
        }

        public void setOpacity(float o)
        {
            baseOpacity = o;
        }

        public float getOpacity()
        {
            if(lit)
            {
                lit = false;
                return currentOpacity;
            }



            return baseOpacity;  
        }


        public void addLampLight(double distanceFromLamp)
        {
            if (distanceFromLamp < 100)
            {
                currentOpacity = baseOpacity - (float)(40.0 / distanceFromLamp);
                lit = true;
            }
        }

        public bool isLit()
        {
            return lit;
        }
    }
}
