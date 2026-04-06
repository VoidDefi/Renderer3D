using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Renderer3D.Graphics.Engine3D.Shapes
{
    public class Plane
    {
        public Camera3D Camera { get; private set; }

        public LightSource LightSource { get; private set; }

        public Plane(Camera3D camera, LightSource lightSource)
        {
            Camera = camera;
            LightSource = lightSource;
        }

        public static int VertexCount => 6; 

        public Vertex3D[] Vertices { get; init; } = new Vertex3D[VertexCount];

        public short[] Indices { get; init; } = new short[] { 0, 1, 2, 3, 4, 5 };

        public void Setup(Vector3 position, Vector2 size, Vector3 rotation, Color color, Vector3? customNormal = null)
        {
            float halfWidth = size.X / 2;
            float halfHeight = size.Y / 2;

            //Calculate Points
            Vector3 leftTop = Vector3.Zero;
            leftTop.X -= halfWidth;
            leftTop.Y -= halfHeight;

            Vector3 rightTop = Vector3.Zero;
            rightTop.X += halfWidth;
            rightTop.Y -= halfHeight;

            Vector3 leftDown = Vector3.Zero;
            leftDown.X -= halfWidth;
            leftDown.Y += halfHeight;

            Vector3 rightDown = Vector3.Zero;
            rightDown.X += halfWidth;
            rightDown.Y += halfHeight;

            /*
            
            leftTop ------ rightTop
            |                     | 
            |                     |
            |                     |
            leftDown ---- rightDown

            */

            //Transform Points

            Matrix translation = 
                Matrix.CreateRotationX(rotation.X) * 
                Matrix.CreateRotationY(rotation.Y) *
                Matrix.CreateRotationZ(rotation.Z) *
                Matrix.CreateTranslation(position.X, position.Y, position.Z);

            leftTop = Vector3.Transform(leftTop, translation);
            rightTop = Vector3.Transform(rightTop, translation);
            leftDown = Vector3.Transform(leftDown, translation);
            rightDown = Vector3.Transform(rightDown, translation);

            //Create Vertices

            Vector3 normal = Vector3.Zero;

            if (customNormal.HasValue)
            {
                normal = customNormal.Value;
            }

            else
            {
                normal = (leftTop + leftDown) / 2;
                normal = Vector3.Transform(normal, Matrix.CreateRotationY(MathF.PI / 2));
            }

            //First Triangle
            Vertices[0] = new Vertex3D(leftTop, color, Vector2.Zero, normal);
            Vertices[1] = new Vertex3D(leftDown, color, Vector2.UnitY, normal);
            Vertices[2] = new Vertex3D(rightTop, color, Vector2.UnitX, normal);

            //Second Triangle
            Vertices[3] = new Vertex3D(rightTop, color, Vector2.UnitX, normal);
            Vertices[4] = new Vertex3D(leftDown, color, Vector2.UnitY, normal);
            Vertices[5] = new Vertex3D(rightDown, color, Vector2.One, normal);
        }

        public void Draw()
        {
            GraphicsDevice device = Main.Instance.GraphicsDevice;
            
            device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, Vertices, 0, VertexCount, Indices, 0, VertexCount / 3);
        }
    }
}
