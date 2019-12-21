using Data.Game;
using UnityEngine;

namespace Monobehaviors.Game.Managers
{
    public class GameStateMachine : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private GameStateData stateData;
        private StateMachine<GameStates> stateMachine;

        private void Awake()
        {
            stateMachine = new StateMachine<GameStates>(stateData);
            var gameStarted = new GameStarted(gameSettings);
            
            stateMachine.RegisterState(GameStates.Started, gameStarted);
        }

        private void Update()
        {
            stateMachine.Tick();
        }
    }
}