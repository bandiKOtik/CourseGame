using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.EntitiesCore.Mono
{
    public class MonoEntity : MonoBehaviour
    {
        public void Setup(Entity entity)
        {
            MonoEntityRegistrator[] registrators = GetComponentsInChildren<MonoEntityRegistrator>();

            if (registrators != null )
                foreach (var register in registrators)
                    register.Register(entity);
        }

        public void Cleanup(Entity entity)
        {

        }
    }
}
