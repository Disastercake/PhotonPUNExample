using System;
using Photon.Pun;
using UnityEngine;

namespace PhotonPunExample
{
    [DisallowMultipleComponent]
    public class DisconnectWatcher : MonoBehaviour
    {
        [SerializeField, Tooltip("This component will load this scene when disconnected from Photon.")]
        private string _sceneName;
        
        private void Update()
        {
            if (PhotonNetwork.IsConnected)
                Debug.LogError("Disconnected from Photon server.");
        }
    }
}
