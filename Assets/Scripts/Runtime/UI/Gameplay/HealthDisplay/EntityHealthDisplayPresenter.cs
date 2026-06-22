using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.MainBaseBuilding;
using Assets.Scripts.Runtime.UI.CommonViews;
using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Utilities.Factory.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.UI.Gameplay.HealthDisplay
{
    public class EntityHealthDisplayPresenter : IPresenter
    {
        private readonly EntitiesLifeContext _context;
        private readonly EntitiesHealthDisplay _view;

        private readonly GameplayPresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;

        private Dictionary<Entity, EntityHealthBarInfo> _entityToBarInfo = new();

        public EntityHealthDisplayPresenter(
            EntitiesLifeContext context,
            EntitiesHealthDisplay view,
            GameplayPresentersFactory presentersFactory,
            ViewsFactory viewsFactory)
        {
            _context = context;
            _view = view;
            _presentersFactory = presentersFactory;
            _viewsFactory = viewsFactory;
        }

        public void Initialize()
        {
            _context.Added += OnEntityAdded;
            _context.Released += OnEntityReleased;

            foreach (var entity in _context.Entities)
                OnEntityAdded(entity);
        }

        public void LateUpdate()
        {
            foreach (var info in _entityToBarInfo)
                _view.UpdatePositionFor(info.Value.HealthPresenter.Bar, info.Value.BarPoint.position);
        }

        public void Dispose()
        {
            _context.Added -= OnEntityAdded;
            _context.Released -= OnEntityReleased;

            foreach (EntityHealthBarInfo info in _entityToBarInfo.Values)
                DisposeFor(info);

            _entityToBarInfo.Clear();
        }

        private void OnEntityAdded(Entity entity)
        {
            if (entity.TryGetHealthBarPoint(out var barPoint))
            {
                BarWithText barView = null;

                if (entity.HasComponent<IsMainBase>())
                    barView = _viewsFactory.Create<BarWithText>(ViewIDs.HealthBarWithText);
                else
                    barView = _viewsFactory.Create<BarWithText>(ViewIDs.SimpleHealthBar);

                _view.Add(barView);

                EntityHealthPresenter healthPresenter = _presentersFactory.CreateEntityHealthPresenter(entity, barView);
                healthPresenter.Initialize();

                IDisposable removeReason = entity.IsDead.Subscribe((oldValue, isDead) =>
                {
                    if (isDead)
                        RemoveHealthBarFor(entity);
                });

                _entityToBarInfo.Add(entity, new(barPoint, removeReason, healthPresenter));
            }
        }

        private void OnEntityReleased(Entity entity)
        {
            if (_entityToBarInfo.ContainsKey(entity))
                RemoveHealthBarFor(entity);
        }

        private void RemoveHealthBarFor(Entity entity)
        {
            EntityHealthBarInfo info = _entityToBarInfo[entity];
            DisposeFor(info);
            _entityToBarInfo.Remove(entity);
        }

        private void DisposeFor(EntityHealthBarInfo info)
        {
            info.RemoveReason.Dispose();

            _view.Remove(info.HealthPresenter.Bar);
            _viewsFactory.Release(info.HealthPresenter.Bar);

            info.HealthPresenter.Dispose();
        }

        private class EntityHealthBarInfo
        {
            public EntityHealthBarInfo(
                Transform barPoint,
                IDisposable removeReason,
                EntityHealthPresenter healthPresenter)
            {
                BarPoint = barPoint;
                RemoveReason = removeReason;
                HealthPresenter = healthPresenter;
            }

            public Transform BarPoint { get; }
            public IDisposable RemoveReason { get; }
            public EntityHealthPresenter HealthPresenter { get; }
        }
    }
}