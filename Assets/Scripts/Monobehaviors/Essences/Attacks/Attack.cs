using Data.ScriptableObjects.Attacks;
using Data.ScriptableObjects.Globals;
using DataBehaviors.Essences;
using UnityEngine;

namespace Monobehaviors.Essences.Attacks
{
    public class AttackComponent : MonoBehaviour
    {
        [SerializeField] private TransformList enemiesList;
        [SerializeField] private TowerAttack towerAttack;
        private AttackController attackController;


        private void Awake()
        {
            attackController = new AttackController(transform, towerAttack, enemiesList);
        }

        private void Update()
        {
            attackController.Tick();
        }
    }
}