using Microsoft.Xna.Framework;

namespace Renderer3D.Graphics.Engine3D
{
    //Global light source (sun)
    public class LightSource
    {
        private Vector3 direction;

        public Vector3 Direction 
        { 
            get => direction;
            set 
            { 
                direction = value; 
                direction.Normalize();
            }
        }
    }
}
