using UnityEngine;

public class InputChecker : MonoBehaviour
{
    [SerializeField] private UIController UIController;
    private bool isMousePressed => Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2);
    public bool CanInteract()
    {
        if (UIController.IsAnyClosableUIOpened())
            return false;
        else if (!Input.GetKeyDown(KeyCode.F))
            return false;
        else
            return true;
    }
    public bool CanOpenInventory()
    {
        if (UIController.IsAnyClosableUIOpened())
            return false;
        else if (!Input.GetKeyDown(KeyCode.Tab))
            return false;
        else
            return true;
    }
    public bool CanCloseUI()
    {
        if(isMousePressed)
            return false;
        else if (!UIController.IsAnyClosableUIOpened())
            return false;
        else if (!Input.GetKeyDown(KeyCode.Tab) && !Input.GetKeyDown(KeyCode.Escape))
            return false;
        else
            return true;
    }
}
