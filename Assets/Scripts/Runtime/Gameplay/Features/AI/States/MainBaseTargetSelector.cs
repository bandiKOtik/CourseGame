using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.MainBaseBuilding;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class MainBaseTargetSelector : ITargetSelector
    {
        private MainBaseHolderService _holder;

        public MainBaseTargetSelector(Entity source, MainBaseHolderService holder)
        {
            _holder = holder;
        }

        public Entity SelectTargetFrom(IEnumerable<Entity> targets)
            => targets.FirstOrDefault((entity) => entity == _holder.MainBase);
    }
}
