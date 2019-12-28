using Data.ScriptableObjects.Attacks;
using Data.ScriptableObjects.Globals;
using DataBehaviors.Essences;
using UnityEngine;

namespace Monobehaviors.Essences.Attacks
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private TransformList enemiesList;
        [SerializeField] private AttackBehaviour attackBehaviour;
        private AttackController attackController;


        private void Awake()
        {
            attackController = new AttackController(transform, attackBehaviour, enemiesList);
        }

        private void Update()
        {
            attackController.Tick();
        }
    }
}