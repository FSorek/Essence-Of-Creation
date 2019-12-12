using Data.Unit;

namespace Data.Interfaces.Game.Waves
{
    internal interface IWaveGenerator
    {
        UnitData Generate(int powerPoints);
    }
}