using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities.Reactive;
using System;

namespace Assets.Scripts.Runtime.Gameplay.Features.MainBaseBuilding
{
    public class MainBaseHolder : IInitializable, IDisposable
    {
        private EntitiesLifeContext _context;

        private ReactiveEvent<Entity> _baseRegistred = new();
        private Entity _mainBase;

        public MainBaseHolder(EntitiesLifeContext context)
        {
            _context = context;
        }

        public IReadOnlyEvent<Entity> BaseRegistred => _baseRegistred;
        public Entity MainBase => _mainBase;

        public void Initialize()
        {
            _context.Added += OnEntityAdded;
        }

        public void Dispose()
        {
            _context.Added -= OnEntityAdded;
        }

        private void OnEntityAdded(Entity entity)
        {
            if (entity.HasComponent<IsMainBase>())
            {
                _context.Added -= OnEntityAdded;
                _mainBase = entity;
                _baseRegistred?.Invoke(_mainBase);
            }
        }
    }
}
