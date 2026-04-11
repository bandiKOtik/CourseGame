using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class Layers
    {
        public static readonly int Characters = LayerMask.NameToLayer("Character");
        public static readonly LayerMask CharacterMask = 1 << Characters;


        public static readonly int Environment = LayerMask.NameToLayer("Environment");
        public static readonly LayerMask EnvironmentMask = 1 << Environment;
    }
}
