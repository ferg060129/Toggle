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
        InventoryItem selectedItem;
        private int x,y;
        SpriteFont sf;
        Game1 engine;
        
        public Inventory(Game1 eng)
        {
            items = new InventoryItem[2,7];
            initializeItemRectangles();
            inventoryGraphic = Textures.textures["inventory2"];
            sf = Textures.fonts["arial12"];
            engine = eng;
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
            //Draw the selected item last so it is on bottom.
            if(selectedItem != null)
            sb.Draw(selectedItem.getGraphic(), new Vector2(selectedItem.getX() + this.x, selectedItem.getY() + this.y), new Rectangle(0, 0, 32, 32), Color.White);
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
            Vector2 itemCenter = i.getCenter();
            for(int x = 0; x < items.GetLength(0); x++)
            {
                for(int y = 0; y < items.GetLength(1); y++)
                {
                    
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

            if(itemCenter.X > 0 && itemCenter.X < inventoryGraphic.Width && itemCenter.Y > 0 && itemCenter.Y < inventoryGraphic.Height)
            {
                returnItemToSlot(i);
            }
            else{
                Player p = (Player)(Game1.creatures[0]);
                Rectangle rect = p.getHitBox();
                foreach(Platform plat in Game1.platforms)
                {
                    if(p.getX() == plat.getX() && p.getY() == plat.getY())
                    {
                        bool added = plat.addItemToPlatform(i);
                        if(added)
                        {
                            removeItem(i);
                            return;
                        }
                    }
                }
                returnItemToSlot(i);
            }
            
        }
        /*
        public bool addItemFromBox(InventoryItem i)
        {
            for (int x = 0; x < items.GetLength(0); x++)
            {
                for (int y = 0; y < items.GetLength(1); y++)
                {
                    Vector2 itemCenter = i.getCenter();
                    itemCenter.X -= this.x;
                    itemCenter.Y -= this.y;
   
                    if (itemRectangles[x, y].Contains(itemCenter) && (items[x, y] == null || (items[x, y] != null && !items[x, y].Equals(i))))
                    {
                        if (items[x, y] == null)
                        {
                            items[x, y] = i;
                            i.setX(itemRectangles[x, y].X);
                            i.setY(itemRectangles[x, y].Y);
                            return true;
                        }
                        else
                        {
                        }

                    }
                }
            }
            return false;

        }*/

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

        public InventoryItem[,] getItemsCopy()
        {
            return (InventoryItem[,])items.Clone();
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
        public void setSelectedItem(InventoryItem i, bool b)
        {
            if(b)
            {
                selectedItem = i;
            }
            if(i.isSelected() && !b)
            {
                selectedItem = null;
            }
            i.setSelected(b);
        }

        public InventoryItem getSelectedItem()
        {
            return selectedItem;
        }

        public SpriteFont getFont()
        {
            return sf;
        }

        public void setInventoryItems(InventoryItem[,] inv)
        {
            if(inv == null)
            {
                return;
            }
            for (int x = 0; x < items.GetLength(0); x++)
            {
                for (int y = 0; y < items.GetLength(1); y++)
                {

                        if(items[x,y]!= null && !containsItem(inv,items[x,y]))
                        {
                            //Remove batteries from lamp if necessary
                            if(items[x,y] is LampI)
                            {
                                ((LampI)items[x, y]).setBatteries(false);
                            }

                            setSelectedItem(items[x,y], false);
                            //engine.addItemToCurrentLevel(items[x,y].getItem());
                        }

                        /*
                        if(inv[x,y] == null)
                        {
                            engine.addItemToCurrentLevel(items[x, y].getItem());
                        }
                        else if()
                        */
                    setInventoryItem(inv[x, y],x,y);
                }
            }
        }

        public bool containsItem(InventoryItem [,] i, InventoryItem it)
        {
            foreach(InventoryItem itt in i)
            {
                if(it.Equals(itt))
                {
                    return true;
                }
            }
            return false;
        }

        public void setInventoryItem(InventoryItem ii, int x, int y)
        {
            if (ii == null) return;
            ii.setX(itemRectangles[x, y].X);
            ii.setY(itemRectangles[x, y].Y);
            items[x, y] = ii;
        }
    }
}
