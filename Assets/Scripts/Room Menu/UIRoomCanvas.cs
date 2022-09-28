using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace PhotonPunExample
{
    [DisallowMultipleComponent]
    public class UIRoomCanvas : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject _previousMenu;

        [SerializeField] private TMPro.TextMeshProUGUI _roomNameTextComp;

        public override void OnEnable()
        {
            Room room = PhotonNetwork.CurrentRoom;
            _roomNameTextComp.text = room != null ? room.Name : string.Empty;

            base.OnEnable();
        }

        public override void OnLeftRoom()
        {
            gameObject.SetActive(false);
            _previousMenu.SetActive(true);
        }

        public void TryStartGame()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
                return;
            }

            // PhotonNetwork.AutomaticallySyncScene = true;
            Debug.LogFormat("PhotonNetwork : Loading Level \"GameScene\" for {0} players.", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.LoadLevel("GameScene");
        }
    }
}
