using System.Collections.Generic;
using UnityEngine;

public class ItemPlayerStorage : MonoBehaviour
{
    [SerializeField] private Transform inventoryPanel;
    [SerializeField] private Transform hotBarPanel;
    [SerializeField] private RaycastDetector raycastDetector;
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
    public void TryAddItem(GameObject target)
    {
        foreach (InventorySlot slot in slots)
            if(slot.isEmpty)
            {
                slot.SetData(target.GetComponent<Item>().data);
                Destroy(target);
                return;
            }
    }
}
