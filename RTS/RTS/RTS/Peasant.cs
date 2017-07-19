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

namespace RTS
{



    public class Peasant : Actor
    {
        double baseX;
        double baseY;

        public static String[] commands = { "Move", "Attack", "Hold Position", "Repair", "Gather", "Build Structure", "Call To Arms" };
        public static int StartingGoldCost = 50;
        public static int StartingResourceCost = 5;
        public static int StartingTimeToSpawn = 300;
        public static int StartingHPRegen = 120;
        public static int StartingCooldown = 60;
        public static int StartingSight = 50;
        public static int StartingFoodNeeded = 0;
        public static int StartingHealth = 100;
        public static int StartingDamage = 15;
        public static int StartingSpeed = 3;
        public static Texture2D spriteSheet;
        public static List<SoundEffect> sounds = new List<SoundEffect>();
        public static SoundEffect readyToWork;
        public static Texture2D icon;

        /* 
        public int goldCost = StartingGoldCost;
         public int resourceCost = StartingResourceCost;
         public int timeToSpawn = StartingTimeToSpawn;
         public int healthRegen = StartingHPRegen;
         public int cooldown = StartingCooldown;
         public int sight = StartingSight;
         public int foodNeeded = StartingFoodNeeded;
         public int health = StartingHPRegen;
         public int damage = StartingDamage;
         public int speed = StartingSpeed;
         */
        public static Random rand = new Random();

        public Peasant(Point location, Boolean enemy) : base(location, enemy, Peasant.StartingSpeed, Peasant.StartingHealth)
        {

            
        }

        

        public Rectangle getSourceRect()
        {
            
            Rectangle rect = Rectangle.Empty;
            if (angle == Angle.Up)
            {
                rect.X = 16;
                rect.Y = 8;
                rect.Width = 26;
                rect.Height = 30;
            } else if (angle == Angle.UpRight || angle == Angle.UpLeft)
            {
                rect.X = 54;
                rect.Y = 5;
                rect.Width = 26;
                rect.Height = 29;
            } else if (angle == Angle.Right || angle == Angle.Left)
            {
                rect.X = 90;
                rect.Y = 3;
                rect.Width = 24;
                rect.Height = 30;
            } else if (angle == Angle.DownLeft || angle == Angle.DownRight)
            {
                rect.X = 126;
                rect.Y = 2;
                rect.Width = 26;
                rect.Height = 31;
            } else if (angle == Angle.Down)
            {
                rect.X = 166;
                rect.Y = 7;
                rect.Width = 26;
                rect.Height = 27;
            }

            return rect;
        }

        public static void playSound()
        {
            sounds[rand.Next(0, sounds.Count)].Play();
        }

        public Texture2D getTexture()
        {
            return spriteSheet;
        }
        public void update()
        {
            if (mode == Mode.Attack)
            {

            }
            else if (mode == Mode.Protect)
            {
                baseX = XLoc;
                baseY = YLoc;

            }
            else if (mode == Mode.Wait)
            {

            }
        }

    }
}
