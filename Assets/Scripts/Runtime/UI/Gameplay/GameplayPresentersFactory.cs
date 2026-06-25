using Assets.Scripts.Configs.Gameplay;
using Assets.Scripts.Configs.Gameplay.Abilities;
using Assets.Scripts.Infrastructure.ConfigsManagement;
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
        private readonly GameplayInputArgs _args;
        private readonly MainBaseHolderService _heroHolder;
        private readonly EntitiesLifeContext _lifeContext;
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly StageProviderService _stageProvider;
        private readonly ConfigsProviderService _configsProvider;
        private readonly ViewsFactory _viewsFactory;
        private readonly AbilitiesFactory _abilitiesFactory;
        private readonly AbilityDropingService _abilityDropingService;

        public GameplayPresentersFactory(
            GameplayInputArgs args,
            EntitiesLifeContext context,
            MainBaseHolderService heroHolder,
            SceneSwitcherService sceneSwitcherService,
            StageProviderService stageProvider,
            ConfigsProviderService configsProvider,
            ViewsFactory viewsFactory,
            AbilitiesFactory abilitiesFactory,
            AbilityDropingService abilityDropingService)
        {
            _args = args;
            _heroHolder = heroHolder;
            _lifeContext = context;
            _sceneSwitcher = sceneSwitcherService;
            _stageProvider = stageProvider;
            _configsProvider = configsProvider;
            _viewsFactory = viewsFactory;
            _abilitiesFactory = abilitiesFactory;
            _abilityDropingService = abilityDropingService;
        }

        public GameplayScreenPresenter CreateGameplayScreenPresenter(GameplayScreenView view)
        {
            return new(view, this);
        }

        public WinPopupPresenter CreateWinPopupPresenter(WinPopupView view)
        {
            return new(
                view,
                _sceneSwitcher);
        }

        public DefeatPopupPresenter CreateDefeatPopupPresenter(DefeatPopupView view)
        {
            return new(
                view,
                _sceneSwitcher,
                _args);
        }

        public StagePresenter CreateStagePresenter(IconTextView view)
        {
            return new(view, _stageProvider);
        }

        public EntityHealthPresenter CreateEntityHealthPresenter(Entity entity, BarWithText bar)
        {
            return new(entity, bar);
        }

        public EntityHealthDisplayPresenter CreateEntityHealthDisplayPresenter(EntitiesHealthDisplay view)
        {
            return new(
                _lifeContext,
                view,
                this,
                _viewsFactory);
        }

        public SelectableAbilityPresenter CreateSelectableAbilityPresenter(
            AbilityConfig config,
            SelectableAbilityView view,
            Entity entity)
        {
            return new(
                config,
                view,
                _abilitiesFactory,
                entity);
        }

        public AbilitySelectPopupPresenter CreateAbilitySelectPopupPresenter(AbilitySelectPopupView view, Entity entity, int level)
        {
            return new(
                view,
                entity,
                _abilityDropingService,
                this,
                _viewsFactory,
                level);
        }

        public MainBaseExperiencePresenter CreateMainBaseExperiencePresenter(BarWithText view)
        {
            return new(
                view,
                _heroHolder,
                _configsProvider.GetConfig<ExperienceForUpgradeLevelConfig>());
        }
    }
}