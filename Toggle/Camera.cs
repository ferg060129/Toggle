using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Toggle
{
    class Camera
    {
        private Matrix camMatrix;
        private int effectDuration;
        private Object camFocus;
        private int x;
        private int y;
        private int screenWidth;
        private int screenHeight;

        public Camera(Object focus,int gameWidth,int gameHeight)
        {
            effectDuration = 0;

            camFocus = focus;
            x = -camFocus.getX();
            y = -camFocus.getY();
            screenWidth = gameWidth;
            screenHeight = gameHeight;
            camMatrix = Matrix.CreateTranslation(x + (screenWidth / 2), y + (screenHeight / 2), 0);
        }
        public void update()
        {
            int xMod = 0;
            int yMod = 0;
            x = -camFocus.getX();
            y = -camFocus.getY();
            Random rnd = new Random();
            if (effectDuration > 0)
            {
                effectDuration--;
                xMod = (int)(rnd.Next(1, 5) * Math.Sin(effectDuration));
                yMod = (int)(rnd.Next(1, 5) * Math.Sin(effectDuration));
            }
            camMatrix = Matrix.CreateTranslation(x + (screenWidth / 2) + xMod, y + (screenHeight / 2) + yMod, 0);
        }

        public Matrix getMatrix()
        {
            return camMatrix;
        }

        public int getX()
        {
            return x;
        }
        public int getY()
        {
            return y;
        }

        public void shake(int duration)
        {
            effectDuration = duration;
        }
    }
}
