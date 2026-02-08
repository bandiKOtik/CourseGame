using Assets.Scripts.Infrastructure.Configs;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.Gameplay;
using Assets.Scripts.Utilities.AssetsManagement;

namespace Assets.Scripts.Utilities.Factory
{
    public class GameModeConfigFactory
    {
        public GameModeConfig Create(DIContainer c, GameMode gameMode)
        {
            switch (gameMode)
            {
                case GameMode.Numbers:
                    return c
                        .Resolve<ResourcesAssetsLoader>()
                        .Load<GameModeConfig>("Configs/GameMode/Numbers");

                case GameMode.Letters:
                    return c
                        .Resolve<ResourcesAssetsLoader>()
                        .Load<GameModeConfig>("Configs/GameMode/Letters");

                default:
                    throw new System.NotImplementedException
                        ("There is no realization for this type of gamemode: " + gameMode);
            }
        }
    }
}