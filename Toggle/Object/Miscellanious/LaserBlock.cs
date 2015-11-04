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
                            c.zap();
                        }
                    }
                }
            }


        }
    }
}
