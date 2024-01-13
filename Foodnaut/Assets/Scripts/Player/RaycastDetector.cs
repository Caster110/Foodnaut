using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RaycastDetector : MonoBehaviour
{
    [SerializeField] private float reachDistance = 2.5f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private CrosshairUIController crosshairUIController;
    private GameObject detectedObject;
    private string detectedObjectType = null;
    private readonly string[] interactableTypes = {"CraftStation", "CollectibleItem"};
    public GameObject GetDetectedObjectWithTag(string tagOfRequestedObject) 
    {
        if (detectedObjectType == tagOfRequestedObject)
            return detectedObject;
        else
            return null;
    }
    private void Update()
    {
        detectedObject = TryDetect();
    }
    private GameObject TryDetect()
    {
        bool isInteractableObject = false;
        detectedObjectType = null;
        if (inventoryUI.isOpened)
            return null;

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
         
        if (Physics.Raycast(ray, out hit, reachDistance))
        {
            Transform target = hit.transform;
            foreach (string i in interactableTypes)
                if (isInteractableObject = target.tag == i)
                {
                    detectedObjectType = target.tag;
                    crosshairUIController.TryChangeColor(isInteractableObject);
                    return target.gameObject;
                }
        }
        crosshairUIController.TryChangeColor(isInteractableObject);
        return null;
    }
}
