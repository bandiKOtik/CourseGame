using Assets.Scripts.Configs.Meta.GameModeConfigs;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.InputManagement;
using Assets.Scripts.Utilities.AssetsManagement;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.SceneManagement;

namespace Assets.Scripts.Infrastructure.DIRegistrations
{
    public class MainMenuContextRegistrations
    {
        private readonly string _gameModeConfigPath = "Configs/GameModeConfig";

        public void Process(DIContainer container)
        {
            container.RegisterAsSingle<IInputHandler>(CreateGameModeSelector);
        }

        private GameModeSelector CreateGameModeSelector(DIContainer c)
            => new GameModeSelector(
                c.Resolve<ICoroutinesPerformer>(),
                c.Resolve<SceneSwitcherService>(),
                c.Resolve<PlayerDataProvider>(),
                c.Resolve<ResourcesAssetsLoader>()
                .Load<GameModeConfig>(_gameModeConfigPath));
    }
}