using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.SceneManagement;
using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay
{
    public class GameSession : IDisposable
    {
        public event Action Win;

        public event Action Defeat;

        private ICoroutinesPerformer _performer;
        private SceneSwitcherService _sceneSwitcher;
        private PlayerDataProvider _dataProvider;

        public GameSession(
            ICoroutinesPerformer performer,
            SceneSwitcherService sceneSwitcher,
            PlayerDataProvider dataProvider)
        {
            _performer = performer;
            _sceneSwitcher = sceneSwitcher;
            _dataProvider = dataProvider;
        }

        public void Dispose()
        {
        }

        private void SessionWin()
        {
            Debug.Log("And it's <color=green>CORRECT</color>!");
            Win?.Invoke();
            ReturnToMenu();
        }

        private void SessionDefeat()
        {
            Debug.Log("And it's <color=red>WRONG</color>.");
            Defeat?.Invoke();
            ReturnToMenu();
        }

        private void ReturnToMenu()
        {
            _performer.StartPerform(_dataProvider.SaveAsync());

            _performer
                .StartPerform(_sceneSwitcher
                .ProcessSwitchTo(Scenes.MainMenu));
        }
    }
}