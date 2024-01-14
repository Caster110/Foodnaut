using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RaycastDetector : MonoBehaviour
{
    [SerializeField] private float reachDistance = 2.5f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private UIController inventoryUI;
    [SerializeField] private CrosshairUIController crosshairUIController;
    private Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
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
        if (inventoryUI.IsAnyClosableUIOpened())
            return null;
        bool isInteractableObject = false;
        detectedObjectType = null;

        Ray ray = playerCamera.ScreenPointToRay(screenCenter);
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
