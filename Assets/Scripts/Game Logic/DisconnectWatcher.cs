using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhotonPunExample
{
    [DisallowMultipleComponent]
    public class DisconnectWatcher : MonoBehaviour
    {
        [SerializeField, Tooltip("This component will load this scene when disconnected from Photon.")]
        private string _sceneName;
        
        private void Update()
        {
            if (!PhotonNetwork.IsConnected)
            {
                Debug.LogWarning("(PhotonNetwork.IsConnected == false) -> Returning to main menu.");
                SceneManager.LoadScene(_sceneName);
            }
        }
    }
}
