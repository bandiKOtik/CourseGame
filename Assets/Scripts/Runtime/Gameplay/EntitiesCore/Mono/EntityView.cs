using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.EntitiesCore.Mono
{
    public abstract class EntityView : MonoBehaviour
    {
        public void Link(Entity entity)
        {
            entity.Initialized += OnEntityInitialize;
        }

        public virtual void CleanUp(Entity entity)
        {
            entity.Initialized -= OnEntityInitialize;
        }

        protected abstract void OnEntityInitialize(Entity entity);
    }
}