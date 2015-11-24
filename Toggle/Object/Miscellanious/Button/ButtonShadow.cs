using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Toggle
{
    class ButtonShadow: Button
    {

        bool existState;
        Queue<Point> jumpPoints = new Queue<Point>();
        Queue<Torch> torches = new Queue<Torch>();
        int totalPoints = 0;
        bool triggerCooldown = false;
        public ButtonShadow(int xLocation, int yLocation, Miscellanious linked, bool initstate)
            : base(xLocation, yLocation, linked)
        {
            goodGraphic = Textures.textures["buttonSUp"];
            badGraphic = Textures.textures["buttonSUp"];
            link = linked;
            existState = initstate;
            heavyButton = false;
        }

        public override void onShift()
        {
            for (int i = 0; i < 40; i++)
            {
                Game1.particles.Add(new Particle(x, y, "particleShadow"));
            }
            triggerCooldown = false;
            Console.WriteLine(existState);
            if (state != existState)
            {
                imageBoundingRectangle = new Rectangle(0, 0, 0, 0);
                hitBox = new Rectangle(0, 0, 0, 0);
            }
            else
            {
                imageBoundingRectangle = new Rectangle(0, 0, 32, 32);
                hitBox = new Rectangle(x, y, 32, 32);
            }
        }

        public override void onTrigger()
        {
            if (triggerCooldown == false)
            {
                
                if (torches.Count > 0)
                {
                    torches.Dequeue().onButton();
                }
                if (jumpPoints.Count > 0)
                {
                    Point tempPoint = jumpPoints.Dequeue();
                    Console.WriteLine(jumpPoints.Count);
                    x = tempPoint.X;
                    y = tempPoint.Y;
                    existState =! existState;
                    imageBoundingRectangle = new Rectangle(0, 0, 0, 0);
                    hitBox = new Rectangle(0, 0, 0, 0);
                }
                else
                {
                    link.onButton();
                }
                triggerCooldown = true;
            }

        }

        //takes two coordnates and converts them to pixels (snap to 32)
        public void addPressPoint(int xIn, int yIn)
        {
            Point temp = new Point((xIn * 32), (yIn * 32));
            jumpPoints.Enqueue(temp);
            totalPoints = jumpPoints.Count;
        }

        public void addTorchQueue(Torch input)
        {
            torches.Enqueue(input);
        }
    }


}
