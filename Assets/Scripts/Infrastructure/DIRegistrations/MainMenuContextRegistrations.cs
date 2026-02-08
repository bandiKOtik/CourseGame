using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.InputManagement;

namespace Assets.Scripts.Infrastructure.DIRegistrations
{
    public class MainMenuContextRegistrations
    {
        public void Process(DIContainer container)
        {
            container.RegisterAsSingle<IInputHandler>(CreateMainMenuInputHandler);
        }

        private MainMenuInputHandler CreateMainMenuInputHandler(DIContainer c)
            => new MainMenuInputHandler(c);
    }
}