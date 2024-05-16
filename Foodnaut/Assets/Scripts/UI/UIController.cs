using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject UIBackpack_Panel;
    [SerializeField] private GameObject UICraftStation_Panel;
    [SerializeField] private GameObject UIBlender_Panel;
    [SerializeField] private GameObject UItooltip_Panel;
    [SerializeField] private GameObject crosshair;
    private InteractableUI currentInteractableUI;
    private void Start()
    {
        UIBackpack_Panel.SetActive(false);
        UICraftStation_Panel.SetActive(false);
        UIBlender_Panel.SetActive(false);
    }
    public void OpenInventory()
    {
        UIBackpack_Panel.SetActive(true);
        crosshair.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void OpenCraftStation()
    {
        UICraftStation_Panel.SetActive(true);
        crosshair.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void OpenBlender()
    {
        UIBlender_Panel.SetActive(true);
        crosshair.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void CloseUI(Transform playerCameraPosition)
    {
        UIBackpack_Panel.SetActive(false);
        UICraftStation_Panel.SetActive(false);
        UIBlender_Panel.SetActive(false);
        UItooltip_Panel.SetActive(false);
        if (currentInteractableUI)
        {
            currentInteractableUI.Off(playerCameraPosition);
            currentInteractableUI = null;
        }
        crosshair.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void OpenCraftStation(GameObject target)
    {
        UIBackpack_Panel.SetActive(true);
        UICraftStation_Panel.SetActive(true);
        currentInteractableUI = target.GetComponent<InteractableUI>();
        if (currentInteractableUI == null)
            Debug.Log("Крафтовая станция");
        crosshair.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void OpenBlender(GameObject target)
    {
        UIBackpack_Panel.SetActive(true);
        UIBlender_Panel.SetActive(true);
        currentInteractableUI = target.GetComponent<InteractableUI>();
        if (currentInteractableUI == null)
            Debug.Log("Блендер");
        crosshair.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public bool IsAnyClosableUIOpened() 
    { 
        if (UIBackpack_Panel.activeSelf)
            return true;
        /*else if (currentInteractableUI)
            return true;*/ // ������ �� ��������� ����������� � �����������
        else
            return false;
    }
}
