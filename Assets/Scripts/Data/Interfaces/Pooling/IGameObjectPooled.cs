using Monobehaviors.Pooling;

namespace Data.Interfaces.Pooling
{
    public interface IGameObjectPooled
    {
        ObjectPool Pool { get; set; }
    }
}