using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay
{
    public class TestGameplayExample : MonoBehaviour
    {
        private DIContainer _container;
        private EntitiesFactory _factory;

        private Entity _entity;

        private bool _initialized = false;

        public IEnumerator Initialize(DIContainer c)
        {
            _container = c;
            _factory = _container.Resolve<EntitiesFactory>();

            yield break;
        }

        public void Run()
        {
            //_entity = _factory.CreateRigidbodyMoveableEntity(Vector3.zero);

            _entity = _factory.CreateCharacterControllerEntity(Vector3.zero);

            _initialized = true;
        }

        private void Update()
        {
            if (_initialized == false)
                return;

            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            _entity.MoveDirection.Value = input;
            _entity.RotationDirection.Value = input;
        }
    }
}
