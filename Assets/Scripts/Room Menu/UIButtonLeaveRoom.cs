using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PhotonPunExample
{
    [DisallowMultipleComponent]
    public class UIButtonLeaveRoom : MonoBehaviour, IPointerClickHandler
    {
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Leaving Room...");
            PhotonNetwork.LeaveRoom();
        }
    }
}
