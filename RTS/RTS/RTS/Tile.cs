using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS
{
    public class Tile
    {

        public enum Type
        {

        }

        //each sprite tile is 32 x 32
        public static int individualTileWidth = 32;
        public static int individualTileHeight = 32;
        public int row;
        public int col;
        public int locationX;
        public int locationY;
        public int type;
        public Rectangle rect;
        public Boolean buildable;
        
        public Tile(int row, int col, int type)
        {
            this.row = row;
            this.col = col;
            locationX = col * Tile.individualTileWidth;
            locationY = row * Tile.individualTileHeight;
            rect = new Rectangle(locationX, locationY, Tile.individualTileWidth, Tile.individualTileHeight);
            buildable = true;
        }
 

    }
}
