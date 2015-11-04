using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Toggle
{
    class LaserBlock : Pushable
    {
        int updatePeriod;
        ArrayList laserSegments;
        ArrayList laserDetectors;
        public LaserBlock(int xLocation, int yLocation) : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["BoxLight"];
            badGraphic = Textures.textures["BoxDark"];
            updatePeriod = 0;
        }

        public override void onUpdate()
        {
            bool zapCreature = false;
            updatePeriod++;
            if (updatePeriod > 0)
            {
                foreach (Creature c in Game1.creatures)
                {
                    if (c.getX() == x)
                    {
                        zapCreature = true;
                        foreach (Miscellanious m in Game1.miscObjects)
                        {
                            if (m != this)
                            {
                                if (m.getX() == x)
                                {
                                    if (Math.Abs(m.getY()) < Math.Abs(c.getY()))
                                    {
                                        zapCreature = false;
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
                }
            }


        }
    }
}
