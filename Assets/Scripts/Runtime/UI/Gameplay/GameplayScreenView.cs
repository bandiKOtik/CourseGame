using Assets.Scripts.Runtime.UI.CommonViews;
using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Runtime.UI.Gameplay.HealthDisplay;
using UnityEngine;

namespace Assets.Scripts.Runtime.UI.Gameplay
{
    public class GameplayScreenView : MonoBehaviour, IView
    {
        [field: SerializeField] public IconTextView StageNumberView { get; private set; }
        [field: SerializeField] public EntitiesHealthDisplay EntitiesHealthDisplay { get; private set; }
        [field: SerializeField] public BarWithText ExperienceBarView { get; private set; }
    }
}