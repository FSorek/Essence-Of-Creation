using UnityEngine;

namespace Data.Interfaces.Game
{
    public interface IMover
    {
        void Move(Vector3 position, float speed);
    }
}