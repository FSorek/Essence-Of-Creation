using Data.Data_Types.Enums;
using Data.ScriptableObjects.StateMachines;
using UnityEngine;

namespace Data.ScriptableObjects.Player
{
    [CreateAssetMenu(fileName = "Player State Data", menuName = "Essence/Player/State Data")]
    public class PlayerStateData : StateData<PlayerStates>
    {
    }
}