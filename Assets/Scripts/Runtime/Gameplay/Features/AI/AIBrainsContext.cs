using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI
{
    public class AIBrainsContext : IDisposable
    {
        private readonly List<EntityToBrain> _entityToBrain = new();

        public void SetFor(Entity entity, IBrain brain)
        {
            foreach (var item in _entityToBrain)
            {
                if (item.Entity == entity)
                {
                    item.Brain.Disable();
                    item.Brain.Dispose();
                    item.Brain = brain;
                    item.Brain.Enable();
                    return;
                }
            }

            _entityToBrain.Add(new(entity, brain));
            brain.Enable();
        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < _entityToBrain.Count; i++)
            {
                if (_entityToBrain[i].Entity.Initialized == false)
                {
                    int lastIndex = _entityToBrain.Count - 1;

                    _entityToBrain[i].Brain.Dispose();
                    _entityToBrain[i] = _entityToBrain[lastIndex];
                    _entityToBrain.RemoveAt(lastIndex);
                    i--;
                    continue;
                }

                _entityToBrain[i].Brain.Update(deltaTime);
            }
        }

        public void Dispose()
        {
            foreach (var item in _entityToBrain)
                item.Brain.Dispose();

            _entityToBrain.Clear();
        }

        private class EntityToBrain
        {
            public Entity Entity;
            public IBrain Brain;

            public EntityToBrain(Entity entity, IBrain brain)
            {
                Entity = entity;
                Brain = brain;
            }
        }
    }
}