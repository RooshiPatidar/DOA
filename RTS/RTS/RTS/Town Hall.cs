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
    public class TownHall : Building
    {
        public static String[] actions = { "Peasant", "Call To Arms", "Back To Work"};
        public static int pixelWidth = 128;
        public static int pixelHeight = 128;
        public static int tileWidth = 4;
        public static int tileHeight = 4;
        public static int totalHealth = 1000;
        public int x;
        public int y;
        public int percentCompleted;
        public int health;
        public static Texture2D halfCompleted;
        public static Texture2D completed;
        //public Rectangle rect;
        public List<Peasant> peasants;

        public TownHall(Tile topLeftTile) : base(topLeftTile)
        {
            percentCompleted = 100;
            health = totalHealth;
            rect = new Rectangle(topLeftTile.locationX, topLeftTile.locationY, TownHall.pixelWidth, TownHall.pixelHeight);
            peasants = new List<Peasant>();
        }

        public Texture2D getTexture()
        {
            if (percentCompleted != 100 || health < 500)
            {
                return halfCompleted;
            }
            else
            {
                return completed;
            }
        }
      
    }
}
