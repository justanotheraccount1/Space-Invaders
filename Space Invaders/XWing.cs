using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    public class XWing
    {
        private Texture2D _texture;
        private Rectangle _rectangle;
        private Vector2 _speed;
        KeyboardState keyboardState;
        public XWing(Texture2D texture, Rectangle rectangle, Vector2 speed)
        {
            _texture = texture;
            _rectangle = rectangle;
            _speed = speed;
        }
        public Texture2D Texture
        {
            get { return _texture; }
        }
        public Vector2 Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public Rectangle Rectangle
        {
            get { return _rectangle; }
        }

        public int X
        {
            get
            {
                return _rectangle.X;
            }
        }

        public void Move(Rectangle window)
        {
            keyboardState = Keyboard.GetState();

            _rectangle.Offset(_speed);
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                _speed.X = -6;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                _speed.X = 6;
            }
            if (_rectangle.X <= window.X)
            {
                _rectangle.X = (window.X + 1);
            }
            if (_rectangle.Right >= window.Right)
            {
                _rectangle.X = (window.Right - _rectangle.Width - 1);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectangle, Color.White);
        }
    }
}
