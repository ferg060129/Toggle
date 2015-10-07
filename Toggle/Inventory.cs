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
        Object center;
        int xLocation, yLocation;
        
        public Inventory(int xLoc, int yLoc)
        {
            items = new InventoryItem[2,7];
            initializeItemRectangles();
            inventoryGraphic = Textures.textures["inventory"];
            xLocation = xLoc;
            yLocation = yLoc;
        }


        public void addInventoryItem(InventoryItem ii)
        {
            for (int x = 0; x < items.GetLength(0); x++)
            {
                for (int y = 0; y < items.GetLength(1); y++)
                {
                   if (items[x,y] == null)
                   {
                        items[x,y] = ii;
                        return;
                   }
                }
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
                    ypos += 35;
                }
                xpos += 35;
            }

        }

        public void drawInventory(SpriteBatch sb)
        {
            sb.Draw(inventoryGraphic, new Vector2(xLocation, yLocation), Color.White);
            int xpos = 3;
            int ypos = 3;
            for (int x = 0; x < items.GetLength(0); x++)
            {
                for (int y = 0; y < items.GetLength(1); y++)
                {
                    if (items[x, y] != null)
                    {
                        sb.Draw(items[x, y].getGraphic(), new Vector2(xpos + xLocation, ypos + yLocation), new Rectangle(0, 0, 32, 32), Color.White);
                    }
                    ypos += 35;
                }
                xpos += 35;
            }
        }

        public InventoryItem[,] getItems()
        {
            return items;
        }
    }
}
