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
    class Archer : Actor
    {

        public enum Armor
        {

            Leather = 5,
            ReinforcedLeather = 10,
            Dragonskin = 20,
        }

        public static String[] commands = { "Move", "Attack", "Hold Position" };
        public static int StartingGoldCost = 125;
        public static int StartingResourceCost = 30;
        public static int StartingTimeToSpawn = 720;
        public static int StartingHPRegen = 60;
        public static int StartingCooldown = 18;
        public static int StartingSight = 100;
        public static int StartingFoodNeeded = 1;
        public static int StartingHealth = 150;
        public static int StartingDamage = 10;
        public static int StartingRange = 80;
        public static int StartingSpeed = 7;

        int goldCost = StartingGoldCost;
        int resourceCost = StartingResourceCost;
        int timeToSpawn = StartingTimeToSpawn;
        int healthRegen = StartingHPRegen;
        int cooldown = StartingCooldown;
        int sight = StartingSight;
        int foodNeeded = StartingFoodNeeded;
        int health = StartingHPRegen;
        int damage = StartingDamage;
        int speed = StartingSpeed;

        public Archer(Point location, Boolean enemy) : base(location, enemy, Archer.StartingSpeed, Archer.StartingHealth)
        {

        }
    }
}
