using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class Item : Object
    {
        protected InventoryItem inventoryItem;
        public Item(int xLocation, int yLocation) : base(xLocation, yLocation)
        {


        }

        public virtual void makeInventoryItem()
        {
            inventoryItem = new InventoryItem();
        }

        public InventoryItem pickUpItem()
        {
            if(inventoryItem==null)
                makeInventoryItem();
            return inventoryItem;
        }




    }
}
