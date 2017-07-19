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
    class Position
    {
        public int x;
        public int y;
        public int speed;
        public Boolean clickable;

        public Position(int x, int y, int speed, Boolean clickable)
        {
            this.x = x;
            this.y = y;
            this.speed = speed;
            this.clickable = clickable;
        }
    }
}
