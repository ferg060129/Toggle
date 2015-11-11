using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class Platform : Object
    {
        protected Type itemType = null;
        bool itemOnPlatform = false;
        public Platform(int xLocation, int yLocation)
            : base(xLocation, yLocation)
        {


        }

        public void reportCollision(Object o)
        {
            Type t = o.GetType();

            if (t.IsAssignableFrom(itemType) || itemType.IsAssignableFrom(t))
            {
                ((Item)o).setPickupAble(false);
                itemOnPlatform = true;
            }
        }

        public bool isItemOnPlatform()
        {
            return itemOnPlatform;
        }

    }
}
