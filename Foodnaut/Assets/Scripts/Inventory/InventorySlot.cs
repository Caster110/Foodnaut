using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemScriptableObject itemData;
    public Image itemIcon;
    public void SetIcon(Sprite icon)
    {
        itemIcon.color = new Color(1,1,1,255);
        itemIcon.sprite = icon;
    }
}
