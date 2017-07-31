namespace LD39
{
    public class Constants
    {
        // Game Window
        public static readonly int GameWidth = 800;
        public static readonly int GameHeight = 608;
        public static readonly bool GameWindowed = false;
        public static readonly bool MouseVisible = true;

        // Game Debug State
        public static readonly bool DebugMode = false;

        // Image resolution
        public static readonly int TextureWidth = 32;
        public static readonly int TextureHeight = 32;

        public static readonly int PlayerWidth = 18;
        public static readonly int PlayerHeight = 27;

        public static readonly int LetterSize = 24;

        // Gameplay elements
        public static readonly float Heal = 20f;
        public static readonly float Hurt = -20f;

        public static readonly float BatteryBonus = 50;
        public static readonly float BatteryMalus = -25;

        public static readonly float ImmobileDuration = 3f;

        public static readonly bool LightActive = true;
    }
}
