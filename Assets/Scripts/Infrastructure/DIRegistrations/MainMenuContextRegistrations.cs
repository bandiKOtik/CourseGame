using Assets.Scripts.Infrastructure.DI_Container;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.DIRegistrations
{
    public class MainMenuContextRegistrations
    {
        public void Process(DIContainer container)
        {
            Debug.Log("MainMenuContextRegistrations: Processing...");
        }
    }
}
