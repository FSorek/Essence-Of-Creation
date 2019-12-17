using System;
using Monobehaviors.Game.Managers;
using Monobehaviors.Projectiles;
using UnityEngine;

namespace Monobehaviors.Unit
{
    [RequireComponent(typeof(UnitComponent))]
    public class UnitMove : MonoBehaviour
    {
        [SerializeField] private TransformList reachPoints;
        [SerializeField] private float baseMovementSpeed;
        
        private SimpleMove move;
        private int reachpoint;
        private Vector3 movePosition;
        private UnitComponent unit;
        
        private void Awake()
        {
            move = new SimpleMove(transform);
            unit = GetComponent<UnitComponent>();
        }
        
        private void Start()
        {
            unit.RegisterStat(StatName.MovementSpeed, baseMovementSpeed);
        }

        private void OnEnable()
        {
            reachpoint = 0;
            movePosition = Vector3.zero;
        }
        
        private void Update()
        {
            Run();
        }

        private void Run()
        {
            if (movePosition == Vector3.zero)
            {
                movePosition = reachPoints.Items[reachpoint].position;
            }

            if (Vector3.Distance(transform.position, movePosition) <= 1f)
            {
                reachpoint = reachpoint >= reachPoints.Items.Count - 1 ? 0 : reachpoint + 1;
                movePosition = reachPoints.Items[reachpoint].position;
            }
            move.Move(movePosition, unit.GetStat(StatName.MovementSpeed));
        }
    }
}