using System.Collections.Generic;

namespace Assets.Scripts.Runtime.Gameplay.Features.StatFeature
{
    public interface IStatsEffect
    {
        void ApplyTo(Dictionary<StatTypes, float> stats);
    }
}