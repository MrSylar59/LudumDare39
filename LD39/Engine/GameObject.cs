using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD39.Engine
{
    abstract class GameObject
    {
        protected Sprite _sprite;
        protected Vector2 _position;
        protected Vector2 _velocity;
        protected Rectangle _hitBox;
        protected bool _isSolide = false;

        public virtual void Update(GameTime pGameTime)
        {
            _sprite.SetPos(_position);
        }

        public virtual void Draw(SpriteBatch pSpriteBatch)
        {
            _sprite.Draw(pSpriteBatch);
            if (Constants.DebugMode)
                pSpriteBatch.Draw(Ressources.Images["Hitbox"], new Rectangle(_hitBox.X, _hitBox.Y, _hitBox.Width, _hitBox.Height), Color.White);
        }

        public virtual bool CollideWith(GameObject other)
        {
            return _hitBox.Intersects(other._hitBox);
        }

        public Vector2 GetPosition()
        {
            return _position;
        }

        public void SetPosition(Vector2 pPos)
        {
            _position = pPos;
        }

        public void SetTexture(Texture2D pNewTexture)
        {
            _sprite.UpdateTexture(pNewTexture);
        }

        public bool IsSolid()
        {
            return _isSolide;
        }
    }
}
