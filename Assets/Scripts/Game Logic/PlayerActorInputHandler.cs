using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PhotonPunExample
{
    /// <summary>
    /// Handles receiving player input and routing it to the correct components.
    /// </summary>
    [DisallowMultipleComponent]
    public class PlayerActorInputHandler : MonoBehaviour
    {
        private ActorMotor _actorMotor;
        private Vector3 _moveVector;

        #region Initialization

        private bool _initialized = false;

        private void Initialize()
        {
            if (_initialized) return;
            _initialized = true;

            _actorMotor = GetComponent<ActorMotor>();
        }

        #endregion

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            _actorMotor.Move(_moveVector);
        }

        public void OnMoveInput(InputAction.CallbackContext value)
        {
            if (!enabled)
            {
                Debug.Log("OnMoveInput: " + enabled);
                return;
            }
            
            Initialize();
            
            var moveVector2d = value.ReadValue<Vector2>();
            
            if (moveVector2d.magnitude > 1f)
                moveVector2d.Normalize();

            _moveVector = new Vector3(moveVector2d.x, 0, moveVector2d.y);
        }
    }
}
