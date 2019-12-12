using Data.Data_Types;
using Data.Interfaces.Player;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEngine.Experimental.VFX.Utility;

namespace DataBehaviors.Player.Particles
{
    public class SummonEffectController
    {
        private readonly VisualEffect summonAir;
        private readonly VisualEffect summonEarth;
        private readonly VisualEffect summonFire;
        private readonly VisualEffect summonWater;

        public SummonEffectController(Monobehaviors.Player.PlayerComponent playerComponent)
        {
            playerComponent.OnElementExecuted += Summon;
            var vfxData = playerComponent.VFXData;

            summonFire = Object.Instantiate(vfxData.SummonFire).GetComponent<VisualEffect>();
            summonAir = Object.Instantiate(vfxData.SummonAir).GetComponent<VisualEffect>();
            summonEarth = Object.Instantiate(vfxData.SummonEarth).GetComponent<VisualEffect>();
            summonWater = Object.Instantiate(vfxData.SummonWater).GetComponent<VisualEffect>();

            summonFire.GetComponent<VFXPositionBinder>().Target = playerComponent.HandTransform;
            summonAir.GetComponent<VFXPositionBinder>().Target = playerComponent.HandTransform;
            summonEarth.GetComponent<VFXPositionBinder>().Target = playerComponent.HandTransform;
            summonWater.GetComponent<VFXPositionBinder>().Target = playerComponent.HandTransform;
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
}