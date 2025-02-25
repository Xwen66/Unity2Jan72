using UnityEngine;
using UnityEngine.EventSystems;

public class UIAutoSelect : MonoBehaviour, IPointerEnterHandler     // we're implementing the pointer enter interface
{
    [SerializeField] private bool _firstSelected = false;

    // called every time mouse moves over UI element
    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    private void OnEnable()
    {
        if (_firstSelected) EventSystem.current.SetSelectedGameObject(gameObject);
    }
}