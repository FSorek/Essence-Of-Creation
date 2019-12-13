using Data.Game;
using Data.Interfaces.Game.Economy;
using Data.Interfaces.Game.Waves;
using Data.Interfaces.Player;
using DataBehaviors.Player.States;
using Monobehaviors.Player;

namespace DataBehaviors.Game.Economy
{
    public class EconomyManagerController : IEconomyManager
    {
        private readonly IWaveManager waveManager;
        private readonly int essencePerEnemy = 10;

        public EconomyManagerController(EconomySettings settings, IWaveManager waveManager)
        {
            Settings = settings;
            this.waveManager = waveManager;
            Monobehaviors.Unit.UnitComponent.OnUnitDeath += AddEssence;
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

        private void AddEssence(Monobehaviors.Unit.UnitComponent unitComponent)
        {
            Essence += essencePerEnemy;
        }
    }
}