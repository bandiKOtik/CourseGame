using Assets.Scripts.Runtime.UI.Core;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Runtime.UI.CommonViews
{
    public class BarWithText : MonoBehaviour, IView
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Bar _bar;

        public void UpdateText(string text) => _text.text = text;

        public void UpdateValue(float value) => _bar.UpdateValue(value);

        public void SetFillerColor(Color color) => _bar.SetFillerColor(color);
    }
}