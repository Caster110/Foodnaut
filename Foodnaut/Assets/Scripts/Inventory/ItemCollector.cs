using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>(); //������ ����� �� ������
    [SerializeField] private Transform inventoryPanel;
    [SerializeField] private Transform hotBarPanel;
    [SerializeField] private GameObject UI_inventoryPanel;
    [SerializeField] private GameObject UI_crosshair; // �� ���/����������� ������ ��������
    [SerializeField] private float reachDistance = 2.5f;
    [SerializeField] private PlayerCamera playerCameraScript;
    [SerializeField] private Camera playerCamera;
    private bool isOpened = false;
    void Start()
    {
        int slotsCount = hotBarPanel.childCount;
        for (int i = 0; i < slotsCount; i++)
        {
            slots.Add(hotBarPanel.GetChild(i).GetComponent<InventorySlot>());
        }

        slotsCount = inventoryPanel.childCount;
        for (int i = 0; i < slotsCount; i++)
        {
            slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
        }
        UI_inventoryPanel.SetActive(false);
    }

    void Update()
    {
        TrySwitchInventory();   

        GameObject collectibleElement;
        if (isOpened)
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

    private void TrySwitchInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpened = !isOpened;
            if (isOpened)
            {
                UI_inventoryPanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                playerCameraScript.enabled = false;
            }
            else
            {
                UI_inventoryPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                playerCameraScript.enabled = true;
            }
        }
    }
    private GameObject CheckObjectsToCollect()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, reachDistance))
        {
            if (hit.transform.tag == "Collectible Item")
            {
                Debug.DrawRay(ray.origin, ray.direction * reachDistance, Color.blue);
                return hit.transform.gameObject;
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * reachDistance, Color.red);
        }
        UI_crosshair.SetActive(false);
        return null;
    }
    private void TryCollect(GameObject target)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (AddItem(target.GetComponent<Item>().item))
                Destroy(target);
        }
    }
    private bool AddItem(ItemScriptableObject item)
    {
        bool itemWasAdded = false;
        foreach (InventorySlot slot in slots)
        {
            if(slot.isEmpty)
            {
                slot.Set(item.icon, item);
                itemWasAdded = true;
                break;
            }
        }
        return itemWasAdded;
    }
}
