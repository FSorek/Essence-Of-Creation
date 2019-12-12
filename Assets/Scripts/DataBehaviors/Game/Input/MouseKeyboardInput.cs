using Data.Data_Types;
using Data.Interfaces.Player;
using UnityEngine;

namespace DataBehaviors.Game.Input
{
    public class MouseKeyboardInput : IInputProcessor
    {
        public void GlobalControls()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1)) ProcessElement(Elements.Fire);

            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2)) ProcessElement(Elements.Air);

            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3)) ProcessElement(Elements.Water);

            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha4)) ProcessElement(Elements.Earth);

            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha5)) ProcessElement(Elements.Invocation);
        }

        public Elements CurrentElement { get; private set; } = Elements.None;

        private void ProcessElement(Elements element)
        {
            CurrentElement = CurrentElement != element ? element : Elements.None;
        }
    }
}