using Assets.Scripts.Infrastructure.ConfigsManagement.Bootstraps;
using Assets.Scripts.Utilities.SceneManagement;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Infrastructure.Bootstraps
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        public override void ProcessRegistrations(IInputSceneArgs sceneArgs = null)
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