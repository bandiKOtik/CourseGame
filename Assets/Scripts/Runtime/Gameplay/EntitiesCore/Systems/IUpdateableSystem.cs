namespace Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems
{
    public interface IUpdateableSystem : IEntitySystem
    {
        void OnUpdate(float deltaTime);
    }
}