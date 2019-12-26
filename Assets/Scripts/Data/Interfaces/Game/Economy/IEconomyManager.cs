using Data.ScriptableObjects.Game;

namespace Data.Interfaces.Game.Economy
{
    public interface IEconomyManager
    {
        int Essence { get; }
        EconomySettings Settings { get; }
    }
}