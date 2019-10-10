using UnityEngine;

public interface ICanAttack 
{
    int AttackerID { get; }
    IEntity Entity { get; }
    ITowerLastingAbility[] ActiveTowerAbilities { get; set; }
    TowerAttackData AttackData { get; set; }
    void Attack(ITakeDamage target);
}