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

        private Entity _ghostEntityTest;

        private bool _initialized = false;

        public IEnumerator Initialize(DIContainer c)
        {
            _container = c;
            _factory = _container.Resolve<EntitiesFactory>();

            yield break;
        }

        public void Run()
        {
            _ghostEntityTest = _factory.CreateHero(Vector3.zero);

            _factory.CreateGhost(Vector3.forward * 5);

            _factory.CreateWizzard(Vector3.back * 5);

            _initialized = true;
        }

        private void Update()
        {
            if (_initialized == false)
                return;

            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            _ghostEntityTest.MoveDirection.Value = input;
            _ghostEntityTest.RotationDirection.Value = input;

            if (Input.GetKeyDown(KeyCode.R))
                _ghostEntityTest.TakeDamageRequest.Invoke(1f);

            if (Input.GetKeyDown(KeyCode.Space))
                _ghostEntityTest.StartAttackRequest.Invoke();
        }
    }
}