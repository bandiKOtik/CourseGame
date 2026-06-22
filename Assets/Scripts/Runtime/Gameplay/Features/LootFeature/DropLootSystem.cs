using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.Reactive;

namespace Assets.Scripts.Runtime.Gameplay.Features.LootFeature
{
    public class DropLootSystem : IInitializableSystem, IUpdateableSystem
    {
        private DropLootService _dropLootService;

        private Entity _entity;
        private ReactiveVariable<bool> _isDropped;
        private ICompositeCondition _canDrop;

        public DropLootSystem(DropLootService dropLootService)
        {
            _dropLootService = dropLootService;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _isDropped = entity.LootIsDropped;
            _canDrop = entity.CanDropLoot;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_canDrop.Evaluate())
            {
                DropLoot();
                _isDropped.Value = true;
            }
        }

        private void DropLoot() => _dropLootService.DropLootFor(_entity);
    }
}