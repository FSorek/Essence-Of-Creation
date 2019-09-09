using System;
using UnityEngine;

[RequireComponent(typeof(PlayerExtractAssembleTower))]
public class PlayerMergeAttack : MonoBehaviour
{
    private PlayerExtractAssembleTower extractionSystem;
    private void Awake()
    {
        MergeAttackPlayerState.OnMiddleMouseAttack += Attack;
        extractionSystem = GetComponent<PlayerExtractAssembleTower>();
    }

    private void Attack() // check if we have towers in extracted list
    {
        if(extractionSystem.ExtractedTowers.Count <= 0)
            return;

        var extractedTower = extractionSystem.ExtractedTowers.Peek().GetComponent<Obelisk>();

        // create a new type 'PlayerAttack' based on the obelisk stats

        throw new NotImplementedException();
    }
}

public class PlayerAttack
{
    private Damage damage;
    private float range;
    private float interval;

}

/*
 *
 *  public string Name;
    public Damage Damage;
    public float Range = 20;
    public float AttackTimer = 1f;
    public GameObject projectileModel;
    public AttackType AttackType;
    public bool CanFollowTarget = true;
    public float ProjectileSpeed;
    public int TargetLimit = 1;
 */

