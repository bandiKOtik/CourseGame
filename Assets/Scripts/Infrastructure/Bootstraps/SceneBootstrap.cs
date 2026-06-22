using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Utilities.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.ConfigsManagement.Bootstraps
{
    public abstract class SceneBootstrap : MonoBehaviour
    {
        public abstract void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null);

        public abstract UniTask Initialize();

        public abstract void Run();
    }
}