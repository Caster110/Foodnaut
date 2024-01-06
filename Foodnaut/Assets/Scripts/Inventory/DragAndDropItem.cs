using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private InventorySlot thisSlot;
    [SerializeField] private Image image;
    private Transform player;
    private RectTransform ImageTransform;

    private void Start()
    {
        player = GameObject.Find("CameraPos").transform;
        ImageTransform = image.gameObject.GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (thisSlot.itemData == null)
            return;
        ImageTransform.position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (thisSlot.itemData == null)
            return;
        transform.SetParent(transform.parent.parent.parent);
        image.raycastTarget = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (thisSlot.itemData == null)
            return;
        image.raycastTarget = true;
        transform.SetParent(thisSlot.transform);
        transform.position = thisSlot.transform.position;
        bool targetSlotIsEmpty;
        GameObject targetObject = eventData.pointerCurrentRaycast.gameObject;
        InventorySlot targetSlot;

        if (targetObject)
        {
            if (targetObject.tag == "Slot")
            {
                targetSlot = targetObject.transform.parent.GetComponent<InventorySlot>();
                if (targetSlotIsEmpty = targetSlot.itemData == null)
                    ExchangeSlotData(targetSlot, targetSlotIsEmpty);
                else
                    ExchangeSlotData(targetSlot, targetSlotIsEmpty);
            }
            else
            { 
                return;
            }
        }
        else
        {
            Instantiate(thisSlot.itemData.itemPrefab, player.position + player.forward, Quaternion.identity);
            NullifySlotData();
        }
    }
    void NullifySlotData()
    {
        thisSlot.itemData = null;
        image.sprite = null;
        image.color = new Color(1, 1, 1, 0);
    }
    private void ExchangeSlotData(InventorySlot targetSlot, bool targetSlotIsEmpty)
    {
        if (targetSlotIsEmpty)
        {
            targetSlot.itemData = thisSlot.itemData;
            targetSlot.SetIcon(image.sprite);

            thisSlot.itemData = null;
            image.sprite = null;
            image.color = new Color(1, 1, 1, 0);
        }
        else
        {
            ItemScriptableObject tempItem = targetSlot.itemData;
            Sprite tempItemIcon = targetSlot.itemIcon.sprite;
            targetSlot.itemData = thisSlot.itemData;
            targetSlot.SetIcon(image.sprite);
            thisSlot.itemData = tempItem;
            thisSlot.SetIcon(tempItemIcon);
            tempItem = null;
            tempItemIcon = null;
        }
    }
}
