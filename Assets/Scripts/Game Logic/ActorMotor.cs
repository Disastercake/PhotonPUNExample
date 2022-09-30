using System;
using UnityEngine;

namespace PhotonPunExample
{
    [DisallowMultipleComponent]
    public class ActorMotor : MonoBehaviour
    {
        [SerializeField] private Vector3 _gravity = new Vector3(0, -10f, 0);
        [SerializeField] private float _speed;
        private CharacterController _cc;

        private Vector3 _moveVector;

        #region Initialization

        private bool _initialized = false;

        private void Initialize()
        {
            if (_initialized) return;
            _initialized = true;
            
            _cc = GetComponent<CharacterController>();
        }

        #endregion

        private void Awake()
        {
            Initialize();
        }

        public void Move(Vector3 moveVector)
        {
            _moveVector = moveVector * _speed;
        }

        private void FixedUpdate()
        {
            Vector3 finalMove = (_moveVector + _gravity) * Time.deltaTime;
            _moveVector = Vector3.zero;
            _cc.Move(finalMove);
            Debug.Log("FixedUpdate: " + finalMove);
        }
    }
}
