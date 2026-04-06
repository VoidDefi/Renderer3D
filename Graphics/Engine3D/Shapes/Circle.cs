using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renderer3D.Graphics.Engine3D.Shapes
{
    public class Circle
    {
        public Camera3D Camera { get; private set; }

        public LightSource LightSource { get; private set; }

        public Circle(Camera3D camera, LightSource lightSource)
        {
            Camera = camera;
            LightSource = lightSource;
        }

        public Vertex3D[] Vertices { get; set; } = new Vertex3D[0];

        public short[] Indices { get; set; } = new short[0];

        public void Setup(Vector3 position, float radius, Vector3 rotation, Color color, int anglesCount, Vector3? customNormal = null)
        {
            if (anglesCount < 4) return;

            //Resize Vertices

            int vertexCount = anglesCount + 1;

            if (Vertices.Length != vertexCount)
                Vertices = new Vertex3D[vertexCount];

            if (Indices.Length != vertexCount * 3 - 3)
                Indices = new short[vertexCount * 3 - 3];

            //Set Center Vertex

            Vertices[0] = new Vertex3D(Vector3.Zero, color, Vector2.One / 2f, Vector3.Backward);

            //Ran Circle Vertex
            float angle = 0;
            float addAngle = MathF.PI * 2 / anglesCount;

            for (int i = 1; i < vertexCount; i++)
            {
                Vector3 anglePos = Vector3.UnitX * radius;

                Matrix rotate = Matrix.CreateRotationY(angle);

                anglePos = Vector3.Transform(anglePos, rotate);

                Vertices[i] = new Vertex3D(anglePos, color, Vector2.One / 2f, Vector3.Backward);
                angle += addAngle;

                int index = (i - 1) * 3;

                Indices[index] = (short)i;
                Indices[index + 1] = (short)(i + 1);
                Indices[index + 2] = 0;
            }

            //Set Last Vertex First
            Indices[Indices.Length - 2] = 1;

            //Transform Points
            Matrix translation =
                Matrix.CreateRotationX(rotation.X) *
                Matrix.CreateRotationY(rotation.Y) *
                Matrix.CreateRotationZ(rotation.Z) *
                Matrix.CreateTranslation(position.X, position.Y, position.Z);

            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i].position = Vector3.Transform(Vertices[i].position, translation);
            }

            Vector3 normal = Vector3.Zero;

            if (customNormal.HasValue)
            {
                normal = customNormal.Value;
            }

            else
            {
                normal = Vertices[0].position - Vertices[1].position;
                normal = Vector3.Transform(normal, Matrix.CreateRotationY(-MathF.PI / 2));
            }

            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i].normal = normal;
            }
        }

        public void Draw()
        {
            if (Vertices.Length >= 4 && Indices.Length > 6)
            {
                GraphicsDevice device = Main.Instance.GraphicsDevice;

                device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, Vertices, 0, Vertices.Length, Indices, 0, Vertices.Length - 1);
            }
        }
    }
}
