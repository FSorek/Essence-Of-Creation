using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Building Data", menuName = "Essence/Player/Building Data", order = 0)]
public class PlayerBuildingData : ScriptableObject
{
    public float BuildSpotDetectionRange = 20f;
    public float BuildTime = 2f;

    public GameObject BuildSpotFundamentals;
}
