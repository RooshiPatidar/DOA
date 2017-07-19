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
    class Mage : Actor
    {

        public enum Armor
        {

            WizardRobes = 5,
            EnchantedRobes = 15,
            BlessedRobes = 30,
        }

        public static int StartingGoldCost = 175;
        public static int StartingResourceCost = 50;
        public static int StartingTimeToSpawn = 900;
        public static int StartingHPRegen = 30;
        public static int StartingCooldown = 60;
        public static int StartingSight = 95;
        public static int StartingFoodNeeded = 2;
        public static int StartingHealth = 300;
        public static int StartingDamage = 20;
        public static int StartingRange = 50;
        public static int StartingMana = 200;
        public static int StartingManaRegen = 12;
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
        
        public Mage(Point location, Boolean enemy) : base(location, enemy, Mage.StartingSpeed, Mage.StartingHealth)
        {

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
