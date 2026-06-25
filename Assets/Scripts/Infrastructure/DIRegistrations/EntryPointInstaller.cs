using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.DIRegistrations
{
    public class EntryPointInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("Entry point installation...");

            Debug.Log("Entry point installation completed!");
        }
    }
}
