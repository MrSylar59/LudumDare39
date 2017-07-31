using LD39.Engine;
using Microsoft.Xna.Framework;

namespace LD39.Game1
{
    class Floor : GameObject
    {
        public Floor(Vector2 pPos)
        {
            _sprite = new Sprite(Ressources.Images["Floor"], new Vector2(pPos.X, pPos.Y), Constants.TextureWidth, Constants.TextureHeight);
            _position = pPos;
            _hitBox = new Rectangle((int)pPos.X, (int)pPos.Y, Constants.TextureWidth, Constants.TextureHeight);
            _velocity = Vector2.Zero;
        }
    }
}
