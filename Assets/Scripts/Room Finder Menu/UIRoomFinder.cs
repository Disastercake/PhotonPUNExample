using System;
using System.Collections.Generic;
using System.Text;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace PhotonPunExample
{
    [DisallowMultipleComponent]
    public class UIRoomFinder : MonoBehaviour
    {
        [SerializeField] private GameObject _roomMenu;
        [SerializeField] private UIRoomFinderList _listComponent;

        private void OnEnable()
        {
            HandleRoomListUpdated(NetworkManager.instance.RoomList);
            NetworkManager.instance.OnRoomListUpdated += HandleRoomListUpdated;
            NetworkManager.instance.OnRoomJoined += HandleRoomJoined;
        }

        private void OnDisable()
        {
            NetworkManager.instance.OnRoomListUpdated -= HandleRoomListUpdated;
            NetworkManager.instance.OnRoomJoined -= HandleRoomJoined;
        }

        private void HandleRoomListUpdated(List<RoomInfo> roomList)
        {
            _listComponent.Populate(roomList);
        }

        // This will be invoked when a list item is clicked and it calls to join the room.
        public void HandleRoomJoined(Room room)
        {
            gameObject.SetActive(false);
            _roomMenu.SetActive(true);
        }
    }
}
