using System;
using UnityEngine;

namespace Monobehaviors.Camera
{
    public class MouseWorldPoint : MonoBehaviour
    {
        private static RaycastHit hit;
        private UnityEngine.Camera cam;
        private Ray ray;

        public static RaycastHit? RaycastHit => hit;

        // Start is called before the first frame update
        private void Start()
        {
            cam = GetComponent<UnityEngine.Camera>();
            ray = cam.ScreenPointToRay(Input.mousePosition);
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (cam == null) throw new Exception("No Camera component attached to read mouse hit point!");
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
            }

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.cyan);
        }
    }
}