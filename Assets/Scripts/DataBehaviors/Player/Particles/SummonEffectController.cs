using Data.Data_Types;
using Data.Interfaces.Player;
using Monobehaviors.Player;
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

        public SummonEffectController(PlayerComponent playerComponent)
        {
            playerComponent.PlayerInput.OnFirePressed += PlayerInputOnFirePressed;
            playerComponent.PlayerInput.OnAirPressed += PlayerInputOnAirPressed;
            playerComponent.PlayerInput.OnEarthPressed += PlayerInputOnEarthPressed;
            playerComponent.PlayerInput.OnWaterPressed += PlayerInputOnWaterPressed;
            
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

        private void PlayerInputOnWaterPressed()
        {
            DisableAllSummons();
            summonWater.SendEvent("OnPlay");
        }

        private void PlayerInputOnEarthPressed()
        {
            DisableAllSummons();
            summonEarth.SendEvent("OnPlay");
        }

        private void PlayerInputOnAirPressed()
        {
            DisableAllSummons();
            summonAir.SendEvent("OnPlay");
        }

        private void PlayerInputOnFirePressed()
        {
            DisableAllSummons();
            summonFire.SendEvent("OnPlay");
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