using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private InventorySlot thisSlot;
    [SerializeField] private Image image;
    private Transform player;
    private Transform ImageTransform;

    private void Start()
    {
        player = GameObject.Find("CameraPos").transform;
        ImageTransform = GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (thisSlot.itemData == null)
            return;
        ImageTransform.position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (thisSlot.itemData == null) //|| eventData.pointerPress == null)
            return;
        // ƒелаем так чтобы нажати€ мышкой не игнорировали эту картинку
        GetComponent<Image>().raycastTarget = true;
        // ƒелаем наш DraggableObject ребенком InventoryPanel чтобы DraggableObject был над другими слотами инвентор€
        transform.SetParent(transform.parent.parent);
        //GetComponent<Canvas>().sortingOrder++;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //if (chosenSlot.itemData == null)
            //return;
        //GetComponentInChildren<Image>().raycastTarget = false;

        //ѕоставить DraggableObject обратно в свой старый слот
        transform.SetParent(thisSlot.transform);
        transform.position = thisSlot.transform.position;
        if (eventData.pointerCurrentRaycast.gameObject.tag != "Inventory" && thisSlot.itemData != null)
        {
            GameObject throwed = Instantiate(thisSlot.itemData.itemPrefab, player.position + player.forward, Quaternion.identity);
            NullifySlotData();
        }
        else if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>() != null)
        {
            ExchangeSlotData(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>());
        }
        //GetComponent<Canvas>().sortingOrder--;
    }
    void NullifySlotData()
    {
        thisSlot.itemData = null;
        image.sprite = null;
        image.color = new Color(1, 1, 1, 0);
    }
    void ExchangeSlotData(InventorySlot newSlot)
    {
        // ¬ременно храним данные newSlot в отдельных переменных
        ItemScriptableObject item = newSlot.itemData;
        bool isEmpty = newSlot.itemData != null;
        Image iconGO = newSlot.itemIcon;

        // «амен€ем значени€ newSlot на значени€ oldSlot
        newSlot.itemData = thisSlot.itemData;
        if (thisSlot.itemData != null)
        {
            newSlot.SetIcon(thisSlot.itemIcon.GetComponent<Image>().sprite);

        }
        else
        {
            newSlot.itemIcon.GetComponent<Image>().sprite = null;
        }

        //newSlot.isEmpty = oldSlot.isEmpty;

        // «амен€ем значени€ oldSlot на значени€ newSlot сохраненные в переменных
        thisSlot.itemData = item;
        if (isEmpty == false)
        {
            thisSlot.SetIcon(iconGO.GetComponent<Image>().sprite);
        }
        else
        {
            thisSlot.itemIcon.GetComponent<Image>().sprite = null;
        }

        //chosenSlot.isEmpty = isEmpty;
    }
}
