using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.Gameplay;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.DIRegistrations
{
    public class GameplayContextRegistrations
    {
        public void Process(DIContainer container, GameplayInputArgs args)
        {
            Debug.Log("GameplayContextRegistrations: Processing...");
        }
    }
}
