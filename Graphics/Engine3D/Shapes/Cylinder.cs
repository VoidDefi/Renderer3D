using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Renderer3D.Graphics.Engine3D.Shapes
{
    public class Cylinder
    {
        public Camera3D Camera { get; private set; }

        public LightSource LightSource { get; private set; }

        public Cylinder(Camera3D camera, LightSource lightSource)
        {
            Camera = camera;
            LightSource = lightSource;
        }

        public Plane[] Planes { get; private set; } = null;

        public Circle[] Circles { get; private set; } = null;

        public void Setup(Vector3 position, float radius, float height, Vector3 rotation, Color color, int anglesCount)
        {
            if (anglesCount < 4) return;

            if (Circles == null)
            {
                Circles = new Circle[2]
                {
                    new(Camera, LightSource),
                    new(Camera, LightSource)
                };
            }

            Matrix rotationMatrix =
                Matrix.CreateRotationX(rotation.X) *
                Matrix.CreateRotationY(rotation.Y) *
                Matrix.CreateRotationZ(rotation.Z);

            Vector3 up = new Vector3(0, height / 2, 0);
            up = Vector3.Transform(up, rotationMatrix);

            Vector3 down = new Vector3(0, -height / 2, 0);
            down = Vector3.Transform(down, rotationMatrix);

            Circles[0].Setup(position + up, radius, rotation, color, anglesCount, up);
            Circles[1].Setup(position + down, radius, rotation, color, anglesCount, down);

            if (Circles[0].Vertices.Length == Circles[1].Vertices.Length)
            {
                int planeCount = Circles[0].Vertices.Length - 1;

                if (Planes == null || Planes?.Length != planeCount)
                {
                    Planes = new Plane[planeCount];
                    for (int i = 0; i < Planes.Length; i++)
                    {
                        Planes[i] = new(Camera, LightSource);
                    }
                }

                for (int i = 0; i < planeCount; i++)
                {
                    int next = i + 2;
                    next = next > planeCount ? 1 : next;

                    Vector3 top1 = Circles[0].Vertices[i + 1].position;
                    Vector3 top2 = Circles[0].Vertices[next].position;

                    Vector3 bottom1 = Circles[1].Vertices[i + 1].position;
                    Vector3 bottom2 = Circles[1].Vertices[next].position;

                    float planeWidth = Vector3.Distance(top1, top2);

                    Vector3 top = Vector3.Lerp(top1, top2, 0.5f);
                    Vector3 bottom = Vector3.Lerp(bottom1, bottom2, 0.5f);

                    Vector3 center = Vector3.Lerp(top, bottom, 0.5f);

                    Vector3 normal = Vector3.Normalize(center - position);

                    Planes[i].Setup(bottom, top, planeWidth, normal, color);
                }
            }
        }

        public void Draw()
        {
            if (Circles == null) return;

            foreach (Circle circle in Circles)
            {
                circle.Draw();
            }

            if (Planes == null) return;

            foreach (Plane plate in Planes)
            {
                plate.Draw();
            }
        }
    }
}
