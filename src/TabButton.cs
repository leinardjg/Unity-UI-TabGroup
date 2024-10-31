using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public Image background { get; set; }

    [SerializeField] private TabGroup tabGroup;
    [SerializeField] private UnityEvent onTabSelected;
    [SerializeField] private UnityEvent onTabDeselected;

    private void Awake()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    public void Select()
    {
        if (onTabSelected != null) onTabDeselected.Invoke();
    }

    public void Deselect()
    {
        if (onTabDeselected != null) onTabDeselected.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }
}
