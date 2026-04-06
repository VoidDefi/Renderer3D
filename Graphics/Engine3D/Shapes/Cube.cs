using Microsoft.Xna.Framework;
using System;

namespace Renderer3D.Graphics.Engine3D.Shapes
{
    public class Cube
    {
        public Camera3D Camera { get; private set; }

        public LightSource LightSource { get; private set; }

        public Cube(Camera3D camera, LightSource lightSource)
        {
            Camera = camera;
            LightSource = lightSource;
        }

        public static int PlanesCount => 6;

        public Plane[] Planes { get; private set; } = null;

        public void Setup(Vector3 position, Vector2 size, Vector3 rotation, Color color)
        {
            if (Planes == null)
            {
                Planes = new Plane[6];

                for (int i = 0; i < Planes.Length; i++)
                {
                    Planes[i] = new Plane(Camera, LightSource);
                }
            }

            Matrix rotationMatrix =
                Matrix.CreateRotationX(rotation.X) *
                Matrix.CreateRotationY(rotation.Y) *
                Matrix.CreateRotationZ(rotation.Z);

            float halfHeight = size.Y / 2;

            Vector3 up = new Vector3(0, halfHeight, 0);
            up = Vector3.Transform(up, rotationMatrix);

            Vector3 down = new Vector3(0, -halfHeight, 0);
            down = Vector3.Transform(down, rotationMatrix);

            Vector3 back = new Vector3(0, 0, -halfHeight);
            back = Vector3.Transform(back, rotationMatrix);

            Vector3 front = new Vector3(0, 0, halfHeight);
            front = Vector3.Transform(front, rotationMatrix);

            Vector3 left = new Vector3(-halfHeight, 0, 0);
            left = Vector3.Transform(left, rotationMatrix);

            Vector3 right = new Vector3(halfHeight, 0, 0);
            right = Vector3.Transform(right, rotationMatrix);

            Planes[0].Setup(position + up, size, rotation + new Vector3(MathF.PI / 2f, 0, 0), color, up);
            Planes[1].Setup(position + down, size, rotation + new Vector3(MathF.PI / 2f, 0, 0), color, down);
            Planes[2].Setup(position + back, size, rotation, color, back);
            Planes[3].Setup(position + front, size, rotation, color, front);

            //left
            Planes[4].Vertices[0] = new(Planes[0].Vertices[0].position, color, Vector2.Zero, left);
            Planes[4].Vertices[1] = new(Planes[1].Vertices[0].position, color, Vector2.UnitY, left);
            Planes[4].Vertices[2] = new(Planes[1].Vertices[1].position, color, Vector2.UnitX, left);

            Planes[4].Vertices[3] = new(Planes[0].Vertices[0].position, color, Vector2.UnitX, left);
            Planes[4].Vertices[4] = new(Planes[3].Vertices[0].position, color, Vector2.UnitY, left);
            Planes[4].Vertices[5] = new(Planes[3].Vertices[1].position, color, Vector2.One, left);

            //right
            Planes[5].Vertices[0] = new(Planes[0].Vertices[2].position, color, Vector2.Zero, right);
            Planes[5].Vertices[1] = new(Planes[1].Vertices[2].position, color, Vector2.UnitY, right);
            Planes[5].Vertices[2] = new(Planes[1].Vertices[5].position, color, Vector2.UnitX, right);

            Planes[5].Vertices[3] = new(Planes[0].Vertices[2].position, color, Vector2.UnitX, right);
            Planes[5].Vertices[4] = new(Planes[3].Vertices[2].position, color, Vector2.UnitY, right);
            Planes[5].Vertices[5] = new(Planes[3].Vertices[5].position, color, Vector2.One, right);
        }

        public void Draw()
        {
            if (Planes == null) return;

            foreach (Plane plate in Planes)
            {
                plate.Draw();
            }
        }
    }
}
