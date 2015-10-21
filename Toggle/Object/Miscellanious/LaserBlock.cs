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
            updatePeriod = 0;
        }

        public override void onUpdate()
        {
            updatePeriod++;
            if (updatePeriod % 32 == 0)
            {
                LaserDetector left = new LaserDetector(x, y);
            }
        }
    }
}
