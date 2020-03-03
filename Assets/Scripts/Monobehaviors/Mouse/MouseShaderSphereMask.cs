using UnityEngine;

namespace Monobehaviors.Mouse
{
    public class MouseShaderSphereMask : MonoBehaviour
    {
        public float Hardness = .5f;

        private RaycastHit hit;
        public UnityEngine.Camera PlayerCamera;
        public float Radius = 1f;


        // Update is called once per frame
        private void Update()
        {
            var ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Shader.SetGlobalVector("SpherePosition", hit.point);
                Shader.SetGlobalFloat("SphereRadius", Radius);
                Shader.SetGlobalFloat("SphereHardness", Hardness);
            }
        }
    }
}