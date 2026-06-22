using Assets.Scripts.Configs.Gameplay;
using Assets.Scripts.Configs.Gameplay.Abilities;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.AbilitiesDropingFeature;
using Assets.Scripts.Runtime.Gameplay.Features.AbilitiesFeature;
using Assets.Scripts.Runtime.Gameplay.Features.MainBaseBuilding;
using Assets.Scripts.Runtime.Gameplay.Features.StagesFeature;
using Assets.Scripts.Runtime.UI.CommonViews;
using Assets.Scripts.Runtime.UI.Gameplay.AbilitySelectPopup;
using Assets.Scripts.Runtime.UI.Gameplay.EndgamePopups;
using Assets.Scripts.Runtime.UI.Gameplay.Experience;
using Assets.Scripts.Runtime.UI.Gameplay.HealthDisplay;
using Assets.Scripts.Runtime.UI.Gameplay.Stages;
using Assets.Scripts.Utilities.Factory.UI;
using Assets.Scripts.Utilities.SceneManagement;

namespace Assets.Scripts.Runtime.UI.Gameplay
{
    public class GameplayPresentersFactory
    {
        private readonly DIContainer _container;
        private readonly GameplayInputArgs _args;

        public GameplayPresentersFactory(DIContainer container, GameplayInputArgs args)
        {
            _container = container;
            _args = args;
        }

        public GameplayScreenPresenter CreateGameplayScreenPresenter(GameplayScreenView view)
        {
            return new(view, _container.Resolve<GameplayPresentersFactory>());
        }

        public WinPopupPresenter CreateWinPopupPresenter(WinPopupView view)
        {
            return new(
                view,
                _container.Resolve<SceneSwitcherService>());
        }

        public DefeatPopupPresenter CreateDefeatPopupPresenter(DefeatPopupView view)
        {
            return new(
                view,
                _container.Resolve<SceneSwitcherService>(),
                _args);
        }

        public StagePresenter CreateStagePresenter(IconTextView view)
        {
            return new(view, _container.Resolve<StageProviderService>());
        }

        public EntityHealthPresenter CreateEntityHealthPresenter(Entity entity, BarWithText bar)
        {
            return new(entity, bar);
        }

        public EntityHealthDisplayPresenter CreateEntityHealthDisplayPresenter(EntitiesHealthDisplay view)
        {
            return new(
                _container.Resolve<EntitiesLifeContext>(),
                view,
                this,
                _container.Resolve<ViewsFactory>());
        }

        public SelectableAbilityPresenter CreateSelectableAbilityPresenter(
            AbilityConfig config,
            SelectableAbilityView view,
            Entity entity)
        {
            return new(
                config,
                view,
                _container.Resolve<AbilitiesFactory>(),
                entity);
        }

        public AbilitySelectPopupPresenter CreateAbilitySelectPopupPresenter(AbilitySelectPopupView view, Entity entity, int level)
        {
            return new(
                view,
                entity,
                _container.Resolve<AbilityDropingService>(),
                this,
                _container.Resolve<ViewsFactory>(),
                level);
        }

        public MainBaseExperiencePresenter CreateMainBaseExperiencePresenter(BarWithText view)
        {
            return new(
                view,
                _container.Resolve<MainBaseHolderService>(),
                _container.Resolve<ConfigsProviderService>()
                .GetConfig<ExperienceForUpgradeLevelConfig>());
        }
    }
}