using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemScriptableObject itemData;
    private Image itemIcon;

    private void Start()
    {
        itemIcon = transform.GetChild(0).gameObject.GetComponent<Image>();
    }
    public void SetIcon(Sprite icon)
    {
        itemIcon.color = new Color(1,1,1,255);
        itemIcon.sprite = icon;
    }
}
