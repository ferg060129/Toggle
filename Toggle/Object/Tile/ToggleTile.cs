using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class ToggleTile : Tile
    {
        //Records whether this tile toggles to the good world or the bad world.
        bool worldState;
        public ToggleTile(int xLocation, int yLocation, bool initialState, String gGraphic, String bGraphic, bool worldState1) : base(xLocation, yLocation, initialState, gGraphic, bGraphic)
        {
            worldState = worldState1;
        }


        public bool getWorldState()
        {
            return worldState;
        }

    }
}
