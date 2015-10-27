using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Toggle
{
    class Inventory
    {
        InventoryItem[,] items;
        Rectangle[,] itemRectangles;
        Texture2D inventoryGraphic;
        private int x,y;
        
        public Inventory()
        {
            items = new InventoryItem[2,7];
            initializeItemRectangles();
            inventoryGraphic = Textures.textures["inventory2"];
        }


        public void addInventoryItem(InventoryItem ii)
        {
            int xpos = 3;
            int ypos = 3;
            for (int x = 0; x < items.GetLength(0); x++)
            {
                for (int y = 0; y < items.GetLength(1); y++)
                {
                   if (items[x,y] == null)
                   {
                        items[x,y] = ii;
                       //Set position relative to the inventory
                        items[x, y].setX(xpos);
                        items[x, y].setY(ypos);
                       // items[x, y].setHitBox(new Rectangle(xpos + xx, ypos + yy, 32, 32));
                        return;
                   }
                   xpos += 36;
                }
                ypos += 36;
                xpos = 3;
            }
        }

        public void initializeItemRectangles()
        {
            int xpos = 3;
            int ypos = 3;
            itemRectangles = new Rectangle[2, 7];
            for (int x = 0; x < items.GetLength(0); x++)
            {
                for (int y = 0; y < items.GetLength(1); y++)
                {
                    itemRectangles[x, y] = new Rectangle(xpos, ypos, 32, 32);
                    xpos += 36;
                }
                ypos += 36;
                xpos = 3;
            }
        }

        public void drawInventory(SpriteBatch sb)
        {
            sb.Draw(inventoryGraphic, new Vector2(this.x, this.y), Color.White);
            for (int x = 0; x < items.GetLength(0); x++)
            {
                for (int y = 0; y < items.GetLength(1); y++)
                {
                    if (items[x, y] != null)
                    {
                        sb.Draw(items[x, y].getGraphic(), new Vector2(items[x, y].getX() + this.x, items[x, y].getY() + this.y), new Rectangle(0, 0, 32, 32), Color.White);
                        items[x, y].setHitBox(new Rectangle(items[x, y].getX() + this.x, items[x, y].getY() + this.y, 32, 32));
                    }
                }
            }
        }

        

        public string getItemTip(MouseState ms)
        {
            for (int x = 0; x < items.GetLength(0); x++)
            {
                for (int y = 0; y < items.GetLength(1); y++)
                {
                    if (items[x, y] != null)
                    {
                        var mousePosition = new Point(ms.X, ms.Y);
                        if (items[x, y].getHitBox().Contains(mousePosition))
                        {
                            return items[x, y].getItemTip();
                        }
                    }
                }
            }
            return "banana";
        }

        //Called when an item is dragged and released
        public void setNewIndex(InventoryItem i)
        {
            for(int x = 0; x < items.GetLength(0); x++)
            {
                for(int y = 0; y < items.GetLength(1); y++)
                {
                    Vector2 itemCenter = i.getCenter();
                    //int itemX = (int)(itemCenter.X + 0.5);
                    //int itemY = (int)(itemCenter.Y + 0.5);
                    //Blame merle. He'll fix this later.
                    if(itemRectangles[x,y].Contains(itemCenter) && (items[x,y] == null ||(items[x,y] != null && !items[x,y].Equals(i))))
                    {
                        if(items[x,y] == null)
                        {
                            removeItem(i);
                            items[x, y] = i;
                            i.setX(itemRectangles[x, y].X);
                            i.setY(itemRectangles[x, y].Y);
                        }
                        else
                        {
                            if(items[x, y].combineItems(i))
                                removeItem(i);
                            else
                                returnItemToSlot(i);
                        }
                        
                        return;
                    }
                }
            }
            returnItemToSlot(i);
        }

        public void removeItem(InventoryItem i)
        {
            for (int x = 0; x < items.GetLength(0); x++)
            {
                for (int y = 0; y < items.GetLength(1); y++)
                {
                    if(i.Equals(items[x,y]))
                    {
                        items[x, y] = null;
                        return;
                    }
                }
            }
        }

        public void returnItemToSlot(InventoryItem i)
        {
            for (int x = 0; x < items.GetLength(0); x++)
            {
                for (int y = 0; y < items.GetLength(1); y++)
                {
                    if (i.Equals(items[x, y]))
                    {
                        i.setX(itemRectangles[x, y].X);
                        i.setY(itemRectangles[x, y].Y);
                        return;
                    }
                }
            }
        }

        public InventoryItem[,] getItems()
        {
            return items;
        }

        public LampI getLamp()
        {
            for (int x = 0; x < items.GetLength(0); x++)
            {
                for (int y = 0; y < items.GetLength(1); y++)
                {
                    if(items[x,y] is LampI)
                    {
                        return (LampI)items[x, y];
                    }
                }
            }
            return null;
        }



        public int getX(){return x; }
        public int getY(){return y; }
        public void setX(int x) { this.x = x; }
        public void setY(int y) { this.y = y; }
    }
}
