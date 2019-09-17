using UnityEngine;

public static class GameTime
{
    private static float forward;


    public static void Reset() => forward = 0;

    public static void MoveTimeForward(float seconds)
    {
        forward += seconds;
    }

    public static float time => Time.time + forward;
    public static float deltaTime => Time.deltaTime;
}

