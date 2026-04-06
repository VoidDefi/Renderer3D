using Microsoft.Xna.Framework;
using System;

namespace Renderer3D
{
    public class Program
    {
        public static void Main()
        {
            try 
            {
                Main game = new Main();
                game.Run();
            }

            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
