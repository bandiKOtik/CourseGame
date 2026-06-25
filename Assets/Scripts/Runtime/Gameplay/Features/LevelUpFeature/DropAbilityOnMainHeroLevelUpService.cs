using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.MainBaseBuilding;
using Assets.Scripts.Runtime.Gameplay.Features.PauseFeature;
using Assets.Scripts.Runtime.UI.Gameplay;
using Assets.Scripts.Runtime.UI.Gameplay.AbilitySelectPopup;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Gameplay.Features.LevelUpFeature
{
    public class DropAbilityOnMainHeroLevelUpService : IInitializable, IDisposable
    {
        private MainBaseHolderService _heroHolder;
        private GameplayPopupService _popupService;
        private IPauseService _pauseService;

        private Queue<int> _levelUpRequests = new();

        private AbilitySelectPopupPresenter _popup;
        private UniTask _selectAbilityProcess;

        private IDisposable _heroRegistredDisposable;
        private IDisposable _heroLevelChangedDisposable;

        public DropAbilityOnMainHeroLevelUpService(
            MainBaseHolderService heroHolder,
            GameplayPopupService popupService,
            IPauseService pauseService)
        {
            _heroHolder = heroHolder;
            _popupService = popupService;
            _pauseService = pauseService;
        }

        public bool IsPopupOpened => _popup != null;

        public void Initialize()
        {
            _heroRegistredDisposable = _heroHolder.BaseRegistred.Subscribe(OnRegistred);
        }

        public void Dispose()
        {
            _heroRegistredDisposable.Dispose();
            _heroLevelChangedDisposable.Dispose();
        }

        private void OnRegistred(Entity hero)
        {
            _heroLevelChangedDisposable = hero.Level.Subscribe(OnLevelChanged);
        }

        private void OnLevelChanged(int arg1, int currentLevel)
        {
            _levelUpRequests.Enqueue(currentLevel);

            if (_selectAbilityProcess.Status != UniTaskStatus.Succeeded)
                return;

            _selectAbilityProcess = SelectAbilityProcess();
        }

        private async UniTask SelectAbilityProcess()
        {
            while (_levelUpRequests.Count > 0)
            {
                int level = _levelUpRequests.Dequeue();

                _pauseService.Pause();
                _popup = _popupService.OpenAbilitySelectPopup(_heroHolder.MainBase, level, () =>
                {
                    _pauseService.Unpause();
                    _popup = null;
                });

                await new WaitUntil(() => IsPopupOpened == false);
            }
        }
    }
}