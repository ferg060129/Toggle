using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Toggle
{
    public class Item : Object
    {
        protected InventoryItem inventoryItem;
        protected int itemPickupCD = 0;
        protected bool pickupAble = true;  
        public Item(int xLocation, int yLocation) : base(xLocation, yLocation)
        {
            

        }

        public virtual void makeInventoryItem()
        {
            inventoryItem = new InventoryItem();
        }

        public virtual InventoryItem pickUpItem()
        {
            if(inventoryItem==null)
                makeInventoryItem();
            return inventoryItem;
        }
        public virtual Texture2D getGraphic()
        {
            if(itemPickupCD >0)
            itemPickupCD--;
            if (state)
                return goodGraphic;
            else
                return badGraphic;
            
        }

        public bool canPickUp()
        {
            return itemPickupCD == 0 && pickupAble;
        }

        public void setItemPickupCD()
        {
            itemPickupCD = 150;
        }

        public void setPickupAble(bool b)
        {
            pickupAble = b;
        }


    }
}
