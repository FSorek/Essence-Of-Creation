﻿using UnityEngine;

namespace DataBehaviors.Game.Utility
{
    public static class GameTime
    {
        private static float forward;
        private static float lastFrame;

        public static float time => Time.time + forward;
        public static float deltaTime => Time.deltaTime + lastFrame;

        private static void Reset()
        {
            forward = lastFrame = 0;
        }

        public static void SetOffsetTimeForward(float seconds)
        {
            Reset();
            forward = seconds - Time.time;
        }

        public static void SetTimeSinceLastFrame(float seconds)
        {
            Reset();
            lastFrame = seconds - Time.deltaTime;
        }
    }
}