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
                Invoke(nameof(SpawnPlayer), 1f);
        }

        private void SpawnPlayer()
        {
            Transform spawnPoint = GetRandomSpawnPoint();

            // Parameters passed with instantiation.
            object[] param = new object[1];
            param[0] = $"{_pfPlayer.name} ({PhotonNetwork.LocalPlayer.ActorNumber})";

            PhotonNetwork.Instantiate(
                _pfPlayer.name,
                spawnPoint.position,
                spawnPoint.rotation,
                0,
                param);
        }
        
    }
}
