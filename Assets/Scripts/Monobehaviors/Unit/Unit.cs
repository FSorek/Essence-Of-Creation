using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : GameEntity, ITakeDamage, ICanMove
{
    protected UnitData unitData { get; set; }
    protected List<Effect> activeEffects { get; private set; }

    private UnitController unitController { get; set; }
    private StateMachine stateMachine { get; set; }

    private void Awake()
    {
        activeEffects = new List<Effect>();
        unitData = WaveManager.Instance.GetCurrentGeneratedUnit();
        stateMachine = new StateMachine();
        var dict = new Dictionary<Type, UnitState>()
        {
            {typeof(UnitRunState), new UnitRunState(this, WaveManager.Instance) },
        };
        stateMachine.SetStates(dict);

        unitController = new UnitController(this);
        OnUnitSpawn(this);
    }

    private void Update()
    {
        for (int i = 0; i < activeEffects.Count; i++)
        {
            activeEffects[i].Tick(this);
        }
        stateMachine.Tick();
        unitController.RegenerateHealth();
    }

    public void TakeDamage(int attackerID, Damage damage, Ability[] abilities = null)
    {
        unitController.TakeDamage(attackerID,damage.GetDamageToArmor(unitData.Type),abilities);
        OnTakeDamage(damage);
    }

    public override void Destroy()
    {
        OnUnitDeath(this);
        Destroy(this.gameObject);
    }

    public void RemoveEffect(Effect effect) => unitController.RemoveEffect(effect);

    public float MaxHealth => unitData.Health;
    public float CurrentHealth => unitController.CurrentHealth;
    public float MovementSpeed => unitData.MoveSpeed;
    public float HealthRegeneration => unitData.HealthRegen;

    public static event Action<ITakeDamage> OnUnitSpawn = delegate { };
    public static event Action<ITakeDamage> OnUnitDeath = delegate { };

    public event Action<Damage> OnTakeDamage = delegate { };
}