using UnityEngine;

public interface IEntity
{
    Vector3 Position { get; set; }
    void Destroy();
}