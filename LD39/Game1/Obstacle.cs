using LD39.Engine;
using Microsoft.Xna.Framework;

namespace LD39.Game1
{
    abstract class Obstacle : GameObject
    {
        public void BlockPlayer(Player pPlayer)
        {
            if (pPlayer.GetVelocity().X > 0) // Collision from left
            {
                pPlayer.SetPosition(new Vector2(_hitBox.Left - Constants.TextureWidth, pPlayer.GetPosition().Y));
               // pPlayer.touchedRight = true;
            }
            else if (pPlayer.GetVelocity().X < 0) // Collision from right
            {
                pPlayer.SetPosition(new Vector2(_hitBox.Right, pPlayer.GetPosition().Y));
               // pPlayer.touchedLeft = true;
            }
            else if (pPlayer.GetVelocity().Y > 0) // Collision from top
            {
                pPlayer.SetPosition(new Vector2(pPlayer.GetPosition().X, _hitBox.Top - Constants.TextureHeight));
               // pPlayer.touchedBottom = true;
            }
            else if (pPlayer.GetVelocity().Y < 0) // Collision from bottom
            {
                pPlayer.SetPosition(new Vector2(pPlayer.GetPosition().X, _hitBox.Bottom));
                //pPlayer.touchedTop = true;
            }
        }
    }
}
