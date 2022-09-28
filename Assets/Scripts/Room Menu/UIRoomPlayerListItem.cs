using System.Text;
using Common;
using Photon.Realtime;
using UnityEngine;

namespace PhotonPunExample
{
    [DisallowMultipleComponent]
    public class UIRoomPlayerListItem : PoolableGameObject
    {
        
        [SerializeField] private TMPro.TextMeshProUGUI _nameTextComp;

        public int Order { get; private set; }
        public Player Player { get; private set; }
        public bool IsMasterClient { get; private set; }

        private void OnEnable()
        {
            NetworkManager.instance.MasterClientSwitched += OnMasterClientSwitched;
        }

        private void OnDisable()
        {
            NetworkManager.instance.MasterClientSwitched -= OnMasterClientSwitched;
        }

        private void OnMasterClientSwitched(Player newMasterClient)
        {
            // No change needed if MasterClient state didn't change for this Player.
            if (IsMasterClient == Player.IsMasterClient)
                return;

            IsMasterClient = Player.IsMasterClient;
            RefreshVisuals();
        }

        public void SetPlayer(Player player, int order)
        {
            Order = order;
            Player = player;
            IsMasterClient = Player.IsMasterClient;
            RefreshVisuals();
        }

        private void RefreshVisuals()
        {
            var strbldr = new StringBuilder();

            // If the player doesn't have a nickname set, then use their ActorNumber.
            if (!string.IsNullOrEmpty(Player.NickName))
                strbldr.Append(Player.NickName);
            else
                strbldr.Append("Player ").Append(Player.ActorNumber);
            
            if (IsMasterClient)
                strbldr.Append(" (*)");
            
            _nameTextComp.text = strbldr.ToString();
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
