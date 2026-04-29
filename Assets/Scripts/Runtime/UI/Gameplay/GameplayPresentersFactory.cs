using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.StagesFeature;
using Assets.Scripts.Runtime.UI.CommonViews;
using Assets.Scripts.Runtime.UI.Gameplay.EndgamePopups;
using Assets.Scripts.Runtime.UI.Gameplay.HealthDisplay;
using Assets.Scripts.Runtime.UI.Gameplay.Stages;
using Assets.Scripts.Utilities.CoroutinesManagement;
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
                _container.Resolve<ICoroutinesPerformer>(),
                view,
                _container.Resolve<SceneSwitcherService>());
        }

        public DefeatPopupPresenter CreateDefeatPopupPresenter(DefeatPopupView view)
        {
            return new(
                _container.Resolve<ICoroutinesPerformer>(),
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
    }
}