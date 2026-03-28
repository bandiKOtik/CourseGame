using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Utilities.CoroutinesManagement;

namespace Assets.Scripts.Utilities.Timer
{
    public class TimerServiceFactory
    {
        private readonly DIContainer _container;

        public TimerServiceFactory(DIContainer container)
        {
            _container = container;
        }

        public TimerService Create(float cooldown)
            => new(cooldown, _container.Resolve<ICoroutinesPerformer>());
    }
}