using UnityEngine;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler
{
    // This class is for the extra features not in the Unity Button script
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.FindObjectOfType<MenuAudioManager>().Play("Hover");
    }
}