using Assets.Scripts.Runtime.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.UI.CommonViews
{
    public class Bar : MonoBehaviour, IView
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _filler;

        public void UpdateValue(float value) => _slider.value = value;

        public void SetFillerColor(Color color) => _filler.color = color;
    }
}