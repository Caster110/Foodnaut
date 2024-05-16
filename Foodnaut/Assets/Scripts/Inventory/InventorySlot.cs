using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Для работы с событиями указателя

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum AccessLevel
    {
        Both, // Класть и забирать предметы
        OnlyTake, // Только забирать предметы
        OnlyPut // Только класть предметы
    }

    [SerializeField] private Image itemIconImage;
    [SerializeField] private RectTransform iconRectTransform;
    [SerializeField] private GameObject tooltipBackground;
    private Text itemTooltip; 
    [SerializeField] private AccessLevel accessLevel = AccessLevel.Both; // Уровень доступа по умолчанию

    private ItemScriptableObject itemData;
    public bool isEmpty => itemData == null;
    public ItemScriptableObject GetData() { return itemData; }
    public AccessLevel SlotAccessLevel => accessLevel; // Публичный геттер для уровня доступа

    private bool isPointerOver = false; // Флаг для отслеживания, находится ли указатель мыши над элементом

    private void Start()
    {
        itemTooltip = tooltipBackground.GetComponentInChildren<Text>();
        if (itemTooltip != null)
        {
            tooltipBackground.SetActive(false);

            if (tooltipBackground.GetComponent<Image>())
            {
                tooltipBackground.GetComponent<Image>().raycastTarget = false;
            }
            else if (tooltipBackground.GetComponent<Text>())
            {
                tooltipBackground.GetComponent<Text>().raycastTarget = false;
            }
            else if (tooltipBackground.GetComponent<CanvasGroup>())
            {
                tooltipBackground.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }

    private void Update()
    {
        if (isPointerOver && tooltipBackground != null && !isEmpty)
        {
            Vector3 newPosition = Input.mousePosition;
            // Поскольку теперь мы перемещаем весь фон подсказки, вам может понадобиться настроить смещение
            newPosition.x += tooltipBackground.GetComponent<RectTransform>().rect.width * 0.5f; // Смещаем подсказку вправо от курсора
            newPosition.y -= tooltipBackground.GetComponent<RectTransform>().rect.height * 2.4f; // Смещаем подсказку вниз от курсора
            
            // Преобразование экранных координат в мировые для Canvas
            tooltipBackground.GetComponent<RectTransform>().position = newPosition;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
        if (!isEmpty && itemTooltip != null)
        {
            itemTooltip.text = itemData.name; // Устанавливаем текст подсказки
            tooltipBackground.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
        if (itemTooltip != null)
            tooltipBackground.SetActive(false);
    }

    public void SetData(ItemScriptableObject itemInfo)
    {
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
        itemIconImage.color = new Color(1, 1, 1, 1); // Исправлено значение прозрачности
        itemIconImage.sprite = itemData.icon;
        iconRectTransform.anchorMin = Vector2.zero;
        iconRectTransform.anchorMax = Vector2.one;
    }
}