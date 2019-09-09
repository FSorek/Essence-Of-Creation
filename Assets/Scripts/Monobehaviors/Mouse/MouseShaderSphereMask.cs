using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseShaderSphereMask : MonoBehaviour
{
    public Camera PlayerCamera;
    public float Radius = 1f;
    public float Hardness = .5f;

    private RaycastHit hit;
    

    // Update is called once per frame
    void Update()
    {
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Shader.SetGlobalVector("SpherePosition", hit.point);
            Shader.SetGlobalFloat("SphereRadius", Radius);
            Shader.SetGlobalFloat("SphereHardness", Hardness);
        }
    }
}
