using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemIconImage;
    [SerializeField] private RectTransform iconRectTransform;
    private ItemScriptableObject itemData;
    public bool isEmpty => itemData == null;
    public ItemScriptableObject GetData() { return itemData; }
    public void SetData(ItemScriptableObject itemInfo)
    {
        itemData = itemInfo;
        SetImage();
    }
    private void SetImage()
    {
        itemIconImage.color = new Color(1,1,1,255);
        itemIconImage.sprite = itemData.icon;
        iconRectTransform.anchorMin = Vector2.zero;
        iconRectTransform.anchorMax = Vector2.one;
    }
    public void Clear()
    {
        itemIconImage.sprite = null;
        itemIconImage.color = new Color(1, 1, 1, 0);
        itemData = null;
    }
}
