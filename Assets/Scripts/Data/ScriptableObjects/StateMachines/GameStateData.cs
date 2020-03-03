using Data.Data_Types.Enums;
using UnityEngine;

namespace Data.ScriptableObjects.StateMachines
{
    [CreateAssetMenu(fileName = "Game State Data", menuName = "Essence/Game/State Data")]
    public class GameStateData : StateData<GameStates>
    {
    }
}