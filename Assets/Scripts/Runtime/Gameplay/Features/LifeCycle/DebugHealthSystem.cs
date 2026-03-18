using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.LifeCycle
{
    public class DebugHealthSystem : IInitializableSystem, IUpdateableSystem
    {
        private ReactiveVariable<float> _max;
        private ReactiveVariable<float> _current;

        public void OnInit(Entity entity)
        {
            _max = entity.MaxHealth;
            _current = entity.CurrentHealth;
        }

        public void OnUpdate(float deltaTime)
        {
            Debug.Log($"{_current.Value} / {_max.Value}");
        }
    }
}
