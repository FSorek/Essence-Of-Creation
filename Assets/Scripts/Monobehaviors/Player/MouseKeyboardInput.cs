using UnityEngine;

public class MouseKeyboardInput : IInputProcessor
{
    private Elements currentElement = Elements.None;

    public void GlobalControls()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ProcessElement(Elements.Fire);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ProcessElement(Elements.Air);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ProcessElement(Elements.Water);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ProcessElement(Elements.Earth);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ProcessElement(Elements.Invocation);
        }
    }

    private void ProcessElement(Elements element) => currentElement = currentElement != element ? element : Elements.None;
    public Elements CurrentElement => currentElement;
}
