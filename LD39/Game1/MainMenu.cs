using LD39.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD39.Game1
{
    class MainMenu
    {
        Button start, quit;
        Text title, subTitle, small;

        public MainMenu()
        {
            start = new Button("Start", new Vector2(360, 280), ButtonType.Start);
            quit = new Button("Quit", new Vector2(370, 350), ButtonType.Quit);
            title = new Text("NIGHT TERRORS", new Vector2(100, 80));
            subTitle = new Text("LUDUM DARE 39", new Vector2(150, 50));
            small = new Text("A game by Thomas De Maen", new Vector2(570, 570));
        }

        public void Update()
        {
            start.Update();
            quit.Update();
        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            start.Draw(pSpriteBatch, Ressources.MainFont);
            quit.Draw(pSpriteBatch, Ressources.MainFont);

            title.Draw(pSpriteBatch, Color.Wheat, Ressources.TitleFont);
            subTitle.Draw(pSpriteBatch, Color.Turquoise, Ressources.SubTitleFont);
            small.Draw(pSpriteBatch, Color.White, Ressources.LittleFont);
        }
    }
}
