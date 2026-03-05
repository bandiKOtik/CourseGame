namespace Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems
{
    public interface IInitializeableSystem : IEntitySystem
    {
        void OnInit(Entity entity);
    }
}
