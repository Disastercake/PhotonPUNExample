using UnityEngine;

namespace PhotonPunExample.Common
{
    [DisallowMultipleComponent]
    public class DontDestroyOnLoad : MonoBehaviour
    {
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Destroy(this);
        }
        
    }
}
