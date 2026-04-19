namespace Assets.Scripts.Runtime.Gameplay.EntitiesCore
{
    public class EntitiesHelper
    {
        public static bool TryTakeDamageFrom(Entity source, Entity damageable, float amount)
        {
            if (damageable.TryGetTakeDamageRequest(out var damageRequest) == false)
                return false;

            if (source.TryGetTeam(out var sourceTeam) && damageable.TryGetTeam(out var damageableTeam))
            {
                if (sourceTeam.Value == damageableTeam.Value)
                    return false;
            }

            damageRequest.Invoke(amount);
            return true;
        }
    }
}
