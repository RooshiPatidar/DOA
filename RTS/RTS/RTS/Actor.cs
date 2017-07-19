using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RTS
{
    public enum Mode
    {
        Wait,
        Protect,
        Attack,
    }

    public enum Angle
    {
        Up,
        UpRight,
        Right,
        DownRight,
        Down,
        DownLeft,
        Left,
        UpLeft,
    }
    public abstract class Actor
    {
        public Mode mode;
        public int speed;
        public double health;
        public double XLoc;
        public double YLoc;
        public Rectangle rect;
        public int finalX;
        public int finalY;
        public Angle angle;
        public double movingAngle;
        public List<Actor> fighting;
        public int awareness;
        public int baseX;
        public int baseY;
        public Boolean enemy;
        public static Texture2D icon;
        public Boolean movingOutOfTheWay;
        public bool intersectingAnotherActor;
        public static Random rand = new Random();
        
        
        public Actor(Point location, Boolean enemy, int speed, int health)
        {
            XLoc = location.X;
            YLoc = location.Y;
            rect = new Rectangle((int)XLoc, (int)YLoc, 75, 79);
            this.speed = speed;
            this.health = health;
            fighting = new List<Actor>();
            mode = Mode.Protect;
            movingOutOfTheWay = false;
            this.enemy = enemy;
            intersectingAnotherActor = false;

        }

        
        public void move(int x, int y)
        {
            
            finalX = x + rand.Next(-30, 30);
            finalY  = y + rand.Next(-30, 30);
            movingAngle = Math.Atan2(YLoc - finalY, XLoc - finalX);

        }

        float PointPairToBearingDegrees(Vector2 startingPoint, Vector2 endingPoint)
        {
            Vector2 originPoint = new Vector2(endingPoint.X - startingPoint.X, endingPoint.Y - startingPoint.Y); // get origin point to origin by subtracting end from start
            float bearingRadians = (float)Math.Atan2(originPoint.Y, originPoint.X); // get bearing in radians
            float bearingDegrees = (float)(bearingRadians * (180.0 / Math.PI)); // convert to degrees
            bearingDegrees = (float)(bearingDegrees > 0.0 ? bearingDegrees : (360.0 + bearingDegrees)); // correct discontinuity
            return bearingDegrees;
        }

        public void updateAngle()
        {


            float angle = PointPairToBearingDegrees(new Vector2((float)XLoc, (float)YLoc), new Vector2(finalX, finalY));
            
            if ((angle > 0 && angle < 22.5) || angle > 337.5)
            {
                this.angle = Angle.Left;
            }
            else if (angle > 22.5 && angle < 67.5)
            {
                this.angle = Angle.DownLeft;
            }
            else if (angle > 67.5 && angle < 112.5)
            {
                this.angle = Angle.Down;
            }
            else if (angle > 112.5 && angle < 157.5)
            {
                this.angle = Angle.DownRight;

            }
            else if (angle > 157.5 && angle < 202.5)
            {
                this.angle = Angle.Right;
            }
            else if (angle > 202.5 && angle < 247.5)
            {
                this.angle = Angle.UpRight;

            }
            else if (angle > 247.5 && angle < 292.5)
            {
                this.angle = Angle.Up;
            }
            else if (angle > 292.5 && angle < 337.5)
            {
                this.angle = Angle.UpLeft;
            }

        }

        public void outOfTheWay(bool left, bool up)
        {
            if (left)
            {
                rect.X -= speed;
            } else
            {
                rect.X += speed;
            }
            if (up)
            {
                rect.Y -= speed;
            } else
            {
                rect.Y += speed;
            }
        }

        public void updateMovement()
        {
            

            if (finalX > 0)
            {
                if (XLoc + 38 < finalX)
                {
                    XLoc += Math.Abs(speed * Math.Cos(movingAngle));
                }
                if (XLoc + 38 > finalX)
                {
                    XLoc -= Math.Abs(speed * Math.Cos(movingAngle));
                }
            }
            if (finalY > 0)
            {
                if (YLoc+38 < finalY)
                {
                    YLoc += Math.Abs(speed * Math.Sin(movingAngle));
                }
                if (YLoc+38 > finalY)
                {
                    YLoc -= Math.Abs(speed * Math.Sin(movingAngle));
                }
            }
            if (XLoc == finalX)
            {
                finalX = -100;
            }
            if (YLoc == finalY)
            {
                finalY = -100;
            }
            
            rect.X = (int)XLoc;
            rect.Y = (int)YLoc;
            
        }
        
        public double getDistance(double X2, double Y2)
        {
            return Math.Sqrt(Math.Pow((X2 - XLoc), 2) + Math.Pow((Y2 - YLoc), 2));
        }

        public Actor getClosestEnemy(List<Actor> enemiesNearby)
        {
            double distance = Int16.MaxValue;
            Actor closest = null;
            for (int i = 0; i < enemiesNearby.Count; i++)
            {
                double length = getDistance(enemiesNearby[i].XLoc, enemiesNearby[i].YLoc);
                if (distance < length)
                {
                    closest = enemiesNearby[i];
                    distance = length;
                }

            }

            return closest;
        }
    }
}
