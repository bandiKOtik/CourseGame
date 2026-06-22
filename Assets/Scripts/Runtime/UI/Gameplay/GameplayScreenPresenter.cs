using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Runtime.UI.Gameplay.HealthDisplay;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.UI.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _screen;
        private readonly GameplayPresentersFactory _factory;
        private readonly List<IPresenter> _childPresenters = new();
        private EntityHealthDisplayPresenter _healthPresenter;

        public GameplayScreenPresenter(GameplayScreenView screen, GameplayPresentersFactory factory)
        {
            _screen = screen;
            _factory = factory;
        }

        public void Initialize()
        {
            CreateStageNumber();
            CreateEntityHealthDisplayPresenter();
            CreateMainHeroExperienceView();

            foreach (var presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            foreach (var presenter in _childPresenters)
                presenter.Dispose();

            _childPresenters.Clear();
        }

        public void LateUpdate() => _healthPresenter.LateUpdate();

        private void CreateStageNumber()
        {
            var presenter = _factory.CreateStagePresenter(_screen.StageNumberView);

            _childPresenters.Add(presenter);
        }

        private void CreateEntityHealthDisplayPresenter()
        {
            _healthPresenter = _factory.CreateEntityHealthDisplayPresenter(_screen.EntitiesHealthDisplay);

            _childPresenters.Add(_healthPresenter);
        }

        private void CreateMainHeroExperienceView()
        {
            var presenter = _factory.CreateMainBaseExperiencePresenter(_screen.ExperienceBarView);

            _childPresenters.Add(presenter);
        }
    }
}