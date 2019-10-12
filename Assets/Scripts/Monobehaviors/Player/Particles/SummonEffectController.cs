using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEngine.Experimental.VFX.Utility;

public class SummonEffectController
{
    private VisualEffect summonFire;
    private VisualEffect summonAir;
    private VisualEffect summonEarth;
    private VisualEffect summonWater;

    public SummonEffectController(IPlayer player)
    {
        player.OnElementExecuted += Summon;
        var vfxData = player.VFXData;

        summonFire = GameObject.Instantiate(vfxData.SummonFire).GetComponent<VisualEffect>();
        summonAir = GameObject.Instantiate(vfxData.SummonAir).GetComponent<VisualEffect>();
        summonEarth = GameObject.Instantiate(vfxData.SummonEarth).GetComponent<VisualEffect>();
        summonWater = GameObject.Instantiate(vfxData.SummonWater).GetComponent<VisualEffect>();

        summonFire.GetComponent<VFXPositionBinder>().Target = player.HandTransform;
        summonAir.GetComponent<VFXPositionBinder>().Target = player.HandTransform;
        summonEarth.GetComponent<VFXPositionBinder>().Target = player.HandTransform;
        summonWater.GetComponent<VFXPositionBinder>().Target = player.HandTransform;
    }

    public void Summon(Elements activeElement)
    {
        switch (activeElement)
        {
            default:
                DisableAllSummons();
                break;
            case Elements.None:
                DisableAllSummons();
                break;
            case Elements.Fire:
                DisableAllSummons();
                summonFire.SendEvent("OnPlay");
                break;
            case Elements.Earth:
                DisableAllSummons();
                summonEarth.SendEvent("OnPlay");
                break;
            case Elements.Water:
                DisableAllSummons();
                summonWater.SendEvent("OnPlay");
                break;
            case Elements.Air:
                DisableAllSummons();
                summonAir.SendEvent("OnPlay");
                break;
        }
    }

    private void DisableAllSummons()
    {
        summonFire.SendEvent("OnStop");
        summonAir.SendEvent("OnStop");
        summonEarth.SendEvent("OnStop");
        summonWater.SendEvent("OnStop");
    }
}

