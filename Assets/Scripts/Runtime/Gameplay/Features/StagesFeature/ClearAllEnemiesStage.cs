using Assets.Scripts.Configs.Gameplay.Levels.Stages;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.Enemies;
using Assets.Scripts.Utilities.Reactive;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.Gameplay.Features.StagesFeature
{
    public class ClearAllEnemiesStage : IStage
    {
        private EnemiesFactory _enemiesFactory;
        private ClearAllEnemiesStageConfig _config;
        private EntitiesLifeContext _context;
        private ReactiveEvent _completed = new();
        private bool _inProcess;

        private Dictionary<Entity, IDisposable> _spawnedEnemiesToRemoveReason = new();

        public ClearAllEnemiesStage(ClearAllEnemiesStageConfig config,  EnemiesFactory enemiesFactory, EntitiesLifeContext context)
        {
            _config = config;
            _enemiesFactory = enemiesFactory;
            _context = context;
        }

        public IReadOnlyEvent Completed => _completed;

        public void Start()
        {
            if (_inProcess == true)
                throw new System.InvalidOperationException("Gamemode already started");

            SpawnEnemies();

            _inProcess = true;
        }

        public void Update(float deltaTime)
        {
            if (_inProcess == false)
                return;

            if (_spawnedEnemiesToRemoveReason.Count == 0)
                ProcessEnd();
        }

        public void CleanUp()
        {
            foreach (var item in _spawnedEnemiesToRemoveReason)
            {
                item.Value.Dispose();
                _context.Release(item.Key);
            }

            _spawnedEnemiesToRemoveReason.Clear();
            _inProcess = false;
        }

        public void Dispose()
        {
            foreach (var item in _spawnedEnemiesToRemoveReason)
            {
                item.Value.Dispose();
            }

            _spawnedEnemiesToRemoveReason.Clear();
            _inProcess = false;
        }

        private void SpawnEnemies()
        {
            foreach (var itemConfig in _config.EnemyItems)
                SpawnEnemy(itemConfig);
        }

        private void SpawnEnemy(EnemyItemConfig itemConfig)
        {
            var spawnedEnemy = _enemiesFactory.Create(itemConfig.SpawnPosition, itemConfig.EnemyConfig);

            IDisposable removeReason = spawnedEnemy.IsDead.Subscribe((old, isDead) =>
            {
                if (isDead)
                {
                    IDisposable disposable = _spawnedEnemiesToRemoveReason[spawnedEnemy];
                    disposable.Dispose();
                    _spawnedEnemiesToRemoveReason.Remove(spawnedEnemy);
                }
            });

            _spawnedEnemiesToRemoveReason.Add(spawnedEnemy, removeReason);
        }

        private void ProcessEnd()
        {
            _inProcess = false;
            _completed.Invoke();
        }
    }
}
