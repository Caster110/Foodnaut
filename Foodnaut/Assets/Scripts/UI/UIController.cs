using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject UIBackpack_Panel;
    [SerializeField] private GameObject crosshair;
    private InteractableUI currentInteractableUI;
    private void Start()
    {
        UIBackpack_Panel.SetActive(false);
    }
    public void OpenInventory()
    {
        UIBackpack_Panel.SetActive(true);
        crosshair.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void CloseUI()
    {
        UIBackpack_Panel.SetActive(false);
        if (currentInteractableUI)
        {
            currentInteractableUI.Off();
            currentInteractableUI = null;
        }
        crosshair.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void OpenInteractableStation(GameObject target)
    {
        UIBackpack_Panel.SetActive(true);
        currentInteractableUI = target.GetComponent<InteractableUI>();
        currentInteractableUI.On(target.transform.GetChild(0));
        crosshair.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public bool IsAnyClosableUIOpened() 
    { 
        if (UIBackpack_Panel.activeSelf)
            return true;
        /*else if (lastUICraftStation_Panel.activeSelf)
            return true;*/ // необяз тк инвентарь открывается с крафтСтейшн
        else 
            return false;
    }
}
