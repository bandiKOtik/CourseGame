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
using Zenject;

namespace Assets.Scripts.Infrastructure.ConfigsManagement.Bootstraps
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private GameplayInputArgs _args;

        private MainBaseHolderService _mainBaseHolderService;
        private MainBaseFactory _mainBaseFactory;

        private GameplayStatesContext _gameplayStatesContext;
        private EntitiesLifeContext _lifeContext;
        private AIBrainsContext _brainsContext;

        private GameplayScreenPresenter _screenPresenter;

        private bool _initialized = false;

        [Inject]
        private void Construct(
            GameplayInputArgs args,
            MainBaseHolderService mainBaseHolderService,
            MainBaseFactory mainBaseFactory,
            GameplayStatesContext gameplayStatesContext,
            EntitiesLifeContext lifeContext,
            AIBrainsContext brainsContext,
            GameplayScreenPresenter screenPresenter)
        {
            _args = args;
            _mainBaseHolderService = mainBaseHolderService;
            _mainBaseFactory = mainBaseFactory;
            _gameplayStatesContext = gameplayStatesContext;
            _lifeContext = lifeContext;
            _brainsContext = brainsContext;
            _screenPresenter = screenPresenter;
        }

        public override async UniTask Initialize()
        {
            Debug.Log("Factory: " + _mainBaseFactory == null);
            Debug.Log("Args: " + _args == null);
            _mainBaseFactory.Create(_args, Vector3.zero);

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

            if (Input.GetKeyDown(KeyCode.D))
                _mainBaseHolderService.MainBase.Experience.Value += 1000;
        }

        private void LateUpdate()
        {
            _screenPresenter?.LateUpdate();
        }
    }
}