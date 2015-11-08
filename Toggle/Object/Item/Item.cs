using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

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
        public virtual Texture2D getGraphic()
        {
            if (state)
                return goodGraphic;
            else
                return badGraphic;
        }




    }
}
