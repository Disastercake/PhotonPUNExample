using System;
using Photon.Pun;
using UnityEngine;

namespace PhotonPunExample
{
    [DisallowMultipleComponent]
    public class SpawnManager : MonoBehaviour
    {
        
        #region Singleton

        public static SpawnManager Instance { get; private set; } = null;

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        [SerializeField] private GameObject _pfPlayer;
        [SerializeField] private Transform[] _spawnPoints;

        public Transform GetRandomSpawnPoint()
        {
            return _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)];
        }
        
        private void Start()
        {
            if (PhotonNetwork.IsConnected)
                SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            Transform spawnPoint = GetRandomSpawnPoint();

            PhotonNetwork.Instantiate(_pfPlayer.name, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
