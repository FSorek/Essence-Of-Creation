using System;
using UnityEngine;

public abstract class AttackModule : MonoBehaviour, ICanAttack
{
    public TowerAttackData AttackData { get => attackData; set => attackData = value; }
    public IObelisk Obelisk { get => obelisk; }
    public Ability[] ActiveAbilities { get => activeAbilities; set => activeAbilities = value; }
    public abstract void Attack(ITakeDamage target);

    private Ability[] activeAbilities;
    private Obelisk obelisk;
    [SerializeField]private TowerAttackData attackData;
    private AttackController attackController;


    private void Awake()
    {
        obelisk = GetComponent<Obelisk>();
        this.attackController = new AttackController(this, WaveManager.Instance);
    }

    protected void Update()
    {
        attackController.Tick();
    }
}
