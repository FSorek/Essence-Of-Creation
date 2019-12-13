using System;
using Data.Data_Types;
using DataBehaviors.Player.States;
using Monobehaviors.Tower;
using UnityEngine;

namespace Monobehaviors.Player
{
    [RequireComponent(typeof(PlayerExtractAssembleTower))]
    public class PlayerMergeAttack : MonoBehaviour
    {
        private PlayerExtractAssembleTower extractionSystem;

        private void Awake()
        {
            //MergeAttackPlayerState.OnMiddleMouseAttack += Attack;
            //extractionSystem = GetComponent<PlayerExtractAssembleTower>();
        }

        private void Attack() // check if we have towers in extracted list
        {
            if (extractionSystem.ExtractedEssences.Count <= 0)
                return;

            var extractedTower = extractionSystem.ExtractedEssences.Peek().GetComponent<Obelisk>();

            // create a new type 'PlayerAttack' based on the obelisk stats

            throw new NotImplementedException();
        }
    }

    public class PlayerAttack
    {
        private Damage damage;
        private float interval;
        private float range;
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
}