using Data.Game;
using Data.Interfaces.Game.Economy;
using DataBehaviors.Game.Economy;
using UnityEngine;

namespace Monobehaviors.Game.Managers
{
    public class EconomyManager : MonoBehaviour
    {
        public static IEconomyManager Instance;
        public EconomySettings EconomySettings;

        private void Awake()
        {
            Instance = new EconomyManagerController(EconomySettings, WaveManager.Instance);
        }
    }
}