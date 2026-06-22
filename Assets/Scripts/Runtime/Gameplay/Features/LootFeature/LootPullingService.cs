using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities.Reactive;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.LootFeature
{
    public class LootPullingService : IInitializable, IDisposable
    {
        private ReactiveVariable<bool> _allCollected = new();

        private List<Entity> _loot = new();

        private EntitiesLifeContext _context;

        public LootPullingService(EntitiesLifeContext context)
        {
            _context = context;
        }

        public ReactiveVariable<bool> AllCollected => _allCollected;

        private bool _isActivated;

        public void Initialize()
        {
            _context.Added += OnEntityAdded;
            _context.Released += OnEntityReleased;
        }

        public void Dispose()
        {
            _context.Added -= OnEntityAdded;
            _context.Released -= OnEntityReleased;
        }

        public void PullTo(Entity entity)
        {
            if (_isActivated)
                throw new InvalidOperationException();

            _isActivated = true;

            if (_loot.Count == 0)
            {
                _allCollected.Value = true;
                return;
            }

            foreach (var loot in _loot)
            {
                loot.CurrentTarget.Value = entity;
                loot.InPullingProcess.Value = true;
            }
        }

        public void Reset()
        {
            _isActivated = false;
            _allCollected.Value = false;
        }

        private void OnEntityAdded(Entity entity)
        {
            if (entity.HasComponent<IsPullable>() == false)
                return;

            _loot.Add(entity);

            Transform lootTransform = entity.Transform;

            Vector2 randomOffset = UnityEngine.Random.insideUnitCircle;
            Vector3 offset = new(randomOffset.x, 0, randomOffset.y);
            Vector3 endJumpPosition = lootTransform.position + offset;

            lootTransform
                .DOJump(endJumpPosition, 2, 1, .7f)
                .SetEase(Ease.OutBounce)
                .OnComplete(() => entity.InSpawnProcess.Value = false)
                .Play();
        }

        private void OnEntityReleased(Entity entity)
        {
            bool removed = _loot.Remove(entity);

            if (removed && _loot.Count == 0)
                _allCollected.Value = true;
        }
    }
}