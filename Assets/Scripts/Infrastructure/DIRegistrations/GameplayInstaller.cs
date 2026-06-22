using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.DIRegistrations
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("Gameplay installation...");

            Debug.Log("Gameplay installation complete!");
        }
    }
}
