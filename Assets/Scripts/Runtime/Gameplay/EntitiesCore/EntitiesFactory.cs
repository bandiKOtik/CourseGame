using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Mono;
using Assets.Scripts.Runtime.Gameplay.Features.MovementFeature;
using Assets.Scripts.Runtime.Gameplay.Features.RotationFeature;
using Assets.Scripts.Utilities.Reactive;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.EntitiesCore
{
    public class EntitiesFactory
    {
        private readonly DIContainer _container;
        private readonly EntitiesLifeContext _context;

        private readonly MonoEntitiesFactory _monoEntitiesFactory;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
            _context = _container.Resolve<EntitiesLifeContext>();
            _monoEntitiesFactory = _container.Resolve<MonoEntitiesFactory>();
        }

        public Entity CreateRigidbodyMoveableEntity(Vector3 position)
        {
            var entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/RigidbodyMoveableEntity");

            entity
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(10))
                .AddRotationDirection()
                .AddRotationSpeed(new ReactiveVariable<float>(500));

            entity
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotationSystem());

            _context.Add(entity);

            return entity;
        }

        public Entity CreateCharacterControllerEntity(Vector3 position)
        {
            var entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/CharacterControllerEntity");

            entity
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(10))
                .AddRotationDirection()
                .AddRotationSpeed(new ReactiveVariable<float>(500));

            entity
                .AddSystem(new CharacterControllerMovementSystem())
                .AddSystem(new TransformRotationSystem());

            _context.Add(entity);

            return entity;
        }

        private Entity CreateEmpty() => new Entity();
    }
}
