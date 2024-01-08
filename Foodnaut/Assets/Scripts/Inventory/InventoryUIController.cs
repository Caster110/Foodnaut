using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUIController : MonoBehaviour, IDragHandler
{
    [SerializeField] private GameObject UI_backpackPanel;
    [SerializeField] private CameraMovement playerCameraScript;
    [HideInInspector] public bool isOpened = false;
    private void Start()
    {
        UI_backpackPanel.SetActive(false);
    }
    private void Update()
    {
        TrySwitchInventory();
    }
    private void TrySwitchInventory()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
            return;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpened = !isOpened;
            if (isOpened)
            {
                UI_backpackPanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                playerCameraScript.enabled = false;
            }
            else
            {
                UI_backpackPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                playerCameraScript.enabled = true;
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(eventData);
    }
}
