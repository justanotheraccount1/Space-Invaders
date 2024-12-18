using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Invaders
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Rectangle xwingRect, window;
        Texture2D xwingTexture;
        Vector2 xwingSpeed;
        KeyboardState keyboardState;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();
            window = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            xwingRect = new Rectangle(350, 820, 100, 75);
            xwingSpeed = new Vector2(0, 0);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            xwingTexture = Content.Load<Texture2D>("x-wing");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            xwingRect.Offset(xwingSpeed);
            // TODO: Add your update logic here
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                xwingSpeed.X = -4;
            }
            if (keyboardState.IsKeyDown (Keys.Right))
            {
                xwingSpeed.X = 4;
            }
            if (xwingRect.X <= window.X)
            {
                xwingRect.X = (window.X + 1);
            }
            if (xwingRect.Right >= window.Right)
            {
                xwingRect.X = (window.Right - xwingRect.Width - 1);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            _spriteBatch.Draw(xwingTexture, xwingRect, Color.White);
            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
