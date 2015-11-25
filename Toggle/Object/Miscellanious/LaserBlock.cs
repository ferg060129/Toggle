using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Toggle
{
    class LaserBlock : Pushable
    {
        int updatePeriod;
        bool direction; //true is horizontal, false is verticle, current direction
        bool spawnDirection; //same but determines spawn
        int[] laserEnds = new int[4]; //end points, (left x, right x, top y, bottom y)
        int phaseOffset;    //changes time that lasers fade in and out
        public LaserBlock(int xLocation, int yLocation) : base(xLocation, yLocation)
        {
            phaseOffset = Game1.random.Next(0, 180);
            direction = true;
            spawnDirection = true;
            onShift();
            updateGraphic();
            updatePeriod = 0;
        }

        public LaserBlock(int xLocation, int yLocation,bool dirIn)
            : base(xLocation, yLocation)
        {
            phaseOffset = Game1.random.Next(0, 180);
            spawnDirection = dirIn;
            onShift();
            updateGraphic();
            updatePeriod = 0;
        }

        public void updateGraphic()
        {
            if (direction)
            {
                goodGraphic = Textures.textures["lasBoxHori"];
                badGraphic = Textures.textures["lasBoxHori"];
            }
            else
            {
                goodGraphic = Textures.textures["lasBoxVert"];
                badGraphic = Textures.textures["lasBoxVert"];
            }
        }

        public override void onShift()
        {
            base.onShift();
            if (spawnDirection)
            {
                if (state)
                    direction = false;
                else
                    direction = true;
            }
            else
            {
                if (state)
                    direction = true;
                else
                    direction = false;
            }
            
            updateGraphic();
        }

        public int[] getLaserEnds()
        {
            return laserEnds;
        }

        public bool getDirection()
        {
            return direction;
        }

        public int getPhaseOffset()
        {
            return phaseOffset;
        }

        public void calcLaserEnds()
        {
            ArrayList toIterate = new ArrayList();
            toIterate.AddRange(Game1.miscObjects);
            toIterate.AddRange(Game1.solidTiles);
            laserEnds[0] = 0;
            laserEnds[1] = 999999;
            laserEnds[2] = 0;
            laserEnds[3] = 999999;
            foreach (Object m in toIterate)
            {
                if ((m != this) && (m.getX() == x) && (m.blocksProjectiles()) && (m.getSolid() == true))
                {
                    //calc laser end points
                    if ((m.getY() > y) && (m.getY() < laserEnds[3]))
                        laserEnds[3] = m.getY() - 32;
                    if ((m.getY() < y) && (m.getY() > laserEnds[2]))
                        laserEnds[2] = m.getY() + 32;
                }
                if ((m != this) && (m.getY() == y) && (m.blocksProjectiles()) && (m.getSolid() == true))
                {
                    //calc laser end points
                    if ((m.getX() > x) && (m.getX() < laserEnds[1]))
                        laserEnds[1] = m.getX() - 32;
                    if ((m.getX() < x) && (m.getX() > laserEnds[0]))
                        laserEnds[0] = m.getX() + 32;
                }
            }
        }

        public override void onUpdate()
        {
            bool zapCreature = false;
            updatePeriod++;
            ArrayList toIterate = new ArrayList();
            if (updatePeriod > 0)
            {
                foreach (Creature c in Game1.creatures)
                {
                    if (c is Player)
                    {
                        toIterate.AddRange(Game1.miscObjects);
                        toIterate.AddRange(Game1.solidTiles);
                        if ((c.getX() >= x) && (c.getX() < x + 32) && (!direction))
                        {
                            zapCreature = true;
                            foreach (Object m in toIterate)
                            {
                                if ((m != this) && (m.getX() == x) && (m.blocksProjectiles()) && (m.getSolid() == true))
                                {
                                    //creature below laser case,then above laser case
                                    if (((m.getY() < c.getY()) && (m.getY() > y)) && (c.getY() > y))
                                    {
                                        zapCreature = false;
                                    }
                                    else if (((m.getY() > c.getY()) && (m.getY() < y)) && (c.getY() < y))
                                    {
                                        zapCreature = false;
                                    }
                                }
                            }
                        }
                        if ((c.getY() >= y) && (c.getY() < y + 32) && (direction))
                        {
                            zapCreature = true;
                            foreach (Object m in toIterate)
                            {
                                if ((m != this) && (m.getY() == y) && (m.blocksProjectiles()) && (m.getSolid() == true))
                                {
                                    //creature right laser case,then left laser case
                                    if (((m.getX() < c.getX()) && (m.getX() > x)) && (c.getX() > x))
                                    {
                                        zapCreature = false;
                                    }
                                    else if (((m.getX() > c.getX()) && (m.getX() < x)) && (c.getX() < x))
                                    {
                                        zapCreature = false;
                                    }
                                }
                            }
                        }
                    }
                    if (zapCreature)
                    {
                        //should probably change this to be zap(c);, where zap is defined as a method
                        //of Laser block.  This way you don't have only one "zap" affect for a player, and
                        //can easily make different types of zaps (damage variance, world shifting, etc)
                        c.zap();
                    }
                    
                }
                calcLaserEnds();
            }
            //endmethod

        }
    }
}
