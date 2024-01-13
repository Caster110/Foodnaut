using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
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

    }
    private void Update()
    {
        
    }
}
