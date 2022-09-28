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
    }
}
