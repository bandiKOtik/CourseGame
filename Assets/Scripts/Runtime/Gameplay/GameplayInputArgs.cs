using Assets.Scripts.Configs.Meta.GameModeConfigs;
using Assets.Scripts.Runtime.Gameplay;
using Assets.Scripts.Utilities.SceneManagement;

namespace Assets.Scripts.Infrastructure.Gameplay
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(GameMode currentGamemode, GameModeConfig config)
        {
            CurrentGamemode = currentGamemode;
            GenerationConfig = config.GetSequenceConfig(currentGamemode);
        }

        public GameMode CurrentGamemode { get; }
        public SequenceGenerationConfig GenerationConfig { get; }
    }
}