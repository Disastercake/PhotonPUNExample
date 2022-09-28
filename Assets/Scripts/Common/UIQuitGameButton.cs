using UnityEngine;
using UnityEngine.EventSystems;

namespace PhotonPunExample
{
    [DisallowMultipleComponent]
    public class UIQuitGameButton : MonoBehaviour, IPointerClickHandler
    {
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Quitting game.");
            Application.Quit();
        }
    }
}
