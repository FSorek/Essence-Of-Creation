using Data.Data_Types;
using Data.Interfaces.Player;
using DataBehaviors.Player.States;
using Monobehaviors.Pooling;
using UnityEngine;

namespace Monobehaviors.Player.Particles
{
    public class BuildEffectController : MonoBehaviour
    {
        private BuildEffect activeEffect;
        [SerializeField] private ObjectPool BuildAirPool;
        [SerializeField] private ObjectPool BuildEarthPool;
        [SerializeField] private ObjectPool BuildFirePool;
        [SerializeField] private ObjectPool BuildWaterPool;

        private void Awake()
        {
            AttunedPlayerState.OnElementBuildingStarted += Build;
            AttunedPlayerState.OnElementBuildingInterrupted += BuildStopped;
            AttunedPlayerState.OnElementBuildingFinished += BuildStopped;
        }

        private void BuildStopped(IPlayer obj)
        {
            if (activeEffect != null) activeEffect.Detach();
        }

        public void Build(IPlayer playerCon, BuildSpot.BuildSpotComponent spotComponent)
        {
            switch (playerCon.CurrentElement)
            {
                case Elements.None:
                    break;
                case Elements.Fire:
                    activeEffect = BuildFirePool.Get().GetComponent<BuildEffect>();
                    break;
                case Elements.Earth:
                    activeEffect = BuildEarthPool.Get().GetComponent<BuildEffect>();
                    break;
                case Elements.Water:
                    activeEffect = BuildWaterPool.Get().GetComponent<BuildEffect>();
                    break;
                case Elements.Air:
                    activeEffect = BuildAirPool.Get().GetComponent<BuildEffect>();
                    break;
                case Elements.Life:
                    break;
                case Elements.Death:
                    break;
            }

            activeEffect.SetFollowedTransform(playerCon.HandTransform, spotComponent.transform.position);
        }
    }
}