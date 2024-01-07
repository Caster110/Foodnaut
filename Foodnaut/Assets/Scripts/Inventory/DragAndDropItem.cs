using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private InventorySlot thisSlot;
    [SerializeField] private Image itemPlacementImage;
    private Image itemImage;
    private Transform player;
    private RectTransform itemImageTransform;


    private void Start()
    {
        player = GameObject.Find("CameraPos").transform;
        itemImage = thisSlot.GetIcon();
        itemImageTransform = itemImage.GetComponent<RectTransform>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (thisSlot.isEmpty)
            return;
        itemImage.transform.SetParent(transform.parent.parent.parent);
        itemPlacementImage.raycastTarget = false;
        Cursor.visible = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (thisSlot.isEmpty)
            return;
        itemImageTransform.position += new Vector3(eventData.delta.x, eventData.delta.y);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (thisSlot.isEmpty)
            return;
        Cursor.visible = true; //сделать отдельную часть с подсветкой, видимостью мышки тд визуалом через наблюдателя, event ил что там было
        itemPlacementImage.raycastTarget = true;
        itemImage.transform.SetParent(thisSlot.transform);
        itemImage.transform.position = thisSlot.transform.position;
        GameObject targetObject = eventData.pointerCurrentRaycast.gameObject;
        InventorySlot targetSlot;

        if (targetObject)
        {
            if (targetObject.tag == "Slot Placement")
            {
                targetSlot = targetObject.transform.parent.GetComponent<InventorySlot>();
                if (targetSlot.isEmpty)
                    ExchangeSlotData(targetSlot, targetSlot.isEmpty);
                else
                    ExchangeSlotData(targetSlot, targetSlot.isEmpty);
            }
            else if (targetObject.tag == "Inventrory")
            {
                return;
            }
        }
        else
        {
            Instantiate(thisSlot.GetData().itemPrefab, player.position + player.forward, Quaternion.identity);
            thisSlot.Clear();
        }
    }
    private void ExchangeSlotData(InventorySlot targetSlot, bool targetSlotIsEmpty)
    {
        if (targetSlotIsEmpty)
        {
            targetSlot.Set(itemImage.sprite, thisSlot.GetData());
            thisSlot.Clear();
        }
        else
        {
            ItemScriptableObject tempItemData = targetSlot.GetData();
            Sprite tempItemIcon = targetSlot.GetIcon().sprite;
            targetSlot.Set(itemImage.sprite, thisSlot.GetData());
            thisSlot.Set(tempItemIcon, tempItemData);
            tempItemData = null;
            tempItemIcon = null;
        }
    }
}
