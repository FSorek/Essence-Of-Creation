using System;
using System.Linq;
using UnityEngine;

public class PlacingBuildSpotPlayerState : PlayerState
{
    public static event Action OnIncreaseBuildSpotHeight;
    public static event Action OnDecreaseBuildSpotHeight;
    public static event Action OnBuildSpotCreated;
    public static event Action OnBuildSpotCancelled;
    public static Action<Vector3, Vector3> PlacingBuildSpot;

    private GameEntity[] buildSpotFloorColliders;
    private GameEntity closestAvailableSpace;

    private float lastColliderScanTime = 0;

    public PlacingBuildSpotPlayerState(PlayerController playerC) : base(playerC)
    {
        buildSpotFloorColliders = FloorColliderManager.FloorColliderEntities;
        closestAvailableSpace = ClosestEntityFinder<GameEntity>.GetClosestTransform(buildSpotFloorColliders, playerC.HandTransform.position);
    }

    public override void ListenToState()
    {
        if (GameTime.time - lastColliderScanTime >= 1f)
        {
            lastColliderScanTime = Time.time;
            closestAvailableSpace = ClosestEntityFinder<GameEntity>.GetClosestTransform(buildSpotFloorColliders, playerC.HandTransform.position);
        }
        if(closestAvailableSpace == null)
            return;
        var closestPoint = closestAvailableSpace.GetComponent<Collider>()
            .ClosestPointOnBounds(playerC.HandTransform.position);
        var wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0f)
            OnIncreaseBuildSpotHeight();
        if (wheelInput < 0f)
            OnDecreaseBuildSpotHeight();
        PlacingBuildSpot(closestPoint, playerC.HandTransform.position);
        if (Input.GetMouseButtonDown(0))
        {
            OnBuildSpotCreated();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            OnBuildSpotCancelled();
        }
    }

    public override void OnStateExit()
    {
        OnBuildSpotCancelled();
    }
}
