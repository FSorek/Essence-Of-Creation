using System;
using Data.Data_Types;
using Monobehaviors.Game.Managers;
using UnityEngine;

namespace Monobehaviors.Player
{
    [CreateAssetMenu(fileName = "Player State Data", menuName = "Essence/Player/State Data")]
    public class PlayerStateData : StateData<PlayerStates>
    {
    }
}