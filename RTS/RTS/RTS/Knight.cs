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

    

    class Knight : Actor
    {

        public enum Sword
        {
            Iron,
            Steel,
            Titanium,
        }
        public enum Armor
        {

            Iron = 5,
            Steel = 15,
            Titanium = 30,
        }

        public static int StartingGoldCost = 200;
        public static int StartingResourceCost = 30;
        public static int StartingTimeToSpawn = 1080;
        public static int StartingHPRegen = 40;
        public static int StartingCooldown = 72;
        public static int StartingSight = 90;
        public static int StartingFoodNeeded = 3;
        public static int StartingHealth = 350;
        public static int StartingDamage = 35;
        public static int StartingSpeed = 6;

        int goldCost = StartingGoldCost;
        int resourceCost = StartingResourceCost;
        int timeToSpawn = StartingTimeToSpawn;
        int healthRegen = StartingHPRegen;
        int cooldown = StartingCooldown;
        int sight = StartingSight;
        int foodNeeded = StartingFoodNeeded;
        int health = StartingHPRegen;
        int damage = StartingDamage;

        double baseX;
        double baseY;
        
        public Knight(Point location, Boolean enemy) : base(location, enemy, Knight.StartingSpeed, Knight.StartingHealth)
        {

        }
        public void update(List<Actor> enemiesNearby)
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
