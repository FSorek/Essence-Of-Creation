using UnityEngine;
using UnityEngine.Experimental.VFX;

public class GameEntity : MonoBehaviour, IEntity
{

    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    public virtual void Destroy() // hmm
    {
        var vfx = GetComponent<VisualEffect>();
        vfx?.Stop();
        Destroy(this.gameObject, 4f); // pool
        Destroy(this);
    }
}
