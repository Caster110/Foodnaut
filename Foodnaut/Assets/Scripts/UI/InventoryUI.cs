using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject UI_backpackPanel;
    [SerializeField] private CameraMovement cameraMovement;
    [HideInInspector] public bool isOpened = false;
    private bool isTabKeyPressed => Input.GetKeyDown(KeyCode.Tab);
    private bool isFKeyPresed => Input.GetKeyDown(KeyCode.F);
    private bool isEscKeyPresed => Input.GetKeyDown(KeyCode.Escape);
    private bool isMousePressed => Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2);
    private CraftStationUI currentStationUI;
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
        if (isMousePressed)
            return;

        if (isTabKeyPressed)
        {
            isOpened = !isOpened;
            if (isOpened)
            {
                UI_backpackPanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                cameraMovement.enabled = false;
            }
            else
            {
                UI_backpackPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                cameraMovement.enabled = true;
            }
        }
    }
}
