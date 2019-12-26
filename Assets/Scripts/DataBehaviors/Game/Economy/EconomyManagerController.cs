using Data.Interfaces.Game.Economy;
using Data.ScriptableObjects.Game;
using Monobehaviors.Players;

namespace DataBehaviors.Game.Economy
{
    public class EconomyManagerController : IEconomyManager
    {
        private readonly int essencePerEnemy = 10;

        public EconomyManagerController(EconomySettings settings)
        {
            Settings = settings;
            //BuildPlayerState.OnElementBuildingFinished += UseEssenceToBuild;
            //PlaceObeliskPlayerState.OnBuildSpotCreated += UseEssenceForBuildSpot;
            Essence = settings.StartingEssence;
            //essencePerEnemy = settings.EssencePerWave / waveManager.WaveSettings.EnemiesPerWave;
        }

        public int Essence { get; private set; }

        public EconomySettings Settings { get; }

        private void UseEssenceForBuildSpot()
        {
            Essence -= Settings.EssencePerBuildspot;
        }

        private void UseEssenceToBuild(PlayerComponent obj)
        {
            Essence -= Settings.EssencePerSummon;
        }

        private void AddEssence()
        {
            Essence += essencePerEnemy;
        }
    }
}