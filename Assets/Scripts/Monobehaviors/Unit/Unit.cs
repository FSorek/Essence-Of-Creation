using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : GameEntity, IUnit
{
    protected UnitData unitData { get; set; }

    private UnitStatusController unitStatusController { get; set; }
    private UnitDamageController unitDamageController { get; set; }
    private StateMachine stateMachine { get; set; }

    private void OnEnable()
    {
        unitData = WaveManager.Instance.CurrentGeneratedUnit;
        stateMachine = new StateMachine();
        var dict = new Dictionary<Type, UnitState>()
        {
            {typeof(UnitRunState), new UnitRunState(this, WaveManager.Instance) },
        };
        stateMachine.SetStates(dict);

        unitDamageController = new UnitDamageController(this);
        unitStatusController = new UnitStatusController(this);
        OnUnitSpawn(this);
    }

    private void Update()
    {
        stateMachine.Tick();
        unitStatusController.Tick();
        unitDamageController.RegenerateHealth();
    }

    public void TakeDamage(int attackerID, Damage damage)
    {
        unitDamageController.TakeDamage(attackerID, damage);
        OnTakeDamage(damage);
    }

    private void OnDisable()
    {
        OnUnitDeath(this);
    }

    public override void Destroy()
    {
        GetComponent<IGameObjectPooled>().Pool.ReturnToPool(this.gameObject);
    }

    public void AddStatus(IStatus status) => unitStatusController.AddStatus(status);
    public Stat MaxHealth => unitData.Health;
    public Stat CurrentHealth => unitDamageController.CurrentHealth;
    public Stat MovementSpeed => unitData.MoveSpeed;
    public Stat HealthRegeneration => unitData.HealthRegen;
    public Stat ArmorLayers => unitData.ArmorLayers;
    public Stat CrystallineLayers => unitData.CrystallineLayers;
    public ArmorType ArmorType => unitData.Type;
    public IStatus[] CurrentStatuses => unitStatusController.CurrentStatuses;

    public static event Action<ITakeDamage> OnUnitSpawn = delegate { };
    public static event Action<ITakeDamage> OnUnitDeath = delegate { };

    public event Action<Damage> OnTakeDamage = delegate { };

}
