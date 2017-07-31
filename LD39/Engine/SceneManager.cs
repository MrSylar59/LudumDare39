
namespace LD39.Engine
{
    public enum GameState 
    {
        MainMenu,
        InGame,
        Score
    }

    class SceneManager
    {
        public static GameState GameState;

        public SceneManager(GameState pGameState)
        {
            GameState = pGameState;
        }

        public GameState GetGameState()
        {
            return GameState;
        }

        public void SetGameState(GameState pGameState)
        {
            GameState = pGameState;
        }
    }
}
