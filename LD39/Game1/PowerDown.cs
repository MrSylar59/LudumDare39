using LD39.Engine;
using Microsoft.Xna.Framework;

namespace LD39.Game1
{
    class PowerDown : GameObject
    {
        float lifeSpan;
        public PowerDown(Vector2 pPos)
        {
            _sprite = new Sprite(Ressources.Images["Powerdown"], new Vector2(pPos.X, pPos.Y), Constants.TextureWidth, Constants.TextureHeight);
            _position = pPos;
            _hitBox = new Rectangle((int)pPos.X, (int)pPos.Y, Constants.TextureWidth, Constants.TextureHeight);
            _velocity = Vector2.Zero;
            lifeSpan = 10f;
        }

        public float GetLifeSpan()
        {
            return lifeSpan;
        }

        public void DecreaseLifeSpan(float pAmount)
        {
            lifeSpan -= pAmount;
        }
    }
}
