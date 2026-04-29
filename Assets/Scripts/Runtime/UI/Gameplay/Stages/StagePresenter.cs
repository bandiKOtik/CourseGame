using Assets.Scripts.Runtime.Gameplay.Features.StagesFeature;
using Assets.Scripts.Runtime.UI.CommonViews;
using Assets.Scripts.Runtime.UI.Core;
using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.UI.Gameplay.Stages
{
    public class StagePresenter : IPresenter
    {
        private readonly IconTextView _view;
        private readonly StageProviderService _stageProvider;

        private IDisposable _stateNumberChangeDisposable;

        public StagePresenter(IconTextView view, StageProviderService stageProvider)
        {
            _view = view;
            _stageProvider = stageProvider;
        }

        public void Initialize()
        {
            _stateNumberChangeDisposable = _stageProvider.CurrentStageNumber.Subscribe(OnNextStageIndexChanged);

            UpdateViewText();
        }

        public void Dispose() => _stateNumberChangeDisposable.Dispose();

        private void OnNextStageIndexChanged(int arg1, int arg2) => UpdateViewText();

        private void UpdateViewText()
        {
            _view.SetText($"{_stageProvider.CurrentStageNumber.Value} / {_stageProvider.StagesCount}");
        }
    }
}
