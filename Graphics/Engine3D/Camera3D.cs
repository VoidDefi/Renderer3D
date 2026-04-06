using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Renderer3D.Graphics.Engine3D
{
    public class Camera3D
    {
        public GraphicsDevice Device { get; private set; }

        public Matrix Transform { get; private set; }

        public Vector3 Position { get; set; }

        public Vector3 Target { get; set; }

        public Camera3D(GraphicsDevice device)
        {
            Device = device;
        }

        public void Update() 
        { 
            Matrix worldMatrix = Matrix.Identity;
            Matrix viewMatrix = Matrix.CreateLookAt(Position, Target, Vector3.Up);

            Matrix projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                Device.Viewport.AspectRatio,
                1.0f, 300.0f);

            Transform = worldMatrix * viewMatrix * projectionMatrix;
        }
    }
}
