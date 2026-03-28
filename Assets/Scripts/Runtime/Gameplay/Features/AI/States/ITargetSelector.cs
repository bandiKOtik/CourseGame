using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public interface ITargetSelector
    {
        Entity SelectTargetFrom(IEnumerable<Entity> targets);
    }
}