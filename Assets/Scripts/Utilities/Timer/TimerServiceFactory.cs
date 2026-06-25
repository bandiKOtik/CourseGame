namespace Assets.Scripts.Utilities.Timer
{
    public class TimerServiceFactory
    {
        public TimerService Create(float cooldown) => new(cooldown);
    }
}