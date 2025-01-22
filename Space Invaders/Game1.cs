using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Space_Invaders
{
    enum Screen
    {
        Intro,
        Main,
        Options,
        End,
        Win

    }
    public class Game1 : Game
    {
        Screen screen;
        Random generator = new Random();
        bool clickedKeyboard, clickedSpace;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardState oldState;
        Rectangle window, bgRect1, bgRect2, optionsRect, optionsRectTitle, keyboardRect, backRect, spaceRect, playRect;
        Texture2D xwingTexture, bgTexture, laserTexture, optionsTexture, keyboardTexture, backTexture, spaceTexture, tieTexture, playTexture;
        XWing xwing;
        List<TieFighter> tieFighter;
        Vector2 bgSpeed;
        KeyboardState keyboardState;
        List<Laser> lasers;
        SoundEffect laserSound, saberHover;
        SoundEffect introTheme, battleTheme, forceTheme, endTheme;
        SoundEffectInstance introThemeInstance, battleThemeInstance, forceThemeInstance, saberHoverInstance, endThemeInstance;
        MouseState mouseState;
        SpriteFont textFont, smallTextFont, largeTextFont;
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
            this.Window.Title = "Sam's Space Invaders";
            score = 0;
            lasers = new List<Laser>();
            tieFighter = new List<TieFighter>();
            window = new Rectangle(0, 0, 800, 900);
            bgRect1 = new Rectangle(0, 0, 800, 900);
            bgRect2 = new Rectangle(0, -900, 800, 900);
            optionsRect = new Rectangle(0, 50, 50, 50);
            keyboardRect = new Rectangle(300, 450, 200, 150);
            spaceRect = new Rectangle(250, 50, 350, 75);
            optionsRectTitle = new Rectangle(750, 850, 50, 50);
            backRect = new Rectangle(0, 0, 100, 50);
            playRect = new Rectangle(700, 0, 100, 50);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();
            bgSpeed = new Vector2(0, 4);
            base.Initialize();
            xwing = new XWing(xwingTexture, new Rectangle(350, 820, 100, 75), new Vector2(0, 0));
            for (int i = 0; i <= 100; i++)
            {
                tieFighter.Add(new TieFighter(tieTexture, new Rectangle(generator.Next(10, 750), generator.Next(-3500, -100), 50, 75), new Vector2(0, 0)));
            }

            screen = Screen.Intro;
            introThemeInstance.IsLooped = true;
            battleThemeInstance.IsLooped = true;
            forceThemeInstance.IsLooped = true;
            endThemeInstance.IsLooped = true;
            saberHoverInstance.IsLooped = true;
            clickedKeyboard = false;
            clickedSpace = false;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            xwingTexture = Content.Load<Texture2D>("x-wing");
            tieTexture = Content.Load<Texture2D>("tieFighter");
            keyboardTexture = Content.Load<Texture2D>("cleanKeyboard");
            bgTexture = Content.Load<Texture2D>("spaceInvadersBG");
            laserTexture = Content.Load<Texture2D>("laser_X-Wing1");
            optionsTexture = Content.Load<Texture2D>("optionsTab");
            backTexture = Content.Load<Texture2D>("backButton");
            spaceTexture = Content.Load<Texture2D>("SpaceBar");
            playTexture = Content.Load<Texture2D>("playButton");

            laserSound = Content.Load<SoundEffect>("laser");
            saberHover = Content.Load<SoundEffect>("lightsaber");
            introTheme = Content.Load<SoundEffect>("Star Wars");
            battleTheme = Content.Load<SoundEffect>("StarWarsBattle");
            forceTheme = Content.Load<SoundEffect>("ForceTheme");
            endTheme = Content.Load<SoundEffect>("EndTheme");

            textFont = Content.Load<SpriteFont>("8bitText");
            smallTextFont = Content.Load<SpriteFont>("smallText");
            largeTextFont = Content.Load<SpriteFont>("LargeFont");

            introThemeInstance = introTheme.CreateInstance();
            battleThemeInstance = battleTheme.CreateInstance();
            forceThemeInstance = forceTheme.CreateInstance();
            endThemeInstance = endTheme.CreateInstance();
            saberHoverInstance = saberHover.CreateInstance();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        { 
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();


            if (screen != Screen.Options)
            {
                bgRect1.Offset(bgSpeed);
                bgRect2.Offset(bgSpeed);
                if (bgRect1.Y > 900)
                    bgRect1.Y = -900;
                if (bgRect2.Y > 900)
                    bgRect2.Y = -900;
            }
            





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

                }
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    screen = Screen.Main;
                }
            }



            if (screen == Screen.Main)
            {
                introThemeInstance.Stop();
                forceThemeInstance.Stop();
                battleThemeInstance.Play();
                
                
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (optionsRect.Contains(mouseState.Position))
                    {
                        screen = Screen.Options;
                    }
                }
                for (int i = 0; i < (lasers.Count); i++)
                {
                    lasers[i].Move(window);
                    if (!window.Contains(lasers[i].Rectangle))
                    {
                        lasers.RemoveAt(i);
                        i--;
                    }
                }
                for (int i = 0; i < (tieFighter.Count); i++)
                {
                    tieFighter[i].Move(window);
                    if (xwing.Rectangle.Intersects(tieFighter[i].Rectangle) || tieFighter[i].Y >= 900)
                    {
                        screen = Screen.End;
                    }
                }
                for (int i = 0; i < (tieFighter.Count); i++)
                {
                    for (int j = 0; j < (lasers.Count); j++)
                    {
                        if (tieFighter[i].Collide(lasers[j].Rectangle))
                        {
                            tieFighter.RemoveAt(i);

                            i--;

                            
                            lasers.RemoveAt(j);
                            j--;
                            score += 250;
                        }
                    }
                    
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
                if (backRect.Contains(mouseState.Position) || keyboardRect.Contains(mouseState.Position) || spaceRect.Contains(mouseState.Position) || playRect.Contains(mouseState.Position))
                {
                    saberHoverInstance.Play();
                }
                else if (!backRect.Contains(mouseState.Position) || !keyboardRect.Contains(mouseState.Position) || !spaceRect.Contains(mouseState.Position) || !playRect.Contains(mouseState.Position))
                {
                    saberHoverInstance.Stop();
                }
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (keyboardRect.Contains(mouseState.Position))
                    {
                        clickedKeyboard = true;
                    }
                    else if (spaceRect.Contains(mouseState.Position))
                    {
                        clickedSpace = true;
                    }
                    else if (backRect.Contains(mouseState.Position))
                    {
                        screen = Screen.Intro;
                    }
                    else if (playRect.Contains(mouseState.Position))
                    {
                        screen = Screen.Main;
                    }
                    else
                    {
                        clickedKeyboard = false;
                        clickedSpace = false;
                    }
                }
            }



            if (screen == Screen.End)
            {
                battleThemeInstance.Stop();
                introThemeInstance.Stop();
                forceThemeInstance.Stop();
                endThemeInstance.Play();
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
                _spriteBatch.DrawString(textFont, "Space Invaders", new Vector2(80, 50), Color.DarkGoldenrod);
                _spriteBatch.DrawString(textFont, "Space Invaders", new Vector2(74, 44), Color.White);
                _spriteBatch.DrawString(textFont, "Space Invaders", new Vector2(77, 47), Color.Yellow);
                _spriteBatch.DrawString(textFont, "Press ENTER", new Vector2(124, 439), Color.White);
                _spriteBatch.DrawString(textFont, "Press ENTER", new Vector2(130, 445), Color.DarkCyan);
                _spriteBatch.DrawString(textFont, "Press ENTER", new Vector2(127, 442), Color.LightBlue);
                _spriteBatch.DrawString(textFont, "to continue...", new Vector2(124, 497), Color.White);
                _spriteBatch.DrawString(textFont, "to continue...", new Vector2(130, 503), Color.DarkCyan);
                _spriteBatch.DrawString(textFont, "to continue...", new Vector2(127, 500), Color.LightBlue);
                _spriteBatch.Draw(optionsTexture, optionsRectTitle, Color.White);
            }
            if (screen == Screen.Options)
            {
                _spriteBatch.Draw(bgTexture, window, Color.White);
                

                if (keyboardRect.Contains(mouseState.Position))
                {
                    _spriteBatch.Draw(keyboardTexture, new Rectangle(290, 440, 220, 170), Color.LightBlue);
                    
                }
                if (spaceRect.Contains(mouseState.Position))
                {
                    _spriteBatch.Draw(spaceTexture, new Rectangle(240, 40, 370, 95), Color.LightCyan);

                }
                if (backRect.Contains(mouseState.Position))
                {
                    _spriteBatch.Draw(backTexture, new Rectangle(-10, -10, 120, 70), Color.LightBlue);

                }
                if (playRect.Contains(mouseState.Position))
                {
                    _spriteBatch.Draw(playTexture, new Rectangle(690, -10, 120, 70), Color.LightBlue);

                }

                if (clickedKeyboard == true)
                {
                    _spriteBatch.DrawString(smallTextFont, "Movement:", new Vector2(260, 630), Color.White); 
                    _spriteBatch.DrawString(smallTextFont, "[<-] = Left", new Vector2(120, 700), Color.White);
                    _spriteBatch.DrawString(smallTextFont, "[->] = Right", new Vector2(450, 700), Color.White);
                    
                }
                if (clickedSpace == true)
                {
                    _spriteBatch.DrawString(smallTextFont, "Press to Shoot", new Vector2(220, 130), Color.White);
                }
                _spriteBatch.Draw(backTexture, backRect, Color.White);
                _spriteBatch.Draw(keyboardTexture, keyboardRect, Color.White);
                _spriteBatch.Draw(spaceTexture, spaceRect, Color.White);
                _spriteBatch.Draw(playTexture, playRect, Color.White);
            }

            if (screen == Screen.Main)
            {
                
                _spriteBatch.Draw(bgTexture, bgRect1, Color.White);
                _spriteBatch.Draw(bgTexture, bgRect2, Color.White);
                _spriteBatch.DrawString(smallTextFont, "Score: " + score, new Vector2(0, 0), Color.White);
                for (int i = 0; i < (tieFighter.Count); i++)
                {
                    tieFighter[i].Draw(_spriteBatch); 
                }
                for (int i = 0; i < (lasers.Count); i++)
                {
                    lasers[i].Draw(_spriteBatch);
                }
                xwing.Draw(_spriteBatch);
                _spriteBatch.Draw(optionsTexture, optionsRect, Color.White);
                if (score >= 25250)
                {
                    _spriteBatch.DrawString(largeTextFont, "GAME", new Vector2(64, 154), Color.DarkGoldenrod);
                    _spriteBatch.DrawString(largeTextFont, "GAME", new Vector2(50, 140), Color.White);
                    _spriteBatch.DrawString(largeTextFont, "GAME", new Vector2(57, 147), Color.Yellow);
                }
            }
            if (screen == Screen.End)
            {
                _spriteBatch.DrawString(largeTextFont, "GAME", new Vector2(64, 154), Color.DarkRed);
                _spriteBatch.DrawString(largeTextFont, "GAME", new Vector2(50, 140), Color.LightCoral);
                _spriteBatch.DrawString(largeTextFont, "GAME", new Vector2(57, 147), Color.Red);
                _spriteBatch.DrawString(largeTextFont, "OVER", new Vector2(64, 554), Color.DarkRed);
                _spriteBatch.DrawString(largeTextFont, "OVER", new Vector2(50, 540), Color.LightCoral);
                _spriteBatch.DrawString(largeTextFont, "OVER", new Vector2(57, 547), Color.Red);
            }

            
            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
