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

    
    class Footman : Actor
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

        public static String[] commands = { "Move", "Attack", "Hold Position"};
        public static int StartingGoldCost = 100;
        public static int StartingResourceCost = 15;
        public static int StartingTimeToSpawn = 600;
        public static int StartingHPRegen = 60;
        public static int StartingCooldown = 45;
        public static int StartingSight = 65;
        public static int StartingFoodNeeded = 1;
        public static int StartingHealth = 200;
        public static int StartingDamage = 20;
        public static int StartingSpeed = 5;

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
        
        public Footman(Point location, Boolean enemy) : base(location, enemy, Footman.StartingSpeed, Footman.StartingHealth)
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
