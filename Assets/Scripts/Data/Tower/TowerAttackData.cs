using UnityEngine;

[CreateAssetMenu(fileName = "Obelisk Attack Data", menuName = "Essence/Obelisk/Attack Data", order = 0)]
public class TowerAttackData : ScriptableObject
{
    public string Name;
    public Damage Damage;
    public float Range = 20;
    public float AttackTimer = 1f;
    public GameObject projectileModel;
    public bool CanFollowTarget = true;
    public float ProjectileSpeed;
    public int TargetLimit = 1;

}

