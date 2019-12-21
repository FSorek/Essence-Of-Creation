using Data.Game;
using DataBehaviors.Game.Waves;

namespace Monobehaviors.Game.Managers
{
    public class GameStarted : IState
    {
        private GameSettings gameSettings;
        public GameStarted(GameSettings gameSettings)
        {
        }

        public void StateEnter()
        {
            
        }

        public void ListenToState()
        {
            
        }

        public void StateExit()
        {
            
        }
    }
    
    public class GameSpawningWave : IState
    {
        private WaveSpawner waveSpawner;
        public GameSpawningWave(WaveSpawner waveSpawner)
        {
            this.waveSpawner = waveSpawner;
        }

        public void StateEnter()
        {
            
        }

        public void ListenToState()
        {
            
        }

        public void StateExit()
        {
            
        }
    }
    
    public class GameAwaitingNextWave : IState
    {
        private GameSettings gameSettings;
        public GameAwaitingNextWave(GameSettings gameSettings)
        {
        }

        public void StateEnter()
        {
            
        }

        public void ListenToState()
        {
            
        }

        public void StateExit()
        {
            
        }
    }
}