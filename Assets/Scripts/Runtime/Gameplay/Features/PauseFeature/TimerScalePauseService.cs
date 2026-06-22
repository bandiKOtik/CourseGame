using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.PauseFeature
{
    public class TimerScalePauseService : IPauseService
    {
        public bool IsPaused { get; private set; }

        public void Pause()
        {
            Time.timeScale = 0;
            IsPaused = true;
        }

        public void Unpause()
        {
            Time.timeScale = 1;
            IsPaused = false;
        }
    }
}