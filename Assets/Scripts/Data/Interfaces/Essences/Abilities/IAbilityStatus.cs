namespace Monobehaviors.Essences.Attacks
{
    public interface IAbilityStatus
    {
        bool Expired { get; }
        void EffectApplied(EffectController target);
        void Tick(EffectController target);
    }
}