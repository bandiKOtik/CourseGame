using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.AI;
using Assets.Scripts.Runtime.Gameplay.Features.AI.States;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay
{
    public class TestGameplayExample : MonoBehaviour
    {
        private DIContainer _container;
        private EntitiesFactory _factory;
        private BrainsFactory _brainsFactory;

        private Entity _playerEntity;

        private bool _initialized = false;

        public IEnumerator Initialize(DIContainer c)
        {
            _container = c;
            _factory = _container.Resolve<EntitiesFactory>();
            _brainsFactory = _container.Resolve<BrainsFactory>();

            yield break;
        }

        public void Run()
        {
            // Player
            _playerEntity = _factory.CreateHero(Vector3.zero).AddCurrentTarget();
            ITargetSelector nearestSelector = new NearestDamageableTargetSelector(_playerEntity);
            _brainsFactory.CreateManualShooterBrain(_playerEntity);

            // Ghost
            var ghostEntity = _factory.CreateGhost(Vector3.forward * 5);
            _brainsFactory.CreateRandomWalkBrain(ghostEntity);

            // Wizzard
            var wizzardEntity = _factory.CreateWizzard(Vector3.back * 5, 100, 20, 5).AddCurrentTarget();
            ITargetSelector weakestSelector = new WeakestDamageableTargetSelector(wizzardEntity);

            _brainsFactory.CreateRandomTeleportationBrain(wizzardEntity, 1f);
            // OR
            //_brainsFactory.CreateTeleportToWeakestBrain(wizzardEntity, weakestSelector, 1f);

            _initialized = true;
        }

        private void Update()
        {
            if (_initialized == false)
                return;
        }
    }
}