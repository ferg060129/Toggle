using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Toggle
{
    class Box : UpdateMiscellanious
    {
        InventoryItem storedItem = null;
        bool selected = false;
        public Box(int xLoc, int yLoc) : base(xLoc,yLoc)
        {
            goodGraphic = Textures.textures["boxInside"];
            badGraphic = Textures.textures["boxInside"];
            width = 100;
            height = 100;
            imageBoundingRectangle = new Rectangle(0, 0, width, height);
            
        }

        public InventoryItem getStoredItem()
        {
            return storedItem;
        }

        public bool setStoredItem(InventoryItem i)
        {
            if(i == null)
            {
                storedItem = i;
                return true;
            }

            if(storedItem == null)
            {
                storedItem = i;
                storedItem.setX(this.x + 34);
                storedItem.setY(this.y + 34);
                return true;
            } 
            return false;
            
        }
        public void drawBox(SpriteBatch sb)
        {
            sb.Draw(getGraphic(), new Vector2(this.x, this.y), Color.White);
            if(storedItem != null)
            {
                if(!selected)
                {
                    storedItem.setX(this.x + 34);
                    storedItem.setY(this.y + 34);
                }
              
                sb.Draw(storedItem.getGraphic(), new Vector2(storedItem.getX(), storedItem.getY()), Color.White);
                
                storedItem.setHitBox(new Rectangle(this.x + 34, this.y + 34, 32, 32));
            }

        }

        public void setSelectedItem(bool b)
        {
            selected = b;
        }

        public bool isItemSelected()
        {
            return selected;
        }

        public void returnItemToSlot()
        {
            storedItem.setX(this.x + 34);
            storedItem.setY(this.y + 34);
        }

    }
}
