using System;
using Photon.Pun;
using UnityEngine;

namespace PhotonPunExample
{
    [DisallowMultipleComponent]
    public class SceneHandler : MonoBehaviour, IPunInstantiateMagicCallback
    {
        private void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        private void OnDisable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info)
        {
            Debug.Log("OnPhotonInstantiate()");
            
            object[] instantiationData = info.photonView.InstantiationData;

            if (instantiationData.Length <= 0)
                return;

            info.photonView.gameObject.name = instantiationData[0] as string ?? $"NO_NAME ({info.photonView.ViewID})";
        }
    }
}
