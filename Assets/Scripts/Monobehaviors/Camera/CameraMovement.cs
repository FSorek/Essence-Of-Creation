﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public int Acceleration = 1;
    public float PanningSpeed = 1f;
    void FixedUpdate()
    {
        float xAxisValue = Mathf.Clamp(Mathf.Pow(Input.GetAxis("Horizontal"), Acceleration), -1, 1);
        float zAxisValue = Mathf.Clamp(Mathf.Pow(Input.GetAxis("Vertical"), Acceleration), -1, 1);
        transform.Translate(new Vector3(xAxisValue * PanningSpeed, 0.0f, zAxisValue * PanningSpeed) * Time.deltaTime);
    }
}
