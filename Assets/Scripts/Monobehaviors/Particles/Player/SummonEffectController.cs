using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEngine.Experimental.VFX.Utility;

public class SummonEffectController : MonoBehaviour
{
    public GameObject SummonFire;
    public GameObject SummonAir;
    public GameObject SummonEarth;
    public GameObject SummonWater;

    private void Awake()
    {
        PlayerController.OnElementExecuted += Summon;
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
                SummonFire.GetComponent<VisualEffect>().SendEvent("OnPlay");
                break;
            case Elements.Earth:
                DisableAllSummons();
                SummonEarth.GetComponent<VisualEffect>().SendEvent("OnPlay");
                break;
            case Elements.Water:
                DisableAllSummons();
                SummonWater.GetComponent<VisualEffect>().SendEvent("OnPlay");
                break;
            case Elements.Air:
                DisableAllSummons();
                SummonAir.GetComponent<VisualEffect>().SendEvent("OnPlay");
                break;
        }
    }

    private void DisableAllSummons()
    {
        SummonFire.GetComponent<VisualEffect>().SendEvent("OnStop");
        SummonAir.GetComponent<VisualEffect>().SendEvent("OnStop");
        SummonEarth.GetComponent<VisualEffect>().SendEvent("OnStop");
        SummonWater.GetComponent<VisualEffect>().SendEvent("OnStop");
    }
}

