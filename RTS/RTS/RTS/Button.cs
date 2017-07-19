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
    class Button
    {
        public Boolean clickable;
        public Rectangle rect;
        public Dictionary<Int32, Position> positions;
        public Texture2D pressed;
        public Texture2D unpressed;
        public Boolean finishedMoving;


        public Button(Rectangle rect, Boolean clickable)
        {
            positions = new Dictionary<Int32, Position>();
            this.rect = rect;
            this.clickable = clickable;

        }

        public void addPosition(int positionKey, Position newPos)
        {
            positions.Add(positionKey, newPos);
        }

        public void animate(int pos)
        {
            bool finishedX = false;
            bool finishedY = false;
            if (rect.X > positions[pos].x - 10 && rect.X < positions[pos].x + 10)
            {
                finishedX = true;
            }
            else
            {
                if (rect.X < positions[pos].x)
                {
                    rect.X += positions[pos].speed;
                }
                if (rect.X > positions[pos].x)
                {
                    rect.X -= positions[pos].speed;
                }
            }

            if (rect.Y > positions[pos].y - 10 && rect.Y < positions[pos].y + 10)
            {
                finishedY = true;
            }
            else
            {
                if (rect.Y < positions[pos].y)
                {
                    rect.Y += positions[pos].speed;
                }
                if (rect.Y > positions[pos].y)
                {
                    rect.Y -= positions[pos].speed;
                }
            }
            if (finishedX && finishedY)
            {
                finishedMoving = true;
            }

        }

        public Boolean isClicked(int x, int y, int key)
        {
            if (rect.Contains(x, y) && positions[key].clickable)
            {
                return true;
            }
            return false;

        }
    }
}
