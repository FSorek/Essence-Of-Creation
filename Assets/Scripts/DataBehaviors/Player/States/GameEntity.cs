using UnityEngine;

public class GameEntity : MonoBehaviour, IEntity
{
    public Vector3 Position => transform.position;
}
