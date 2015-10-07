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
        private int boundX;
        private int boundY;
        private int screenWidth;
        private int screenHeight;

        public Camera(Object focus,int gameWidth,int gameHeight)
        {
            effectDuration = 0;
            boundX = 0;
            boundY = 0;
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
            int playerX = camFocus.getX();
            int playerY = camFocus.getY();
            Console.WriteLine(playerX);
            Console.WriteLine(playerY);
            if (boundY < screenHeight)
            {
                //if level is smaller than screen, just center it
                x = -(screenWidth / 2);
                y = -(screenHeight / 2);
            }
            else
            {
                if ((playerX >= (screenWidth / 2)) && (playerX <= (boundX - (screenWidth / 2))))
                {
                    x = -camFocus.getX();
                }
                if ((playerY >= (screenHeight / 2)) && (playerY <= (boundY - (screenHeight / 2))))
                {
                    y = -camFocus.getY();
                }
            }
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

        public void setBounds(int xin, int yin)
        {
            boundX = xin;
            boundY = yin;
        }

        public void shake(int duration)
        {
            effectDuration = duration;
        }
    }
}
