using UnityEngine;

public class FloorColliderManager : MonoBehaviour
{
    public static GameEntity[] FloorColliderEntities => Instance.floorColliderEntities;
    [SerializeField]private GameEntity[] floorColliderEntities;
    public static FloorColliderManager Instance;

    public void Awake()
    {
        if( Instance != null )
            Destroy(this);
        else
        {
            Instance = this;
        }
    }
}
