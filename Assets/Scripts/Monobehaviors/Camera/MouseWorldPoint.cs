using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorldPoint : MonoBehaviour
{
    private static RaycastHit hit;
    private Camera cam;
    private Ray ray;

    public static RaycastHit? RaycastHit => hit;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        ray = cam.ScreenPointToRay(Input.mousePosition);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(cam == null)
        {
            throw new Exception("No Camera component attached to read mouse hit point!");
        }
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            
        }
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.cyan);
    }
}
