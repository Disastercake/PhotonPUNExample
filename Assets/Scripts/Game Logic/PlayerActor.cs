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
        private ActorMotor _actorMotor;
        private PlayerActorInputHandler _inputHandler;
        private CharacterController _cc;

        #region Initialization

        private bool _initialized = false;

        private void Initialize()
        {
            if (_initialized) return;
            _initialized = true;
            _gameObject = gameObject;
            _actorMotor = GetComponent<ActorMotor>();
            _inputHandler = GetComponent<PlayerActorInputHandler>();
            _cc = GetComponent<CharacterController>();
        }

        #endregion
        
        void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info)
        {
            Initialize();
            
            // e.g. Store this GameObject as this player's character in Player.TagObject
            info.Sender.TagObject = _gameObject;
            _gameObject.name = $"{_gameObject.name} (Player: {info.Sender.ActorNumber})";

            bool isLocal = info.Sender.IsLocal;
            
            // Disable the input if not the local player.
            _inputHandler.enabled = isLocal;
            _cc.enabled = isLocal;
        }
        
    }
}
