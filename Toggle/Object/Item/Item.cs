using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class Item : Object
    {
        protected InventoryItem inventoryItem;
        public Item(int xLocation, int yLocation, bool initialState) : base(xLocation, yLocation, initialState)
        {


        }

        public virtual void makeInventoryItem()
        {
            inventoryItem = new InventoryItem(state);
        }

        public InventoryItem pickUpItem()
        {
            if(inventoryItem==null)
            makeInventoryItem();
            return inventoryItem;
        }


    }
}
