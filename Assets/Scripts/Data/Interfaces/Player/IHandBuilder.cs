using Data.Data_Types;
using Data.Player;
using Monobehaviors.BuildSpot;

namespace Data.Interfaces.Player
{
    public interface IHandBuilder
    {
        void Build(Elements element, PlayerBuildingData data, BuildSpotComponent buildSpotComponent);
    }
}