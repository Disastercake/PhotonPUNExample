using System;
using UnityEngine;

namespace Common
{
    [DisallowMultipleComponent]
    public class WireframeSphereGizmo : MonoBehaviour
    {
#if UNITY_EDITOR

        [SerializeField] private Color _color = Color.white;
        [SerializeField] private bool _wireframe = true;
        [SerializeField] private float _radius = 1f;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = _color;
            
            if (_wireframe)
                Gizmos.DrawWireSphere(transform.position, _radius);
            else
                Gizmos.DrawSphere(transform.position, _radius);
        }

#endif
    }
}
