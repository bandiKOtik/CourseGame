using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities.Reactive;
using System;

namespace Assets.Scripts.Runtime.Gameplay.Features.MainHero
{
    public class MainHeroHolderService : IInitializable, IDisposable
    {
        private EntitiesLifeContext _context;

        private ReactiveEvent<Entity> _heroRegistred = new();
        private Entity _mainHero;

        public MainHeroHolderService(EntitiesLifeContext context)
        {
            _context = context;
        }

        public IReadOnlyEvent<Entity> HeroRegistred => _heroRegistred;
        public Entity MainHero => _mainHero;

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
            if (entity.HasComponent<IsMainHero>())
            {
                _context.Added -= OnEntityAdded;
                _mainHero = entity;
                _heroRegistred?.Invoke(_mainHero);
            }
        }
    }
}
