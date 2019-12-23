using System;
using Data.Data_Types;
using Monobehaviors.Game.Managers;
using Monobehaviors.Projectiles;
using UnityEngine;

namespace Monobehaviors.Unit
{
    [RequireComponent(typeof(StatController))]
    public class UnitMove : MonoBehaviour
    {
        [SerializeField] private TransformList reachPoints;
        
        private IMover move;
        private int reachpoint;
        private Vector3 movePosition;
        private float moveSpeed;
        private StatController stats;
        
        private void Awake()
        {
            move = new NavMeshMove(transform);
            stats = GetComponent<StatController>();
            InvokeRepeating(nameof(UpdateMoveSpeed), 0f, 0.1f);
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

        public void UpdateMoveSpeed()
        {
            var currentSpeed = stats.GetStat(StatName.MovementSpeed);
            if(currentSpeed != null && moveSpeed == currentSpeed) return;

            moveSpeed = currentSpeed;
        }

        private void Run()
        {
            if (movePosition == Vector3.zero)
            {
                movePosition = reachPoints.Items[reachpoint].position;
            }

            if (Vector3.Distance(transform.position, movePosition) <= 15f)
            {
                reachpoint = reachpoint >= reachPoints.Items.Count - 1 ? 0 : reachpoint + 1;
                movePosition = reachPoints.Items[reachpoint].position;
            }
            move.Move(movePosition, moveSpeed);
        }
    }
}