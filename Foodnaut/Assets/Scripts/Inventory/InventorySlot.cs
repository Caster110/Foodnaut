using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private GameObject itemIcon;
    [SerializeField] private Image itemIconPicture;
    private ItemScriptableObject itemData;
    public bool isEmpty => itemData == null;
    public void Set(Sprite iconSprite, ItemScriptableObject info)
    {
        itemIconPicture.color = new Color(1,1,1,255);
        itemIconPicture.sprite = iconSprite;
        RectTransform iconRectTransform = itemIconPicture.GetComponent<RectTransform>();
        iconRectTransform.anchorMin = Vector2.zero;
        iconRectTransform.anchorMax = Vector2.one;
        itemData = info;
    }
    public void Clear()
    {
        itemIconPicture.color = new Color(1, 1, 1, 0);
        itemIconPicture.sprite = null;
        itemData = null;
    }
    public ItemScriptableObject GetData() { return itemData; }
    public Image GetIcon() { return itemIconPicture; }
}
