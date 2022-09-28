using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using PrefabFactorySystem;
using UnityEngine;

namespace PhotonPunExample
{
    [DisallowMultipleComponent]
    public class UIRoomPlayerList : MonoBehaviourPunCallbacks
    {
        
        [SerializeField] private UIRoomPlayerListItem _pfUIRoomPlayerListItem;

        private PrefabFactory<UIRoomPlayerListItem> _listItemFactory;

        private void Awake()
        {
            _listItemFactory = new PrefabFactory<UIRoomPlayerListItem>(_pfUIRoomPlayerListItem, transform);

            UIRoomPlayerListItem[] existingItems = GetComponentsInChildren<UIRoomPlayerListItem>();

            _listItemFactory.GetPool().AddRange(existingItems);
            _listItemFactory.PoolAll();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            Populate();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _listItemFactory.PoolAll();
        }

        private void Populate()
        {
            _listItemFactory.PoolAll();
            
            Dictionary<int, Player> playerDictionary = PhotonNetwork.CurrentRoom.Players;

            foreach (KeyValuePair<int,Player> keyValuePair in playerDictionary)
            {
                UIRoomPlayerListItem listItem = _listItemFactory.Get();
                listItem.SetPlayer(keyValuePair.Value, keyValuePair.Key);
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("OnPlayerEnteredRoom");
            UIRoomPlayerListItem listItem = _listItemFactory.Get();
            listItem.SetPlayer(newPlayer, newPlayer.ActorNumber);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log("OnPlayerLeftRoom");
            foreach (UIRoomPlayerListItem listItem in _listItemFactory.GetPool())
            {
                if (Equals(listItem.Player, otherPlayer))
                {
                    listItem.OnReturnToPool();
                }
            }
        }
        
    }
}
