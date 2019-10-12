using UnityEngine;
public class BuildEffectController : MonoBehaviour
{
    [SerializeField] private ObjectPool BuildFirePool;
    [SerializeField] private ObjectPool BuildAirPool;
    [SerializeField] private ObjectPool BuildEarthPool;
    [SerializeField] private ObjectPool BuildWaterPool;

    private BuildEffect activeEffect;

    private void Awake()
    {
        AttunedPlayerState.OnElementBuildingStarted += Build;
        AttunedPlayerState.OnElementBuildingInterrupted += BuildStopped;
        AttunedPlayerState.OnElementBuildingFinished += BuildStopped;
    }

    private void BuildStopped(IPlayer obj)
    {
        if (activeEffect != null)
        {
            activeEffect.Detach();
        }
    }

    public void Build(IPlayer playerCon, BuildSpot spot)
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

        activeEffect.SetFollowedTransform(playerCon.HandTransform, spot.Position);
    }
}

