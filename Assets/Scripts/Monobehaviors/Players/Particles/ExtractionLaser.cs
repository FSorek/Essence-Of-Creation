using System;
using System.Collections;
using System.Collections.Generic;
using Data.Data_Types.Enums;
using Data.ScriptableObjects.Player;
using UnityEngine;

public class ExtractionLaser : MonoBehaviour
{
    [SerializeField] private PlayerStateData playerStateData;
    [SerializeField] private PlayerBuildData playerBuildData;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform playerParticlePosition;

    private void Awake()
    {
        if (lineRenderer.enabled)
            lineRenderer.enabled = false;
        
        playerStateData.OnStateEntered += PlayerStateDataOnStateEntered;
        playerStateData.OnStateExit += PlayerStateDataOnStateExit;
    }

    private void PlayerStateDataOnStateExit(PlayerStates state)
    {
        if(state != PlayerStates.EXTRACTING)
            return;

        lineRenderer.enabled = false;
    }

    private void PlayerStateDataOnStateEntered(PlayerStates state)
    {
        if(state != PlayerStates.EXTRACTING)
            return;

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, playerParticlePosition.position);
        lineRenderer.SetPosition(1, playerBuildData.TargetAttraction.transform.position);
    }
    
    private void Update()
    {
        if(lineRenderer.enabled)
            lineRenderer.SetPosition(0, playerParticlePosition.position);
    }
}
