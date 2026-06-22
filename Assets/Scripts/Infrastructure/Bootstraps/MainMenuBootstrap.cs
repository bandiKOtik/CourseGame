using Assets.Scripts.Infrastructure.ConfigsManagement.Bootstraps;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.DIRegistrations;
using Assets.Scripts.Utilities.SceneManagement;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Infrastructure.Bootstraps
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            //_container = container;

            //_contextRegistrations.Process(_container);
        }

        public override async UniTask Initialize()
        {
            await UniTask.CompletedTask;
        }

        public override void Run()
        { }
    }
}