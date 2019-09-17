using UnityEngine;

public interface ICanAttack 
{
    int AttackerID { get; }
    IEntity Entity { get; }
    Ability[] ActiveAbilities { get; set; }
    TowerAttackData AttackData { get; set; }
    void Attack(ITakeDamage target);
}