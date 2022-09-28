using Common;
using Photon.Pun;
using Photon.Realtime;
using PrefabFactorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PhotonPunExample
{
    [DisallowMultipleComponent]
    public class UIRoomFinderListItem : PoolableGameObject, IPointerClickHandler
    {

        [SerializeField] private TMPro.TextMeshProUGUI _nameTextComp;

        public int Order { get; private set; }
        public RoomInfo RoomInfo { get; private set; }

        public void SetRoom(RoomInfo roomInfo, int order)
        {
            Order = order;
            RoomInfo = roomInfo;
            RefreshVisuals();
        }

        private void RefreshVisuals()
        {
            if (RoomInfo != null)
                _nameTextComp.text = $"{RoomInfo.Name} ({RoomInfo.PlayerCount} / {RoomInfo.MaxPlayers})";
            else
                _nameTextComp.text = "ERROR";
            
            transform.SetSiblingIndex(Order);
        }

        protected override void SetStateToPooled()
        {
            RoomInfo = null;
            Order = 0;
            base.SetStateToPooled();
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            NetworkManager.instance.JoinRoom(RoomInfo.Name);
        }
    }
}
