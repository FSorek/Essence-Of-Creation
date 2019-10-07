using UnityEngine;

public class FloorColliderManager : MonoBehaviour
{
    public static GameEntity[] FloorColliderEntities => Instance?.GetFloorColliders();
    [SerializeField]private Transform floorColliderParent;
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

    private GameEntity[] GetFloorColliders()
    {
        int children = floorColliderParent.childCount;
        GameEntity[] colliders = new GameEntity[children];
        for (int i = 0; i < children; ++i)
            colliders[i] = floorColliderParent.GetChild(i).GetComponent<GameEntity>();
        return colliders;
    }
}
