using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private InventoryUIController inventoryUIController;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform inventoryPanel;
    [SerializeField] private Transform hotBarPanel;
    [SerializeField] private GameObject UI_crosshair; // не тут/рассмотреть другие варианты
    [SerializeField] private float reachDistance = 2.5f;
    private List<InventorySlot> slots = new List<InventorySlot>(); //узнать можно ли приват

    private void Start()
    {
        InitializeSlots();
    }
    private void InitializeSlots()
    {
        int slotsCount = hotBarPanel.childCount;
        for (int i = 0; i < slotsCount; i++)
            slots.Add(hotBarPanel.GetChild(i).GetComponent<InventorySlot>());

        slotsCount = inventoryPanel.childCount;
        for (int i = 0; i < slotsCount; i++)
            slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
    }
    private void Update()
    {
        GameObject collectibleElement;
        if (inventoryUIController.isOpened)
        {
            UI_crosshair.SetActive(false);
            return;
        }
        else if (collectibleElement = CheckObjectsToCollect())
        {
            TryCollect(collectibleElement);
            UI_crosshair.SetActive(true);
        }
    }
    private GameObject CheckObjectsToCollect()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, reachDistance))
        {
            if (hit.transform.tag == "Collectible Item")
                return hit.transform.gameObject;
        }
        UI_crosshair.SetActive(false);
        return null;
    }
    private void TryCollect(GameObject target)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (AddItem(target.GetComponent<Item>().data))
                Destroy(target);
        }
    }
    private bool AddItem(ItemScriptableObject itemData)
    {
        bool itemWasAdded = false;
        foreach (InventorySlot slot in slots)
        {
            if(slot.isEmpty)
            {
                slot.SetData(itemData);
                itemWasAdded = true;
                break;
            }
        }
        return itemWasAdded;
    }
}
