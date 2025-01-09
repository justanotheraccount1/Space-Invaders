﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Space_Invaders
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Rectangle window, bgRect1, bgRect2;
        Texture2D xwingTexture, bgTexture, laserTexture;
        XWing xwing;
        Laser laser;
        Vector2 bgSpeed;
        KeyboardState keyboardState;
        List<Laser> lasers;
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
            
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            xwingTexture = Content.Load<Texture2D>("x-wing");
            bgTexture = Content.Load<Texture2D>("spaceInvadersBG");
            laserTexture = Content.Load<Texture2D>("laser_X-Wing1");


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        { 
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            keyboardState = Keyboard.GetState();
            for (int i = 0; i < (lasers.Count + 1); i++)
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
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                lasers.Add(new Laser(laserTexture, new Rectangle((xwing.X + 45), 900, 10, 50), new Vector2(0, 0)));

            }
            base.Update(gameTime);
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _spriteBatch.Draw(bgTexture, bgRect1, Color.White);
            _spriteBatch.Draw(bgTexture, bgRect2, Color.White);
            xwing.Draw(_spriteBatch);
            for (int i = 0; i < (lasers.Count + 1); i++)
            {
                lasers[i].Draw(_spriteBatch);
            }
            
            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
