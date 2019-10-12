using UnityEngine;

public class EconomyManagerController : IEconomyManager
{
    private int playerEssence;
    private int essencePerEnemy = 10;
    private EconomySettings settings;
    private readonly IWaveManager waveManager;

    public EconomyManagerController(EconomySettings settings, IWaveManager waveManager)
    {
        this.settings = settings;
        this.waveManager = waveManager;
        Unit.OnUnitDeath += AddEssence;
        AttunedPlayerState.OnElementBuildingFinished += UseEssenceToBuild;
        PlacingBuildSpotPlayerState.OnBuildSpotCreated += UseEssenceForBuildSpot;
        playerEssence = settings.StartingEssence;
        //essencePerEnemy = settings.EssencePerWave / waveManager.WaveSettings.EnemiesPerWave;
    }
    private void UseEssenceForBuildSpot()
    {
        playerEssence -= settings.EssencePerBuildspot;
    }

    private void UseEssenceToBuild(IPlayer obj)
    {
        playerEssence -= settings.EssencePerSummon;
    }

    private void AddEssence(ITakeDamage obj)
    {
        playerEssence += essencePerEnemy;
    }

    public int Essence => playerEssence;
    public EconomySettings Settings => settings;
}
