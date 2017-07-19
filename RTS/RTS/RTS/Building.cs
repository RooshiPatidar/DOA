using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;
using System.IO;

namespace RTS
{
    public abstract class Building
    {
        public int tileWidth;
        public int tileHeight;
        //public Tile[,] tiles;
        public Rectangle rect;

        public Building(Tile topLeftTile)
        {

        }

        
    }
}
