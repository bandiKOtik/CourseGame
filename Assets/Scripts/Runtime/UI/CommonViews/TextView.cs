using Assets.Scripts.Runtime.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.UI.CommonViews
{
    public class TextView : MonoBehaviour, IView
    {
        [SerializeField] private Text _text;

        public void SetText(string text) => _text.text = text;
    }
}