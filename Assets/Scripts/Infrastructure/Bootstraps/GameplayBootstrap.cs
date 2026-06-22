using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.DIRegistrations;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.AI;
using Assets.Scripts.Runtime.Gameplay.Features.MainBaseBuilding;
using Assets.Scripts.Runtime.Gameplay.States;
using Assets.Scripts.Runtime.UI.Gameplay;
using Assets.Scripts.Utilities.SceneManagement;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.ConfigsManagement.Bootstraps
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _args;
        private GameplayContextRegistrations _contextRegistrations = new();

        private GameplayStatesContext _gameplayStatesContext;
        private EntitiesLifeContext _lifeContext;
        private AIBrainsContext _brainsContext;

        private GameplayScreenPresenter _screenPresenter;

        private MainBaseHolderService _mainBaseHolderService;

        private bool _initialized = false;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs args)
                throw new ArgumentException(
                    nameof(sceneArgs) + " is not match with " + typeof(GameplayInputArgs));
            else
                _args = args;

            _contextRegistrations.Process(_container, args);

            _mainBaseHolderService = _container.Resolve<MainBaseHolderService>();
        }

        public override async UniTask Initialize()
        {
            _lifeContext = _container.Resolve<EntitiesLifeContext>();
            _brainsContext = _container.Resolve<AIBrainsContext>();

            _gameplayStatesContext = _container.Resolve<GameplayStatesContext>();

            _container.Resolve<MainBaseFactory>().Create(_args, Vector3.zero);

            _screenPresenter = _container.Resolve<GameplayScreenPresenter>();

            await UniTask.CompletedTask;
        }

        public override void Run()
        {
            _initialized = true;

            _gameplayStatesContext.Run();
        }

        private void Update()
        {
            if (_initialized == false)
                return;

            _brainsContext?.Update(Time.deltaTime);
            _lifeContext?.Update(Time.deltaTime);
            _gameplayStatesContext?.Update(Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.L))
                _mainBaseHolderService.MainBase.Experience.Value += 1000;
        }

        private void LateUpdate()
        {
            _screenPresenter?.LateUpdate();
        }
    }
}