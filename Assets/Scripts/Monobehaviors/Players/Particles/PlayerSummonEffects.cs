using Data.Data_Types.Enums;
using Data.ScriptableObjects.Player;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEngine.Experimental.VFX.Utility;

namespace Monobehaviors.Players.Particles
{
    public class PlayerSummonEffects : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private PlayerVFXData vfxData;
        [SerializeField] private PlayerBuildData buildData;
        [SerializeField] private PlayerStateData playerStateData;
        
        private VisualEffect summonAir;
        private VisualEffect summonEarth;
        private VisualEffect summonFire;
        private VisualEffect summonWater;

        public void Awake()
        {
            input.OnFirePressed += PlayerInputOnFirePressed;
            input.OnAirPressed += PlayerInputOnAirPressed;
            input.OnEarthPressed += PlayerInputOnEarthPressed;
            input.OnWaterPressed += PlayerInputOnWaterPressed;
            
            summonFire = Instantiate(vfxData.SummonFire).GetComponent<VisualEffect>();
            summonAir = Instantiate(vfxData.SummonAir).GetComponent<VisualEffect>();
            summonEarth = Instantiate(vfxData.SummonEarth).GetComponent<VisualEffect>();
            summonWater = Instantiate(vfxData.SummonWater).GetComponent<VisualEffect>();

            summonFire.GetComponent<VFXPositionBinder>().Target = buildData.ConstructorObject;
            summonAir.GetComponent<VFXPositionBinder>().Target = buildData.ConstructorObject;
            summonEarth.GetComponent<VFXPositionBinder>().Target = buildData.ConstructorObject;
            summonWater.GetComponent<VFXPositionBinder>().Target = buildData.ConstructorObject;
            
            playerStateData.OnStateEntered += PlayerStateDataOnStateEntered;
            
            Debug.Log(buildData.ConstructorObject.position);
        }

        private void PlayerStateDataOnStateEntered(PlayerStates state)
        {
            if(state == PlayerStates.WEAVE_ESSENCE)
                DisableAllSummons();
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