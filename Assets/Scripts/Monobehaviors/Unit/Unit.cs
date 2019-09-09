using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour, ITakeDamage
{
    public static event Action<Unit> OnUnitDeath;
    public event Action<Damage> OnTakeDamage = delegate {};

    public float CurrentHealth => currentHealth;
    public float MaxHealth => unitData.Health;
    public Vector3 Position => transform.position;

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

    public void TakeDamage(Damage damage)
    {
        var finalDamage = DamageManager.Instance.GetFinalDamageValues(damage, unitData.Type);
        currentHealth -= finalDamage;
        OnTakeDamage(damage);
        if (currentHealth <= 0)
        {
            OnDeath();
            return;
        }
    }

    public void AddEffect(string attackerName, Effect effect, bool stacksInDuration)
    {
        var currentInstance = activeEffects.FirstOrDefault(a => a.AttackerName == attackerName);
        if (stacksInDuration && currentInstance != null)
        {
            currentInstance.Extend();
        }
        else
        {
            activeEffects.Add(effect);
        }
    }
    internal void RemoveEffect(Effect effect)
    {
        activeEffects.Remove(effect);
    }

    public Stat MoveSpeed => unitData.MoveSpeed;
}
