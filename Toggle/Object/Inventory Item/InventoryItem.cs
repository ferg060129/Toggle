using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
namespace Toggle
{

    //Maybe this shouldn't inherit from object
    public class InventoryItem : Object
    {
        protected string itemTipGood = "Banana";
        protected string itemTipBad = "Rotten banana :c";
        protected bool hovered = false;
        protected bool selected = false;
        protected Item myItem = null;
        public InventoryItem(Item i) : base()
        {
            width = 32;
            height = 32;
            myItem = i;
        }

        public void setHitBox(Rectangle r)
        {
            hitBox = r;

        }
        public string getItemTip()
        {
            if(state)
            {
                return itemTipGood;
            }
            else
            {
                return itemTipBad;
            }
        }

        public bool isHovered()
        {
            return hovered;
        }

        public void setHovered(bool b)
        {
            hovered = b;
        }

        public void setSelected(bool sel)
        {
            selected = sel;
        }

        public bool isSelected()
        {
            return selected;
        }

        //return false if items were not combined
        public virtual bool combineItems(InventoryItem i)
        {
            return false;
        }
        public virtual Texture2D getGraphic()
        {
            if (state)
                return goodGraphic;
            else
                return badGraphic;
        }

        public Item getItem()
        {
            return myItem;
        }
    }
}
