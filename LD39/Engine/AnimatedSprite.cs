using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD39.Engine
{
    class AnimatedSprite : Sprite
    {
        private int _numFrame, _currentFrame;
        private float _toNextFrame, _interval;
        private Texture2D _lastTexture;

        public AnimatedSprite(Texture2D pTexture, Vector2 pPos, int pWidth, int pHeight, int pNumFrame) 
            : base(pTexture, pPos, pWidth, pHeight)
        {
            _currentFrame = 0;
            _interval = 0.2f;
            _toNextFrame = _interval;
            _numFrame = pNumFrame;
            _lastTexture = _texture;
        }

        public override void Draw(SpriteBatch pSpriteBatch)
        {
            Rectangle sourceRect = new Rectangle(_width*_currentFrame, 0, _width, _height);
            Rectangle destRect = new Rectangle((int)_pos.X, (int)_pos.Y, _width, _height);

            pSpriteBatch.Draw(_texture, destRect, sourceRect, Color.White);
        }

        public void SetInterval(float pInterval)
        {
            _interval = pInterval;
            _toNextFrame = _interval;
        }

        public void Update(GameTime pGameTime)
        {
            _toNextFrame -= (float)pGameTime.ElapsedGameTime.TotalSeconds;
            if (_toNextFrame <= 0)
            {
                NextFrame();
                _toNextFrame = _interval;
            }
        }

        public void UpdateTexture(Texture2D pTexture, int pNumFrame, float pInterval)
        {
            if (_texture != pTexture)
            {
                _texture = pTexture;
                _numFrame = pNumFrame;
                _currentFrame = 0;
                _interval = pInterval;
            }
        }

        private void NextFrame()
        {
            _currentFrame++;
            if (_currentFrame == _numFrame)
                _currentFrame = 0;
        }
    }
}
