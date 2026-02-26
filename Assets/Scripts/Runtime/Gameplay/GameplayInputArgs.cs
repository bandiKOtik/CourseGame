using Assets.Scripts.Configs.Meta.GameModeConfigs;
using Assets.Scripts.Runtime.Gameplay;
using Assets.Scripts.Utilities.SceneManagement;

namespace Assets.Scripts.Infrastructure.Gameplay
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(GameMode currentGamemode)
        {
            CurrentGamemode = currentGamemode;
        }

        public GameMode CurrentGamemode { get; }
    }
}