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
        Options,
        End

    }
    public class Game1 : Game
    {
        Screen screen;
        bool clickedKeyboard;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardState oldState;
        Rectangle window, bgRect1, bgRect2, optionsRect, optionsRectTitle, keyboardRect;
        Texture2D xwingTexture, bgTexture, laserTexture, optionsTexture, keyboardTexture;
        XWing xwing;
        Vector2 bgSpeed;
        KeyboardState keyboardState;
        List<Laser> lasers;
        SoundEffect laserSound;
        SoundEffect introTheme, battleTheme, forceTheme;
        SoundEffectInstance introThemeInstance, battleThemeInstance, forceThemeInstance;
        MouseState mouseState;
        SpriteFont textFont, smallTextFont;
        int score;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            score = 0;
            lasers = new List<Laser>();
            window = new Rectangle(0, 0, 800, 900);
            bgRect1 = new Rectangle(0, 0, 800, 900);
            bgRect2 = new Rectangle(0, -900, 800, 900);
            optionsRect = new Rectangle(0, 50, 50, 50);
            keyboardRect = new Rectangle(300, 450, 200, 150);
            optionsRectTitle = new Rectangle(750, 850, 50, 50);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();
            bgSpeed = new Vector2(0, 4);
            base.Initialize();
            xwing = new XWing(xwingTexture, new Rectangle(350, 820, 100, 75), new Vector2(0, 0));
            
            screen = Screen.Intro;
            introThemeInstance.IsLooped = true;
            battleThemeInstance.IsLooped = true;
            forceThemeInstance.IsLooped = true;
            clickedKeyboard = false;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            xwingTexture = Content.Load<Texture2D>("x-wing");
            keyboardTexture = Content.Load<Texture2D>("cleanKeyboard");
            bgTexture = Content.Load<Texture2D>("spaceInvadersBG");
            laserTexture = Content.Load<Texture2D>("laser_X-Wing1");
            optionsTexture = Content.Load<Texture2D>("optionsTab");
            laserSound = Content.Load<SoundEffect>("laser");
            introTheme = Content.Load<SoundEffect>("Star Wars");
            battleTheme = Content.Load<SoundEffect>("StarWarsBattle");
            forceTheme = Content.Load<SoundEffect>("ForceTheme");
            textFont = Content.Load<SpriteFont>("8bitText");
            smallTextFont = Content.Load<SpriteFont>("smallText");
            introThemeInstance = introTheme.CreateInstance();
            battleThemeInstance = battleTheme.CreateInstance();
            forceThemeInstance = forceTheme.CreateInstance();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        { 
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            bgRect1.Offset(bgSpeed);
            bgRect2.Offset(bgSpeed);
            if (bgRect1.Y > 900)
                bgRect1.Y = -900;
            if (bgRect2.Y > 900)
                bgRect2.Y = -900;





            if (screen == Screen.Intro)
            {
                introThemeInstance.Play();
                forceThemeInstance.Stop();
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (optionsRectTitle.Contains(mouseState.Position))
                    {
                        screen = Screen.Options;
                    }
                    else
                    {
                        screen = Screen.Main;
                    }
                }
            }



            if (screen == Screen.Main)
            {
                introThemeInstance.Stop();
                forceThemeInstance.Stop();
                battleThemeInstance.Play();
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (optionsRectTitle.Contains(mouseState.Position))
                    {
                        screen = Screen.Options;
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
            }
            


            if (screen == Screen.Options)
            {
                battleThemeInstance.Stop();
                introThemeInstance.Stop();
                forceThemeInstance.Play();
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (keyboardRect.Contains(mouseState.Position))
                    {
                        clickedKeyboard = true;
                    }
                    else
                    {
                        clickedKeyboard = false;
                    }
                }
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
                _spriteBatch.Draw(bgTexture, bgRect1, Color.White);
                _spriteBatch.Draw(bgTexture, bgRect2, Color.White);
                _spriteBatch.DrawString(textFont, "Space Invaders", new Vector2(78, 53), Color.DarkBlue);
                _spriteBatch.DrawString(textFont, "Space Invaders", new Vector2(72, 47), Color.White);
                _spriteBatch.DrawString(textFont, "Space Invaders", new Vector2(75, 50), Color.Yellow);
                _spriteBatch.DrawString(textFont, "Click anywhere", new Vector2(72, 447), Color.White);
                _spriteBatch.DrawString(textFont, "Click anywhere", new Vector2(78, 453), Color.Indigo);
                _spriteBatch.DrawString(textFont, "Click anywhere", new Vector2(75, 450), Color.LightBlue);
                _spriteBatch.DrawString(textFont, "to continue...", new Vector2(84, 497), Color.White);
                _spriteBatch.DrawString(textFont, "to continue...", new Vector2(90, 503), Color.Indigo);
                _spriteBatch.DrawString(textFont, "to continue...", new Vector2(87, 500), Color.LightBlue);
                _spriteBatch.Draw(optionsTexture, optionsRectTitle, Color.White);
            }
            if (screen == Screen.Options)
            {
                if (keyboardRect.Contains(mouseState.Position))
                {
                    _spriteBatch.Draw(keyboardTexture, new Rectangle(290, 440, 220, 170), Color.LightBlue);
                }
                
                if (clickedKeyboard == true)
                {
                    _spriteBatch.DrawString(smallTextFont, "Movement:", new Vector2(260, 630), Color.White); 
                    _spriteBatch.DrawString(smallTextFont, "[<-] = Left", new Vector2(120, 700), Color.White);
                    _spriteBatch.DrawString(smallTextFont, "[->] = Right", new Vector2(450, 700), Color.White);
                    _spriteBatch.Draw(keyboardTexture, new Rectangle(290, 440, 220, 170), Color.LightBlue);
                }
                _spriteBatch.Draw(keyboardTexture, keyboardRect, Color.White);
            }

            if (screen == Screen.Main)
            {
                
                _spriteBatch.Draw(bgTexture, bgRect1, Color.White);
                _spriteBatch.Draw(bgTexture, bgRect2, Color.White);
                _spriteBatch.DrawString(smallTextFont, "Score: " + score, new Vector2(0, 0), Color.White);
                for (int i = 0; i < (lasers.Count); i++)
                {
                    lasers[i].Draw(_spriteBatch);
                }
                xwing.Draw(_spriteBatch);
                _spriteBatch.Draw(optionsTexture, optionsRect, Color.White);
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
