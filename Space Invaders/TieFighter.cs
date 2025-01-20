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
    internal class TieFighter
    {
        private Texture2D _texture;
        private Rectangle _rectangle;
        private Vector2 _speed;
        private Random generator = new Random();
        KeyboardState keyboardState;
        public TieFighter(Texture2D texture, Rectangle rectangle, Vector2 speed)
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
            _rectangle.Offset(_speed);

            _speed.Y = generator.Next(1, 2);

        }

        public bool Collide(Rectangle item)
        {
            return _rectangle.Intersects(item);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectangle, Color.White);
        }
    }
}
