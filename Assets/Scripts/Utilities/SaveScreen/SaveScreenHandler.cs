using UnityEngine;

namespace Assets.Scripts.Utilities.SaveScreen
{
    public class SaveScreenHandler : MonoBehaviour, ISaveScreen
    {
        public bool IsShown => gameObject.activeSelf;

        private void Awake()
        {
            Hide();
            DontDestroyOnLoad(this);
        }

        public void Hide() => gameObject.SetActive(false);

        public void Show() => gameObject.SetActive(true);
    }
}
