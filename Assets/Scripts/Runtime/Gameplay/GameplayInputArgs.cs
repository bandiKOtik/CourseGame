using Assets.Scripts.Infrastructure.Configs;
using Assets.Scripts.Utilities.SceneManagement;

namespace Assets.Scripts.Infrastructure.Gameplay
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(GameModeConfig config)
        {
            GameConfig = config;
        }

        public GameModeConfig GameConfig { get; }
    }
}