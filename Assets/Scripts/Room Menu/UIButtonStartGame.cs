using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace PhotonPunExample
{
    [DisallowMultipleComponent,
     RequireComponent(typeof(Button), typeof(Image))]
    public class UIButtonStartGame : MonoBehaviour
    {
        
        [SerializeField] private string _nextScene = "GameScene";
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(StartGame);
        }

        private void OnEnable()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            HandleInteractable();
            NetworkManager.instance.MasterClientSwitched += OnMasterClientSwitched;
        }

        private void OnDisable()
        {
            NetworkManager.instance.MasterClientSwitched -= OnMasterClientSwitched;
        }

        private void OnMasterClientSwitched(Player newMasterClient)
        {
            HandleInteractable();
        }

        private void HandleInteractable()
        {
            // Only the MasterClient can start the game.
            _button.interactable = PhotonNetwork.LocalPlayer.IsMasterClient;
        }

        private void StartGame()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
                return;
            }

            Debug.Log(
                $"PhotonNetwork : Loading Level \"{_nextScene}\" for {PhotonNetwork.CurrentRoom.PlayerCount} players.");
            
            PhotonNetwork.LoadLevel(_nextScene);
        }
        
    }
}
