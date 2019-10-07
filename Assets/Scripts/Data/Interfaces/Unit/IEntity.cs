using UnityEngine;

public interface IEntity
{
    GameObject GameObject { get; }
    Vector3 Position { get; set; }
    void Destroy();
}