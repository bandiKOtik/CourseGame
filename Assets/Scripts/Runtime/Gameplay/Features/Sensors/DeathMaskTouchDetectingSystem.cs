using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Reactive;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.Sensors
{
    public class DeathMaskTouchDetectingSystem : IInitializableSystem, IUpdateableSystem
    {
        private Buffer<Collider> _contacts;
        private ReactiveVariable<bool> _isTouched;
        private LayerMask _deathMask;

        public void OnInit(Entity entity)
        {
            _contacts = entity.ContactsColliderBuffer;
            _isTouched = entity.IsTouchedDeathMask;
            _deathMask = entity.DeathMask;
        }

        public void OnUpdate(float deltaTime)
        {
            for (int i = 0; i < _contacts.Count; i++)
            {
                if (MatchWithDeathLayer(_contacts.Items[i]))
                {
                    _isTouched.Value = true;
                    return;
                }

                _isTouched.Value = false;
            }
        }

        private bool MatchWithDeathLayer(Collider collider)
        {
            return ((1 << collider.gameObject.layer) & _deathMask) != 0;
        }
    }
}