using Assets.Scripts.Infrastructure.ConfigsManagement.Bootstraps;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Infrastructure.Bootstraps
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        public override async UniTask Initialize()
        {
            await UniTask.CompletedTask;
        }

        public override void Run()
        { }
    }
}