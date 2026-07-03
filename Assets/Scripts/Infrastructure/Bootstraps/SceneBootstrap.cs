using Assets.Scripts.Utilities.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.ConfigsManagement.Bootstraps
{
    public abstract class SceneBootstrap : MonoBehaviour
    {
        public abstract UniTask Initialize();

        public abstract void Run();
    }
}