using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private SlotVisualComponent slotVisualComponent;
    [SerializeField] private InventorySlot thisSlot;
    [SerializeField] private Image itemImage;
    [SerializeField] private RectTransform itemTransform;
    private Transform playerFace;
    private void Start()
    {
        playerFace = GameObject.Find("Camera").transform;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (thisSlot.isEmpty)
            return;
        itemImage.transform.SetParent(transform.parent.parent.parent);
        itemImage.raycastTarget = false;

        /*Vector3 toCenter = Input.mousePosition - itemImage.transform.position;
        itemTransform.position += toCenter;*/
        itemTransform.position = Input.mousePosition;
        slotVisualComponent.OnTakenItem();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (thisSlot.isEmpty)
            return;
        itemTransform.position += new Vector3(eventData.delta.x, eventData.delta.y);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (thisSlot.isEmpty)
            return;
        itemImage.raycastTarget = true;
        itemImage.transform.SetParent(thisSlot.transform);
        itemImage.transform.position = thisSlot.transform.position;
        GameObject targetObject = eventData.pointerCurrentRaycast.gameObject;
        InventorySlot targetSlot;

        if (targetObject)
        {
            if (targetObject.tag == "ItemPlacement")
            {
                targetSlot = targetObject.transform.parent.GetComponent<InventorySlot>();
                if (targetSlot.isEmpty)
                    ExchangeSlotData(targetSlot, targetSlot.isEmpty);
                else
                    ExchangeSlotData(targetSlot, targetSlot.isEmpty);
            }
        }
        else
            ThrowItem();
        slotVisualComponent.OnTakenItem();
    }
    private void ThrowItem()
    {
        Instantiate(thisSlot.GetData().itemPrefab, playerFace.position + playerFace.forward, Quaternion.identity);
        thisSlot.Clear();
    }
    private void ExchangeSlotData(InventorySlot targetSlot, bool targetSlotIsEmpty)
    {
        if (targetSlotIsEmpty)
        {
            targetSlot.SetData(thisSlot.GetData());
            thisSlot.Clear();
        }
        else
        {
            ItemScriptableObject tempItemData = targetSlot.GetData();
            targetSlot.SetData(thisSlot.GetData());
            thisSlot.SetData(tempItemData);
        }
    }
}
