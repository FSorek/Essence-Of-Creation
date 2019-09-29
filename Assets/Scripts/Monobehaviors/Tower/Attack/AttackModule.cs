using System;
using UnityEngine;

public abstract class AttackModule : MonoBehaviour, ICanAttack
{
    public IEntity Entity { get => entity; }
    public TowerAttackData AttackData { get => attackData; set => attackData = value; }
    public IAbility[] ActiveAbilities { get => activeAbilities; set => activeAbilities = value; }
    public int AttackerID => TowerRecipeId.GetID(GetComponent<Obelisk>().InfusedElements);

    private IAbility[] activeAbilities;
    private IEntity entity;
    [SerializeField]private TowerAttackData attackData;
    private AttackController attackController;

    public abstract void Attack(ITakeDamage target);

    private void Awake()
    {
        entity = GetComponent<IEntity>();
        this.attackController = new AttackController(this, WaveManager.Instance);
    }

    protected void Update()
    {
        attackController.Tick();
    }
}
