using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;

namespace RTS
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {

                Form frm = (Form)Form.FromHandle(game.Window.Handle);
                frm.FormBorderStyle = FormBorderStyle.None; 
                /*
                frm.Size = new Size(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, 
                    GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);*/
                game.Run();
            }
        }
    }
#endif
}

