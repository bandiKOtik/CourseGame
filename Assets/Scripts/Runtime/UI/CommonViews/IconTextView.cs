using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.UI.CommonViews
{
    public class IconTextView : MonoBehaviour, IView
    {
        [SerializeField] private Text _text;
        [SerializeField] private Image _icon;

        public void SetText(string text) => _text.text = text;
        public void SetIcon(Sprite icon) => _icon.sprite = icon;
    }
}
