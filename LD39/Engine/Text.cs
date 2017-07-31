using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD39.Engine
{
    class Text
    {
        protected Vector2 _position;
        protected string _text;

        public Text(string pText, Vector2 pPos)
        {
            _text = pText;
            _position = pPos;
        }

        public virtual void Draw(SpriteBatch pSpriteBatch, Color pColor, SpriteFont pFont)
        {
            pSpriteBatch.DrawString(pFont, _text, _position, pColor);
        }

        public virtual void UpdateText(string pNewText)
        {
            _text = pNewText;
        }

        public void SetPosition(Vector2 pPos)
        {
            _position = pPos;
        }
    }
}
