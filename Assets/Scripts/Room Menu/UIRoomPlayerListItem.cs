using Common;
using Photon.Realtime;
using PrefabFactorySystem;
using UnityEngine;

namespace PhotonPunExample
{
    [DisallowMultipleComponent]
    public class UIRoomPlayerListItem : PoolableGameObject
    {
        
        [SerializeField] private TMPro.TextMeshProUGUI _nameTextComp;

        public int Order { get; private set; }
        public Player Player { get; private set; }

        public void SetPlayer(Player player, int order)
        {
            Order = order;
            Player = player;
            RefreshVisuals();
        }

        private void RefreshVisuals()
        {
            _nameTextComp.text = Player.NickName;
            transform.SetSiblingIndex(Order);
        }

        protected override void SetStateToPooled()
        {
            Player = null;
            Order = 0;
            base.SetStateToPooled();
        }

    }
}
