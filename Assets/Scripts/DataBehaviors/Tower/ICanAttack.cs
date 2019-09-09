using UnityEngine;

public interface ICanAttack
{
    Ability[] ActiveAbilities { get; set; }
    IObelisk Obelisk { get; }
    TowerAttackData AttackData { get; set; }
    void Attack(ITakeDamage target);
}