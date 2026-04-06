using Microsoft.Xna.Framework;
using Renderer3D.Graphics.Engine3D.Shapes;
using Microsoft.Xna.Framework.Graphics;
using Renderer3D.Graphics.Engine3D;
using Plane3D = Renderer3D.Graphics.Engine3D.Shapes.Plane;
using System;

namespace Renderer3D.Graphics
{
    public static class Scene
    {
        static float timer;

        static Camera3D camera;

        static LightSource lighting;

        static Plane3D plane;
        static Cube cube;

        public static void Init()
        {
            camera = new Camera3D(Main.Instance.GraphicsDevice);
            camera.Position = new Vector3(0, 3, 5);
            camera.Target = Vector3.Zero;

            lighting = new();
            lighting.Direction = new Vector3(0, 1.5f, 1);

            plane = new Plane3D(camera, lighting);
            cube = new Cube(camera, lighting);
        }

        public static void Draw()
        {
            GraphicsDevice device = Main.Instance.GraphicsDevice;
            
            camera.Update();

            Assets.Texturing.Value.Parameters["WorldViewProjection"].SetValue(camera.Transform);
            Assets.Texturing.Value.Parameters["LightDirection"].SetValue(lighting.Direction);

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            device.RasterizerState = rasterizerState;

            foreach (var pass in Assets.Texturing.Value.CurrentTechnique.Passes)
            {
                pass.Apply();

                Color color = Color.White;

                Vector3 rotation = new(0, timer, 0);

                cube.Setup(Vector3.Forward, new Vector2(0.1f, 1), rotation, color);
                cube.Draw();

                rotation.Y += MathF.PI / 2f;

                plane.Setup(Vector3.Zero, new Vector2(1, 1), rotation, color);
                plane.Draw();

                timer += MathF.PI / 50;
            }
        }
    }
}
