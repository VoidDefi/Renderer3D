using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Forms = System.Windows.Forms;
using Form = System.Windows.Forms.Form;
using Renderer3D.Graphics;

namespace Renderer3D
{
    public class Main : Game
    {
        public static Main Instance { get; private set; }

        public static GraphicsDeviceManager Graphics { get; private set; }

        public static SpriteBatch SpriteBatch { get; private set; }

        public static int ScreenWidth { get; private set; } = 1920;

        public static int ScreenHeight { get; private set; } = 1080;

        public Main()
        {
            Instance = this;

            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferWidth = ScreenWidth;
            Graphics.PreferredBackBufferHeight = ScreenHeight;
            Form form = (Form)Form.FromHandle(Window.Handle);
            form.FormBorderStyle = Forms.FormBorderStyle.None;
            form.WindowState = Forms.FormWindowState.Maximized;
            Graphics.ApplyChanges();

            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Scene.Init();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Assets.Load(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new(20, 20, 20));

            Scene.Draw();

            base.Draw(gameTime);
        }
    }
}
