using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>(); //узнать можно ли приват
    [SerializeField] private Transform inventoryPanel;
    [SerializeField] private GameObject UI_inventoryPanel;
    [SerializeField] private float reachDistance = 2.5f;
    [SerializeField] private Camera viewDirection;
    private bool isOpened = false;
    void Start()
    {

        int slotsCount = inventoryPanel.childCount;
        for (int i = 0; i < slotsCount; i++)
        {
            slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
        }
        UI_inventoryPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpened = !isOpened;
            if (isOpened)
                UI_inventoryPanel.SetActive(true);
            else 
                UI_inventoryPanel.SetActive(false);
        }    

        Ray ray = viewDirection.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, reachDistance))
        {
            if(hit.transform.tag == "Collectible Item")
            {
            Debug.DrawRay(ray.origin, ray.direction * reachDistance, Color.blue);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (AddItem(hit.transform.gameObject.GetComponent<Item>().item))
                        Destroy(hit.transform.gameObject);
                }
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * reachDistance, Color.red);
        }
    }
    private bool AddItem(ItemScriptableObject item)
    {
        bool inventoryIsNotFull = false;
        foreach (InventorySlot slot in slots)
        {
            if(slot.itemData == null)
            {
                slot.itemData = item;
                slot.SetIcon(item.icon);
                inventoryIsNotFull = true;
                break;
            }
        }
        return inventoryIsNotFull;
    }
}
