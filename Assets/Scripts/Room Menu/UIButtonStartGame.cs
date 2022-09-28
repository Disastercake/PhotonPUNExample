using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
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
            
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.LoadLevel(_nextScene);
        }
    }
}
