using System.Collections.Generic;
using Photon.Realtime;
using PrefabFactorySystem;
using UnityEngine;

namespace PhotonPunExample
{
    [DisallowMultipleComponent]
    public class UIRoomFinderList : MonoBehaviour
    {
        
        [SerializeField] private UIRoomFinderListItem _pfUIRoomFinderListItem;

        private PrefabFactory<UIRoomFinderListItem> _listItemFactory;

        #region Initialization

        private bool _initialized = false;

        private void Initialize()
        {
            if (_initialized) return;
            _initialized = true;
            
            _listItemFactory = new PrefabFactory<UIRoomFinderListItem>(_pfUIRoomFinderListItem, transform);

            UIRoomFinderListItem[] existingItems = GetComponentsInChildren<UIRoomFinderListItem>();

            _listItemFactory.GetPool().AddRange(existingItems);
            _listItemFactory.PoolAll();
        }

        #endregion

        private void Awake()
        {
            Initialize();
        }

        public void OnDisable()
        {
            _listItemFactory.PoolAll();
        }

        public void Populate(List<RoomInfo> roomList)
        {
            Initialize();
            _listItemFactory.PoolAll();

            for (int i = 0; i < roomList.Count; i++)
            {
                RoomInfo roomInfo = roomList[i];
                UIRoomFinderListItem listItem = _listItemFactory.Get();
                listItem.SetRoom(roomInfo, i + 1);
            }
        }
        
    }
}
