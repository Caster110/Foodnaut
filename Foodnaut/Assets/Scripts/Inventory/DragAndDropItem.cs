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

        if (targetObject)
        {
            if (targetObject.tag == "ItemPlacement")
                ExchangeSlotData(targetObject.transform.parent.GetComponent<InventorySlot>());
        }
        else
        {
            ThrowItem();
        }
        slotVisualComponent.OnTakenItem();
    }
    private void ThrowItem()
    {
        Instantiate(thisSlot.GetData().itemPrefab, playerFace.position + playerFace.forward, Quaternion.identity);
        thisSlot.Clear();
    }
    private void ExchangeSlotData(InventorySlot targetSlot)
    {
        if (targetSlot.isEmpty)
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
