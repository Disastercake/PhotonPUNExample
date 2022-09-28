using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace PhotonPunExample
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {

        private const int MAX_PLAYERS_PER_ROOM = 5;
        
        #region Singleton

        public static NetworkManager instance;

        public List<RoomInfo> RoomList { get; private set; } = new List<RoomInfo>();

        public event Action<List<RoomInfo>> OnRoomListUpdated;
        public event Action<Room> OnRoomJoined;

        private void Awake()
        {
            instance = this;
        }

        #endregion

        #region Connection Methods

        public bool ConnectToServer()
        {
            bool result = PhotonNetwork.ConnectUsingSettings();

            Debug.Log($"ConnectToServer: {result}");

            return result;
        }

        public bool CreateRoom(string roomName)
        {
            var roomOptions = new RoomOptions
            {
                MaxPlayers = MAX_PLAYERS_PER_ROOM,
            };

            bool result = PhotonNetwork.CreateRoom(roomName, roomOptions);

            Debug.Log($"Create room \"{roomName}\": {result}");

            return result;
        }

        public bool JoinRoom(string roomName)
        {
            bool result = PhotonNetwork.JoinRoom(roomName);

            Debug.Log($"Join room \"{roomName}\": {result}");

            return result;
        }

        // public bool FetchRoomList()
        // {
        //     bool result = PhotonNetwork.GetCustomRoomList(TypedLobby.Default, string.Empty);
        //
        //     Debug.Log($"Fetching Room List: {result}");
        //
        //     return result;
        // }

        #endregion

        #region PUN Listeners

        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster()");
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby()");
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("OnCreatedRoom()");
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom()");
            OnRoomJoined?.Invoke(PhotonNetwork.CurrentRoom);
        }

        public override void OnRoomListUpdate(List<RoomInfo> updatedRooms)
        {
            // Debug Logging.
            var strbldr = new StringBuilder();
            strbldr.AppendLine("OnRoomListUpdate() received:");
            foreach (RoomInfo roomInfo in updatedRooms)
                strbldr.AppendLine($"{roomInfo.Name} ({roomInfo.PlayerCount} / {roomInfo.MaxPlayers})");
            Debug.Log(strbldr.ToString());

            
            // Add or update the RoomInfo.
            for (int i = 0; i < updatedRooms.Count; i++)
            {
                RoomInfo roomInfo = updatedRooms[i];

                int index = RoomList.IndexOf(roomInfo);

                if (index >= 0)
                    RoomList[index] = roomInfo;
                else
                    RoomList.Add(roomInfo);
            }
            
            
            // Groom the list to remove any invalid rooms.
            for (int i = RoomList.Count - 1; i >= 0; i--)
            {
                RoomInfo roomInfo = RoomList[i];
                    
                int playerCount = roomInfo.PlayerCount;
                int maxPlayers = roomInfo.MaxPlayers;
                
                if (!CheckIsRoomValid(roomInfo))
                {
                    Debug.Log($"Removing roomInfo for \"{roomInfo.Name}\".  PlayerCount: {playerCount}   MaxPlayers: {maxPlayers}");
                    RoomList.RemoveAt(i);
                }
            }

            OnRoomListUpdated?.Invoke(RoomList);
        }

        private bool CheckIsRoomValid(RoomInfo roomInfo)
        {
            int playerCount = roomInfo.PlayerCount;
            int maxPlayers = roomInfo.MaxPlayers;
            
            // (maxPlayers == 0) means that there is no max player limit on the room. (maybe)
            return (playerCount > 0 && (maxPlayers == 0 || playerCount < maxPlayers));
        }

        #endregion

        #region Fail Listeners

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log($"OnCreateRoomFailed()\nreturnCode: {returnCode}\nmessage: {message}");
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log($"OnJoinRoomFailed()\nreturnCode: {returnCode}\nmessage: {message}");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log($"OnJoinRandomFailed()\nreturnCode: {returnCode}\nmessage: {message}");
        }

        public override void OnCustomAuthenticationFailed(string debugMessage)
        {
            Debug.Log($"OnCustomAuthenticationFailed()\ndebugMessage: {debugMessage}");
        }

        #endregion
        
    }
}