using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : GameEntity, ITakeDamage
{
    public static event Action<Unit> OnUnitDeath;
    public event Action<Damage> OnTakeDamage = delegate {};

    public float CurrentHealth => currentHealth;
    public float MaxHealth => unitData.Health;

    public int TargetReachpoint;
    [SerializeField]
    private UnitData unitData;
    private int currentHealth;

    private StateMachine stateMachine => GetComponent<StateMachine>();
    private List<Effect> activeEffects;
    private List<UnitAbility> unitAbilities;

    private void Awake()
    {
        unitData = WaveManager.Instance.GetCurrentGeneratedUnit();
        currentHealth = unitData.Health;
        TargetReachpoint = -1;
        var dict = new Dictionary<Type, BaseState>()
        {
            {typeof(GetNextWaypoint), new GetNextWaypoint(this) },
            {typeof(RunningState), new RunningState(this) }
        };
        stateMachine.SetStates(dict);
        activeEffects = new List<Effect>();
    }

    private void Update()
    {
        for (int i = 0; i < activeEffects.Count; i++)
        {
            activeEffects[i].Tick(this);
        }
    }

    public void OnDeath()
    {
        OnUnitDeath(this);
        Destroy(this.gameObject);
    }

    public void TakeDamage(int attackerID, Damage damage, Ability[] abilities = null)
    {
        var finalDamage = damage.GetDamageToArmor(unitData.Type);
        currentHealth -= finalDamage;
        OnTakeDamage(damage);
        if (currentHealth <= 0)
        {
            OnDeath();
            return;
        }

        if (abilities != null)
        {
            for(int i = 0; i < abilities.Length; i++)
                AddEffect(attackerID, new Effect(attackerID, abilities[i]));
        }
    }

    public void AddEffect(int attackerID, Effect effect)
    {
        var currentInstance = activeEffects.FirstOrDefault(a => a.AttackerId == attackerID);
        if (effect.StackInDuration && currentInstance != null)
        {
            currentInstance.Extend();
        }
        else
        {
            activeEffects.Add(effect);
        }
    }

    public void RemoveEffect(Effect effect)
    {
        activeEffects.Remove(effect);
    }

    public Stat MoveSpeed => unitData.MoveSpeed;
}
