using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Space_Invaders
{
    enum Screen
    {
        Intro,
        Main,
        End

    }
    public class Game1 : Game
    {
        Screen screen;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardState oldState;
        Rectangle window, bgRect1, bgRect2;
        Texture2D xwingTexture, bgTexture, laserTexture;
        XWing xwing;
        Vector2 bgSpeed;
        KeyboardState keyboardState;
        List<Laser> lasers;
        SoundEffect laserSound;
        SoundEffect introTheme;
        SoundEffectInstance introThemeInstance;
        MouseState mouseState;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            lasers = new List<Laser>();
            window = new Rectangle(0, 0, 800, 900);
            bgRect1 = new Rectangle(0, 0, 800, 900);
            bgRect2 = new Rectangle(0, -900, 800, 900);
            
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();
            bgSpeed = new Vector2(0, 4);
            base.Initialize();
            xwing = new XWing(xwingTexture, new Rectangle(350, 820, 100, 75), new Vector2(0, 0));
            screen = Screen.Intro;
            introThemeInstance.IsLooped = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            xwingTexture = Content.Load<Texture2D>("x-wing");
            bgTexture = Content.Load<Texture2D>("spaceInvadersBG");
            laserTexture = Content.Load<Texture2D>("laser_X-Wing1");
            laserSound = Content.Load<SoundEffect>("laser");
            introTheme = Content.Load<SoundEffect>("Star Wars");
            introThemeInstance = introTheme.CreateInstance();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        { 
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            if (screen == Screen.Intro)
            {
                introThemeInstance.Play();
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    screen = Screen.Main;
                }
            }
            if (screen == Screen.Main)
            {
                introThemeInstance.Stop();
                for (int i = 0; i < (lasers.Count); i++)
                {
                    lasers[i].Move(window);
                }
                xwing.Move(window);
                bgRect1.Offset(bgSpeed);
                bgRect2.Offset(bgSpeed);
                if (bgRect1.Y > 900)
                    bgRect1.Y = -900;
                if (bgRect2.Y > 900)
                    bgRect2.Y = -900;
                if (oldState.IsKeyUp(Keys.Space) && keyboardState.IsKeyDown(Keys.Space))
                {
                    lasers.Add(new Laser(laserTexture, new Rectangle((xwing.X + 45), 800, 10, 50), new Vector2(0, 0)));
                    laserSound.Play();
                }
            }
            for (int i = 0; i < (lasers.Count); i++)
            {
                lasers[i].Move(window);
            }
            xwing.Move(window);
            bgRect1.Offset(bgSpeed);
            bgRect2.Offset(bgSpeed);
            if (bgRect1.Y > 900)
                bgRect1.Y = -900;
            if (bgRect2.Y > 900)
                bgRect2.Y = -900;
            if (oldState.IsKeyUp(Keys.Space) && keyboardState.IsKeyDown(Keys.Space))
            {
                lasers.Add(new Laser(laserTexture, new Rectangle((xwing.X + 45), 800, 10, 50), new Vector2(0, 0)));
                laserSound.Play();
            }
            if (screen == Screen.End)
            {

            }
            oldState = keyboardState;
            base.Update(gameTime);
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {

            }
            if (screen == Screen.Main)
            {
                _spriteBatch.Draw(bgTexture, bgRect1, Color.White);
                _spriteBatch.Draw(bgTexture, bgRect2, Color.White);

                for (int i = 0; i < (lasers.Count); i++)
                {
                    lasers[i].Draw(_spriteBatch);
                }
                xwing.Draw(_spriteBatch);
            }
            if (screen == Screen.End)
            {

            }

            
            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
