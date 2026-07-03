using Assets.Scripts.Configs.Gameplay.Abilities;
using Assets.Scripts.Configs.Gameplay.Levels;
using Assets.Scripts.Configs.Gameplay.Loot;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Mono;
using Assets.Scripts.Runtime.Gameplay.Features.AbilitiesDropingFeature;
using Assets.Scripts.Runtime.Gameplay.Features.AbilitiesFeature;
using Assets.Scripts.Runtime.Gameplay.Features.AI;
using Assets.Scripts.Runtime.Gameplay.Features.Enemies;
using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Runtime.Gameplay.Features.LevelUpFeature;
using Assets.Scripts.Runtime.Gameplay.Features.LootFeature;
using Assets.Scripts.Runtime.Gameplay.Features.MainBaseBuilding;
using Assets.Scripts.Runtime.Gameplay.Features.PauseFeature;
using Assets.Scripts.Runtime.Gameplay.Features.StagesFeature;
using Assets.Scripts.Runtime.Gameplay.States;
using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Runtime.UI.Gameplay;
using Assets.Scripts.Utilities.Factory.UI;
using Assets.Scripts.Utilities.SceneManagement;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.DIRegistrations
{
    public class GameplayInstaller : MonoInstaller
    {
        private GameplayInputArgs _args;

        public override void InstallBindings()
        {
            Debug.Log("Gameplay installation...");

            // Factory
            Container.Bind<EntitiesFactory>().AsSingle();

            Container.BindInterfacesAndSelfTo<EntitiesLifeContext>().AsSingle();

            Container.Bind<MainBaseFactory>().AsSingle();

            Container.Bind<EnemiesFactory>().AsSingle();

            Container.Bind<BrainsFactory>().AsSingle();

            Container.Bind<LootFactory>().AsSingle();

            Container
                .BindInterfacesAndSelfTo<MainBaseHolderService>()
                .AsSingle()
                .NonLazy();

            Container.Bind<LootPullingService>().AsSingle().NonLazy();

            Container
                .Bind<DropLootService>()
                .FromMethod(RigisterDropLootService)
                .AsSingle();

            Container.Bind<StagesFactory>().AsSingle();

            Container.Bind<GameplayStatesFactory>().AsSingle();

            Container
                .Bind<GameplayStatesContext>()
                .FromMethod(RegisterGameplayStatesContext)
                .AsSingle();

            Container.Bind<PreperationInputService>().AsSingle();

            Container
                .Bind<StageProviderService>()
                .FromMethod(RegisterStageProviderService)
                .AsSingle();

            Container.Bind<AIBrainsContext>().AsSingle();

            Container.Bind<IInputService>().To<DesktopInput>().AsSingle();

            Container.Bind<MonoEntitiesFactory>().AsSingle().NonLazy();

            Container
                .Bind<GameplayUIRoot>()
                .FromResource("UI/Gameplay/GameplayUIRoot")
                .AsSingle()
                .NonLazy();

            Container
                .Bind<GameplayScreenPresenter>()
                .FromMethod(RegisterGameplayScreenPresenter)
                .AsSingle()
                .NonLazy();

            Container.Bind<CollidersRegistryService>().AsSingle();

            Container.Bind<GameplayPresentersFactory>().AsSingle();

            Container.Bind<GameplayPopupService>().AsSingle(); //InterfacesAndSelfTo?

            Container.Bind<AbilitiesFactory>().AsSingle();

            Container.Bind<AbilityDropingRulesService>().AsSingle();

            Container
                .Bind<AbilityDropingService>()
                .FromMethod(RegisterAbilityDropingService)
                .AsSingle();

            Container.Bind<DropAbilityOnMainHeroLevelUpService>().AsSingle().NonLazy();

            Container
                .Bind<IPauseService>()
                .To<TimerScalePauseService>()
                .AsSingle();

            Debug.Log("Gameplay installation complete!");
        }

        private DropLootService RigisterDropLootService()
        {
            return new(
                Container.Resolve<ConfigsProviderService>()
                .GetConfig<LootListConfig>(),
                Container.Resolve<LootFactory>());
        }

        private GameplayStatesContext RegisterGameplayStatesContext()
        {
            return new(Container
                .Resolve<GameplayStatesFactory>()
                .CreateGameplayStateMachine(_args));
        }

        private StageProviderService RegisterStageProviderService()
        {
            return new(
                Container.Resolve<StagesFactory>(),
                Container.Resolve<ConfigsProviderService>()
                    .GetConfig<LevelsListConfig>()
                    .GetLevelByNumber(_args.LevelNumber));
        }

        private GameplayScreenPresenter RegisterGameplayScreenPresenter()
        {
            GameplayUIRoot root = Container.Resolve<GameplayUIRoot>();

            GameplayScreenView view = Container
                .Resolve<ViewsFactory>()
                .Create<GameplayScreenView>(ViewIDs.GameplayScreen, root.HUDLayer);

            GameplayScreenPresenter presenter = Container
                .Resolve<GameplayPresentersFactory>()
                .CreateGameplayScreenPresenter(view);

            return presenter;
        }

        private AbilityDropingService RegisterAbilityDropingService()
        {
            return new(
                Container.Resolve<ConfigsProviderService>()
                .GetConfig<AbilitiesConfigsContainer>(),
                Container.Resolve<AbilityDropingRulesService>());
        }
    }
}
