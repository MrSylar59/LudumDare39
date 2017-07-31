using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD39.Engine
{
    public class Sprite
    {
        protected Texture2D _texture;
        protected Rectangle _destRectangle;
        protected Rectangle _sourceRectangle;
        protected Vector2 _pos;
        protected int _width, _height;
        protected float _scale;

        public Sprite(Texture2D pTexture, Vector2 pPos, int pWidth, int pHeight)
        {
            _texture = pTexture;
            _width = pWidth;
            _height = pHeight;
            _pos = pPos;
            _scale = 1.0f;
            _destRectangle = new Rectangle((int)pPos.X, (int)pPos.Y, pWidth, pHeight);
            _sourceRectangle = new Rectangle(0, 0, pWidth, pHeight);
        }

        public virtual void Draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(_texture, _destRectangle, _sourceRectangle, Color.White);
        }

        public Vector2 GetPosition()
        {
            return _pos;
        }

        public void SetPos(Vector2 pPos)
        {
            _destRectangle = new Rectangle((int)pPos.X, (int)pPos.Y, _width, _height);
            _pos = pPos;
        }

        public void UpdateTexture(Texture2D pNewTexture)
        {
            _texture = pNewTexture;
        }

        public float GetScale()
        {
            return _scale;
        }

        public void ChangeScale(float pScale)
        {
            _scale = pScale;
            _destRectangle = new Rectangle((int)_pos.X, (int)_pos.Y, (int)System.Math.Floor(_width*pScale), (int)System.Math.Floor(_height*pScale));
        }
    }
}
