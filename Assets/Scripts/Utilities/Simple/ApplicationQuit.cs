using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Utilities.Simple
{
    [RequireComponent(typeof(Button))]
    public class ApplicationQuit : MonoBehaviour
    {
        public void ExitGame() => Application.Quit();
    }
}