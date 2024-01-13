using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotVisualComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image slotImage;
    private Color slotDefaultColor;
    private bool isColorChanged = false;
    private void Start()
    {
        slotDefaultColor = slotImage.color;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        slotImage.color = Color.grey;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slotImage.color = slotDefaultColor;
    }
    public void OnTakenItem()
    {
        ChangeColor();
        Cursor.visible = !Cursor.visible;
    }
    private void ChangeColor()
    {
        isColorChanged = !isColorChanged;
        if (isColorChanged)
            slotImage.color = Color.grey;
        else
            slotImage.color = slotDefaultColor;
    }
    private void OnDisable()
    {
        slotImage.color = slotDefaultColor;
        isColorChanged = false;
    }
}
