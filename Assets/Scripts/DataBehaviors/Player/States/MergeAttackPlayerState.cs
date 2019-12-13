using System;
using System.Linq;
using Data.Data_Types;
using Data.Interfaces.Player;
using DataBehaviors.Game.Entity.Targeting;
using Monobehaviors.BuildSpot;
using Monobehaviors.Player;
using UnityEngine;

namespace DataBehaviors.Player.States
{
    public class MergeAttackPlayerState : PlayerState
    {
        private AttractionSpot currentAttractionSpot;
        private float timeStarted;

        public MergeAttackPlayerState(PlayerComponent player, PlayerStateMachine stateMachine) : base(player)
        {
        }

        public static event Action<AttractionSpot> OnExtractTowerStarted = delegate { };
        public static event Action<AttractionSpot> OnExtractTowerFinished = delegate { };
        public static event Action OnExtractTowerInterrupted = delegate { };

        public static event Action<AttractionSpot> OnAssembleTowerStarted = delegate { };
        public static event Action<AttractionSpot> OnAssembleTowerFinished = delegate { };
        public static event Action OnAssembleTowerInterrupted = delegate { };

        public static event Action OnMiddleMouseAttack = delegate { };

        public override void ListenToState()
        {
            ///-------- Left Mouse

            if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
            {
                var potentialTargets = RangeTargetScanner.GetTargets(player.HandTransform.transform.position,
                    AttractionSpot.AttractionSpots.ToArray(),
                    player.BuildData.BuildSpotDetectionRange)?.Where(t => !t.GetComponent<AttractionSpot>().IsOccupied).ToArray();
                currentAttractionSpot =
                    ClosestEntityFinder.GetClosestTransform(potentialTargets, player.HandTransform.position).GetComponent<AttractionSpot>();
                if (currentAttractionSpot != null)
                {
                    OnAssembleTowerStarted(currentAttractionSpot);
                    timeStarted = Time.time;
                }
            }

            if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))
                if (Time.time - timeStarted > player.BuildData.BuildTime && currentAttractionSpot != null)
                {
                    OnAssembleTowerFinished(currentAttractionSpot);
                    currentAttractionSpot = null;
                }

            if (Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1))
                if (Time.time - timeStarted < player.BuildData.BuildTime)
                    OnAssembleTowerInterrupted();

            ///-------- Right Mouse

            if (Input.GetMouseButtonDown(1) && !Input.GetMouseButton(0))
            {
                var potentialTargets = RangeTargetScanner.GetTargets(player.HandTransform.transform.position,
                        AttractionSpot.AttractionSpots.ToArray(),
                        player.BuildData.BuildSpotDetectionRange)?.Where(t => t.GetComponent<AttractionSpot>().IsOccupied)
                    .ToArray();
                if(potentialTargets == null)
                    return;
                currentAttractionSpot =
                    ClosestEntityFinder.GetClosestTransform(potentialTargets, player.HandTransform.position).GetComponent<AttractionSpot>();
                if (currentAttractionSpot != null)
                {
                    OnExtractTowerStarted(currentAttractionSpot);
                    timeStarted = Time.time;
                }
            }

            if (Input.GetMouseButton(1) && !Input.GetMouseButton(0))
                if (Time.time - timeStarted > player.BuildData.BuildTime && currentAttractionSpot != null)
                {
                    OnExtractTowerFinished(currentAttractionSpot);
                    currentAttractionSpot = null;
                }

            if (Input.GetMouseButtonUp(1) && !Input.GetMouseButton(0))
                if (Time.time - timeStarted < player.BuildData.BuildTime)
                    OnExtractTowerInterrupted();

            ///-------- Middle Mouse
            if (Input.GetMouseButton(2)) OnMiddleMouseAttack();
            return;
        }

        public override void OnStateExit()
        {
        }

        public override void OnStateEnter()
        {
            
        }
    }
}