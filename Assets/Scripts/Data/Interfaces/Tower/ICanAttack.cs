using UnityEngine;

public interface ICanAttack 
{
    int AttackerID { get; }
    IEntity Entity { get; }
    IAbility[] ActiveAbilities { get; set; }
    TowerAttackData AttackData { get; set; }
    void Attack(ITakeDamage target);
}