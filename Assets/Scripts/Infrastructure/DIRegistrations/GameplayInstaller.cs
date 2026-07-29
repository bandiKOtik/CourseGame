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
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.DIRegistrations
{
    public class GameplayInstaller : MonoInstaller
    {
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

            // Services
            Container
                .BindInterfacesAndSelfTo<MainBaseHolderService>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<LootPullingService>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<DropLootService>()
                .FromMethod(RigisterDropLootService)
                .AsSingle();

            Container.Bind<StagesFactory>().AsSingle();

            Container.Bind<GameplayStatesFactory>().AsSingle();

            Container
                .BindInterfacesAndSelfTo<GameplayStatesContext>()
                .FromMethod(RegisterGameplayStatesContext)
                .AsSingle();

            Container.Bind<PreperationInputService>().AsSingle();

            Container
                .Bind<StageProgress>()
                .FromMethod(RegisterStageProgress)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<StageProviderService>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<AIBrainsContext>().AsSingle();

            Container.Bind<IInputService>().To<DesktopInput>().AsSingle();

            Container.BindInterfacesAndSelfTo<MonoEntitiesFactory>().AsSingle().NonLazy();

            Container
                .Bind<GameplayUIRoot>()
                .FromComponentInNewPrefabResource("UI/Gameplay/GameplayUIRoot")
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<GameplayScreenPresenter>()
                .FromMethod(RegisterGameplayScreenPresenter)
                .AsSingle()
                .NonLazy();

            Container.Bind<CollidersRegistryService>().AsSingle();

            Container.Bind<GameplayPresentersFactory>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameplayPopupService>().AsSingle();

            Container.Bind<AbilitiesFactory>().AsSingle();

            Container.Bind<AbilityDropingRulesService>().AsSingle();

            Container
                .Bind<AbilityDropingService>()
                .FromMethod(RegisterAbilityDropingService)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<DropAbilityOnMainHeroLevelUpService>()
                .AsSingle()
                .NonLazy();

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

        private GameplayStatesContext RegisterGameplayStatesContext(InjectContext arg)
        {
            var args = Container.Resolve<GameplayInputArgs>();

            var stateMachine = Container
                .Resolve<GameplayStatesFactory>()
                .CreateGameplayStateMachine(args);

            return new(stateMachine);
        }

        private StageProgress RegisterStageProgress()
        {
            var args = Container.Resolve<GameplayInputArgs>();

            var config = Container
                .Resolve<ConfigsProviderService>()
                .GetConfig<LevelsListConfig>()
                .GetLevelByNumber(args.LevelNumber);

            return new(config);
        }

        private GameplayScreenPresenter RegisterGameplayScreenPresenter(InjectContext arg)
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
