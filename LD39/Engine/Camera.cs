using Microsoft.Xna.Framework;

namespace LD39.Engine
{
    class Camera
    {
        public Vector2 Position { get; set; }
        public Vector2 InitialPosition { get; set; }
        public float Zoom { get; set; }
        public float Rotation { get; private set; }

        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }
        public Vector2 ViewportCenter
        {
            get
            {
                return new Vector2(ViewportWidth * 0.5f, ViewportHeight * 0.5f);
            }
        }

        public Matrix TranslationMatrix
        {
            get
            {
                return Matrix.CreateTranslation(-(int)Position.X, -(int)Position.Y, 0) *
                    Matrix.CreateRotationZ(Rotation) *
                    Matrix.CreateScale(Zoom, Zoom, 1) *
                    Matrix.CreateTranslation(new Vector3(ViewportCenter, 0));
            }
        }

        public Camera()
        {
            InitialPosition = new Vector2(Constants.GameWidth / 2, Constants.GameHeight / 2);
            Position = InitialPosition;
            Zoom = 1.0f;
        }
    }
}
