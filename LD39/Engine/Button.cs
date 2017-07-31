using LD39.Game1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD39.Engine
{
    public enum ButtonStatus
    {
        Sleep,
        Overed,
        Pressed
    }

    public enum ButtonType
    {
        Start,
        Restart,
        Quit
    }

    class Button : Text
    {
        private Color _textColor;
        private MouseState _mouseState;
        private Rectangle _clickableArea;
        private ButtonStatus _btnState;
        private ButtonType _btnType;
        private int _width, _height;

        public Button(string pText, Vector2 pPos, ButtonType pBtnType) : base(pText, pPos)
        {
            _textColor = Color.White;

            _width = _text.Length * Constants.LetterSize;
            _height = Constants.LetterSize;

            _clickableArea = new Rectangle((int)pPos.X, (int)pPos.Y, _width, _height*2);

            _btnState = ButtonStatus.Sleep;
            _btnType = pBtnType;
        }

        public void Draw(SpriteBatch pSpriteBatch, SpriteFont pFont)
        {
            pSpriteBatch.DrawString(pFont, _text, _position, _textColor);
        }

        public void Update()
        {
            HandleMouse();

            switch (_btnState)
            {
                case ButtonStatus.Pressed:
                    _textColor = Color.Aqua;
                    break;
                case ButtonStatus.Overed:
                    _textColor = Color.Yellow;
                    break;
                case ButtonStatus.Sleep:
                    _textColor = Color.White;
                    break;
            }
        }

        private void HandleMouse()
        {
            _mouseState = Mouse.GetState();

            if (IsPressed())
            {
                _btnState = ButtonStatus.Pressed;
                HandleButton(_btnType);
            }
            else if (IsOvered())
            {
                _btnState = ButtonStatus.Overed;
            }
            else
            {
                _btnState = ButtonStatus.Sleep;
            }
        }

        private void HandleButton(ButtonType pBtnType)
        {
            switch (pBtnType)
            {
                case ButtonType.Start:
                    LD39.MustLaunch();
                    break;
                case ButtonType.Restart:
                    LD39.MustLaunch();
                    Level.MustReload();
                    break;
                case ButtonType.Quit:
                    LD39.MustQuit();
                    break;
            }
        }

        public bool IsOvered()
        {
            return _clickableArea.Contains(_mouseState.X, _mouseState.Y);
        }

        public bool IsPressed()
        {
            return IsOvered() && _mouseState.LeftButton == ButtonState.Pressed;
        }
    }
}
