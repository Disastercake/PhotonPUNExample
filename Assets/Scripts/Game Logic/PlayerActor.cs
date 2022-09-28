using Photon.Pun;
using UnityEngine;

namespace PhotonPunExample
{
    /// <summary>
    /// Note: IPunInstantiateMagicCallback is called on the exact object PhotonNetwork.Instantiate() created.
    /// </summary>
    [DisallowMultipleComponent]
    public class PlayerActor : MonoBehaviour, IPunInstantiateMagicCallback
    {
        
        private GameObject _gameObject;

        #region Initialization

        private bool _initialized = false;

        private void Initialize()
        {
            if (_initialized) return;
            _initialized = true;
            _gameObject = gameObject;
        }

        #endregion
        
        void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info)
        {
            Initialize();
            
            // e.g. Store this GameObject as this player's character in Player.TagObject
            info.Sender.TagObject = _gameObject;
            _gameObject.name = $"{_gameObject.name} (Player: {info.Sender.ActorNumber})";
        }
        
    }
}
