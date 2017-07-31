using LD39.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD39.Game1
{
    class Score
    {
        Button restart, quit;
        Text score;

        private static int finalScore;

        public Score(GraphicsDevice pGraphicsDevice)
        {
            restart = new Button("Start", new Vector2(360, 280), ButtonType.Restart);
            quit = new Button("Quit", new Vector2(370, 350), ButtonType.Quit);
            score = new Text("Final Score: 0", new Vector2(100, 80));
        }

        public void Update()
        {
            restart.Update();
            quit.Update();
            score.UpdateText("Final Score: " + finalScore);
        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            restart.Draw(pSpriteBatch, Ressources.MainFont);
            quit.Draw(pSpriteBatch, Ressources.MainFont);

            score.Draw(pSpriteBatch, Color.Wheat, Ressources.TitleFont);
        }

        public static void SetFinalScore(float pFinalScore)
        {
            if (pFinalScore >= 0)
                finalScore = (int)pFinalScore;
            else
                finalScore = 0;
        }
    }
}
