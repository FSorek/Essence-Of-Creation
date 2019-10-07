using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : GameEntity, IUnit
{
    protected UnitData unitData { get; set; }
    protected List<Effect> activeEffects { get; private set; }

    private UnitController unitController { get; set; }
    private StateMachine stateMachine { get; set; }

    private void OnEnable()
    {
        activeEffects = new List<Effect>();
        unitData = WaveManager.Instance.CurrentGeneratedUnit;
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

    public void TakeDamage(int attackerID, Damage damage, IAbility[] abilities = null)
    {
        unitController.TakeDamage(attackerID, damage, abilities);
        OnTakeDamage(damage);
    }

    public override void Destroy()
    {
        OnUnitDeath(this);
        GetComponent<IGameObjectPooled>().Pool.ReturnToPool(this.gameObject);
    }

    public void RemoveEffect(Effect effect) => unitController.RemoveEffect(effect);

    public Stat MaxHealth => unitData.Health;
    public Stat CurrentHealth => unitController.CurrentHealth;
    public Stat MovementSpeed => unitData.MoveSpeed;
    public Stat HealthRegeneration => unitData.HealthRegen;
    public Stat ArmorLayers => unitData.ArmorLayers;
    public Stat CrystallineLayers => unitData.CrystallineLayers;
    public ArmorType ArmorType => unitData.Type;

    public static event Action<ITakeDamage> OnUnitSpawn = delegate { };
    public static event Action<ITakeDamage> OnUnitDeath = delegate { };

    public event Action<Damage> OnTakeDamage = delegate { };

}
