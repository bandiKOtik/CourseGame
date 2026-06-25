using Assets.Scripts.Configs.Gameplay.Abilities;
using Assets.Scripts.Configs.Gameplay.Levels;
using Assets.Scripts.Configs.Gameplay.Loot;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
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
using Assets.Scripts.Utilities.AssetsManagement;
using Assets.Scripts.Utilities.Factory.UI;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.DIRegistrations
{
    //public class GameplayContextRegistrations
    //{
    //    private GameplayInputArgs _args;

    //    public void Process(DIContainer container, GameplayInputArgs args)
    //    {
    //        _args = args;

    //        container.RegisterAsSingle(c => new EntitiesFactory(c));

    //        container.RegisterAsSingle(c => new EntitiesLifeContext());

    //        container.RegisterAsSingle(c => new MainBaseFactory(c));

    //        container.RegisterAsSingle(CreateMainBaseHolderService).NonLazy();

    //        container.RegisterAsSingle(c => new EnemiesFactory(c));

    //        container.RegisterAsSingle(c => new BrainsFactory(c));

    //        container.RegisterAsSingle(c => new LootFactory(c));

    //        container.RegisterAsSingle(c => new LootPullingService(c.Resolve<EntitiesLifeContext>())).NonLazy();

    //        container.RegisterAsSingle(CreateDropLootService);

    //        container.RegisterAsSingle(c => new StagesFactory(c));

    //        container.RegisterAsSingle(c => new GameplayStatesFactory(c));

    //        container.RegisterAsSingle(CreateGameplayStatesContext);

    //        container.RegisterAsSingle(c => new PreperationInputService());

    //        container.RegisterAsSingle(CreateStageProviderService);

    //        container.RegisterAsSingle(c => new AIBrainsContext());

    //        container.RegisterAsSingle<IInputService>(c => new DesktopInput());

    //        container.RegisterAsSingle(CreateMonoEntitiesFactory).NonLazy();

    //        container.RegisterAsSingle(CreateGameplayUIRoot).NonLazy();

    //        container.RegisterAsSingle(CreateGameplayScreenPresenter).NonLazy();

    //        container.RegisterAsSingle(c => new CollidersRegistryService());

    //        container.RegisterAsSingle(c => new GameplayPresentersFactory(c, _args));

    //        container.RegisterAsSingle(CreateGameplayPopupService);

    //        container.RegisterAsSingle(c => new AbilitiesFactory(c));

    //        container.RegisterAsSingle(c => new AbilityDropingRulesService());

    //        container.RegisterAsSingle(CreateAbilityDropingService);

    //        container.RegisterAsSingle(CreateDropAbilityOnMainHeroLevelUpService).NonLazy();

    //        container.RegisterAsSingle<IPauseService>(c => new TimerScalePauseService());
    //    }

    //    private MainBaseHolderService CreateMainBaseHolderService(DIContainer c)
    //    {
    //        return new(c.Resolve<EntitiesLifeContext>());
    //    }

    //    private DropLootService CreateDropLootService(DIContainer c)
    //    {
    //        return new(
    //            c.Resolve<ConfigsProviderService>()
    //            .GetConfig<LootListConfig>(),
    //            c.Resolve<LootFactory>());
    //    }

    //    private GameplayStatesContext CreateGameplayStatesContext(DIContainer c)
    //    {
    //        return new(c
    //            .Resolve<GameplayStatesFactory>()
    //            .CreateGameplayStateMachine(_args));
    //    }

    //    private StageProviderService CreateStageProviderService(DIContainer c)
    //    {
    //        return new(
    //            c.Resolve<StagesFactory>(),
    //            c.Resolve<ConfigsProviderService>()
    //                .GetConfig<LevelsListConfig>()
    //                .GetLevelByNumber(_args.LevelNumber));
    //    }

    //    private MonoEntitiesFactory CreateMonoEntitiesFactory(DIContainer c)
    //    {
    //        return new(
    //            c.Resolve<CollidersRegistryService>(),
    //            c.Resolve<ResourcesAssetsLoader>(),
    //            c.Resolve<EntitiesLifeContext>());
    //    }

    //    private GameplayUIRoot CreateGameplayUIRoot(DIContainer c)
    //    {
    //        ResourcesAssetsLoader loader = c.Resolve<ResourcesAssetsLoader>();

    //        GameplayUIRoot rootPrefab = loader
    //            .Load<GameplayUIRoot>("UI/Gameplay/GameplayUIRoot");

    //        return Object.Instantiate(rootPrefab);
    //    }

    //    private GameplayScreenPresenter CreateGameplayScreenPresenter(DIContainer c)
    //    {
    //        GameplayUIRoot root = c.Resolve<GameplayUIRoot>();

    //        GameplayScreenView view = c
    //            .Resolve<ViewsFactory>()
    //            .Create<GameplayScreenView>(ViewIDs.GameplayScreen, root.HUDLayer);

    //        GameplayScreenPresenter presenter = c
    //            .Resolve<GameplayPresentersFactory>()
    //            .CreateGameplayScreenPresenter(view);

    //        return presenter;
    //    }

    //    private GameplayPopupService CreateGameplayPopupService(DIContainer c)
    //    {
    //        return new(
    //            c.Resolve<ViewsFactory>(),
    //            c.Resolve<ProjectPresentersFactory>(),
    //            c.Resolve<GameplayUIRoot>(),
    //            c.Resolve<GameplayPresentersFactory>());
    //    }

    //    private AbilityDropingService CreateAbilityDropingService(DIContainer c)
    //    {
    //        return new(
    //            c.Resolve<ConfigsProviderService>().GetConfig<AbilitiesConfigsContainer>(),
    //            c.Resolve<AbilityDropingRulesService>());
    //    }

    //    private DropAbilityOnMainHeroLevelUpService CreateDropAbilityOnMainHeroLevelUpService(DIContainer c)
    //    {
    //        return new(
    //            c.Resolve<MainBaseHolderService>(),
    //            c.Resolve<GameplayPopupService>(),
    //            c.Resolve<IPauseService>());
    //    }
    //}
}