using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class Obelisk : MonoBehaviour, IObelisk
{
    public Vector3 AttackSpawnPosition => transform.position;
    public Vector3 Position => transform.position;

    public List<BaseElement> InfusedElements = new List<BaseElement>();

    private void Awake()
    {
    }
}

public interface IObelisk : IEntity
{
    Vector3 AttackSpawnPosition { get; }
}