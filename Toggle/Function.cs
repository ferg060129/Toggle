using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class Function
    {
        public static int distanceTo(Object me, Object them)
        {
            int dist;
            dist = (int)Math.Sqrt(Math.Pow(them.getX() - me.getX(), 2) + Math.Pow(them.getY() - me.getY(), 2));
            return dist;
        }
    }
}
