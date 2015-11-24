using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    public class Platform : Object
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

        public bool addItemToPlatform(InventoryItem i)
        {
            Type t = i.GetType();

            if (t.IsAssignableFrom(itemType) || itemType.IsAssignableFrom(t))
            {
                setItemOnPlatform(true);
                return true;
            }
            return false;
        }

        public bool isItemOnPlatform()
        {
            return itemOnPlatform;
        }

        public void setItemOnPlatform(bool b)
        {
            itemOnPlatform = b;
            changePlatformGraphic(b);
        }

        public virtual void changePlatformGraphic(bool b)
        {

        }

    }
}
