using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Transform inventoryPanel;
    [SerializeField] private Transform hotBarPanel;
    [SerializeField] private RaycastDetector raycastDetector;
    private GameObject collectibleItem;
    private List<InventorySlot> slots = new List<InventorySlot>();
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
        if (collectibleItem = raycastDetector.GetDetectedObjectWithTag("CollectibleItem"))
            TryCollect(collectibleItem);
    }
    private void TryCollect(GameObject target)
    {
        if (Input.GetKeyDown(KeyCode.F))
            if (AddItem(target.GetComponent<Item>().data))
                Destroy(target);
    }
    private bool AddItem(ItemScriptableObject itemData)
    {
        bool itemWasAdded = false;
        foreach (InventorySlot slot in slots)
            if(slot.isEmpty)
            {
                slot.SetData(itemData);
                itemWasAdded = true;
                break;
            }
        return itemWasAdded;
    }
}
