using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    [DisallowMultipleComponent]
    public class UIConnectionCanvas : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Button _connectServerBtn;
        // [SerializeField] private Button _joinRoomButton;
        // [SerializeField] private Button _createRoomButton;
        // [SerializeField] private TMP_InputField _inputField;

        [SerializeField] private GameObject _roomListMenu;
        // [SerializeField] private GameObject _roomMenu;

        public override void OnEnable()
        {
            if (PhotonNetwork.IsConnectedAndReady)
                OnConnectedToMaster();
            // else
            //     SetStateToNotServerConnected();
            
            base.OnEnable();
        }

        public override void OnConnectedToMaster()
        {
            gameObject.SetActive(false);
            _roomListMenu.SetActive(true);
        }

        // public override void OnJoinedRoom()
        // {
        //     gameObject.SetActive(false);
        //     _roomMenu.SetActive(true);
        // }

        // private void SetStateToServerConnected()
        // {
        //     _connectServerBtn.gameObject.SetActive(false);
        //     _joinRoomButton.gameObject.SetActive(true);
        //     _createRoomButton.gameObject.SetActive(true);
        //     _inputField.interactable = true;
        // }
        //
        // private void SetStateToNotServerConnected()
        // {
        //     _connectServerBtn.gameObject.SetActive(true);
        //     _joinRoomButton.gameObject.SetActive(false);
        //     _createRoomButton.gameObject.SetActive(false);
        //     _inputField.interactable = false;
        // }
    }
}
