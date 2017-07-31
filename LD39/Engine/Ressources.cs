using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LD39.Engine
{
    public class Ressources
    {
        public static Dictionary<string, Texture2D> Images;
        public static Dictionary<string, SoundEffect> Sounds;
        public static SpriteFont MainFont;
        public static SpriteFont TitleFont;
        public static SpriteFont SubTitleFont;
        public static SpriteFont LittleFont;

        public static void LoadImages(ContentManager Content)
        {
            Images = new Dictionary<string, Texture2D>();

            List<string> graphics = new List<string>()
            {
                "PlayerIdle",
                "PlayerWalkDown",
                "PlayerWalkUp",
                "PlayerWalkRight",
                "PlayerWalkLeft",
                "PlayerHurt",

                "Powerup",
                "Hitbox",
                "Powerdown",

                "Wall",
                "Wall2",
                "Floor",
                "Floor2",
                "Floor3",

                "Level",
                "Lightmask",
                "Evil",

                "MainMenu",
                "GameOver"
            };

            foreach (string img in graphics)
            {
                Images.Add(img, Content.Load<Texture2D>("Images/" + img));
            }
        }

        public static void LoadSounds(ContentManager Content)
        {
            Sounds = new Dictionary<string, SoundEffect>();

            List<string> sounds = new List<string>()
            {
                "PlayerHurt",
                "PowerUp",
                "Whispers",
                "MainMusic"
            };

            foreach (string sound in sounds)
            {
                Sounds.Add(sound, Content.Load<SoundEffect>("Sounds/" + sound));
            }
        }

        public static void LoadFonts(ContentManager Content)
        {
            MainFont = Content.Load<SpriteFont>("Fonts/Pixeled");
            TitleFont = Content.Load<SpriteFont>("Fonts/PixeledTitle");
            SubTitleFont = Content.Load<SpriteFont>("Fonts/PixeledSubTitle");
            LittleFont = Content.Load<SpriteFont>("Fonts/PixeledLittle");
        }
    }
}
