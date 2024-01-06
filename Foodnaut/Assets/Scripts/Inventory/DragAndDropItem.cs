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
        ImageTransform = image.transform;
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
        //GetComponent<Image>().raycastTarget = false;
        transform.SetParent(transform.parent.parent);
        //GetComponent<Canvas>().sortingOrder++;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //GetComponentInChildren<Image>().raycastTarget = true;
        if (thisSlot.itemData == null)
            return;
        transform.SetParent(thisSlot.transform);
        transform.position = thisSlot.transform.position;
        bool targetSlotIsEmpty;
        InventorySlot targetSlot;
        GameObject targetSlotObject = eventData.pointerCurrentRaycast.gameObject;
        if(targetSlotObject)
            targetSlot = targetSlotObject.transform.parent.GetComponent<InventorySlot>();
        else
            targetSlot = null;
            
        if (targetSlotObject.transform.tag != "Slot" && targetSlotObject.transform.tag != "Inventory")
        {
            Instantiate(thisSlot.itemData.itemPrefab, player.position + player.forward, Quaternion.identity);
            NullifySlotData();
            Debug.Log("7");
        }
        else if (targetSlotObject.tag == "Inventory")
        {
            Debug.Log("6");
            return;
        }
        else if (targetSlotIsEmpty = targetSlot.itemData == null)
        {
            Debug.Log("5");
            ExchangeSlotData(targetSlot, targetSlotIsEmpty);
        }
        else
        {
            Debug.Log("8");
            ExchangeSlotData(targetSlot, targetSlotIsEmpty);
        }
        //GetComponent<Canvas>().sortingOrder--;
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
            Debug.Log("4");
            targetSlot.itemData = thisSlot.itemData;
            targetSlot.SetIcon(image.sprite);

            thisSlot.itemData = null;
            image.sprite = null;
            image.color = new Color(1, 1, 1, 0);
        }
        else
        {
            Debug.Log("3");
            ItemScriptableObject tempItem = targetSlot.itemData;
            Image tempItemIcon = targetSlot.itemIcon;
            //Debug.Log(tempItemIcon.sprite.ToString());

            targetSlot.itemData = thisSlot.itemData;
            targetSlot.SetIcon(image.sprite);
            thisSlot.itemData = tempItem;

            thisSlot.itemIcon = tempItemIcon;
            //thisSlot.SetIcon(tempItemIcon.sprite); //problem
            //Debug.Log(thisSlot.itemIcon.sprite.ToString());
        }
    }
}
