using UnityEngine;
using UnityEngine.EventSystems;

namespace PhotonPunExample
{
    [DisallowMultipleComponent]
    public class UIConnectServerButton : MonoBehaviour, IPointerClickHandler
    {
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            NetworkManager.instance.ConnectToServer();
        }
    }
}
