using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    public class Laser
    {
        private Texture2D _texture;
        private Rectangle _rectangle;
        private Vector2 _speed;
        KeyboardState keyboardState;
        public Laser(Texture2D texture, Rectangle rectangle, Vector2 speed)
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
            set { _rectangle = value; }
        }
        public void Move(Rectangle window)
        {
            _rectangle.Offset(_speed);
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                _speed.Y = -16;
                
            }

               
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectangle, Color.White);
        }
    }
}

