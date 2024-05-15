using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public enum AccessLevel
    {
        Both, // Класть и забирать предметы
        OnlyTake, // Только забирать предметы
        OnlyPut // Только класть предметы
    }

    [SerializeField] private Image itemIconImage;
    [SerializeField] private RectTransform iconRectTransform;
    [SerializeField] private AccessLevel accessLevel = AccessLevel.Both; // Уровень доступа по умолчанию

    private ItemScriptableObject itemData;
    public bool isEmpty => itemData == null;
    public ItemScriptableObject GetData() { return itemData; }
    public AccessLevel SlotAccessLevel => accessLevel; // Публичный геттер для уровня доступа

    public void SetData(ItemScriptableObject itemInfo)
    {
        /*
        if (accessLevel == AccessLevel.OnlyTake)
        {
            // Не разрешаем класть предметы, если уровень доступа только на забирание предметов
            return;
        }
        */

        itemData = itemInfo;
        
        SetImage();
    }
    
    public void Clear()
    {
        
        if (accessLevel == AccessLevel.OnlyPut)
        {
            return;
        }

        itemIconImage.sprite = null;
        itemIconImage.color = new Color(1, 1, 1, 0);
        itemData = null;
    }

    private void SetImage()
    {
        itemIconImage.color = new Color(1, 1, 1, 255);
        itemIconImage.sprite = itemData.icon;
        iconRectTransform.anchorMin = Vector2.zero;
        iconRectTransform.anchorMax = Vector2.one;
    }
}