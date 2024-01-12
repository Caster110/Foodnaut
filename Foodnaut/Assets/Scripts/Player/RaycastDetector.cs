using UnityEngine;

public class RaycastDetector : MonoBehaviour
{
    [SerializeField] private float reachDistance = 2.5f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private UIController inventoryUIController;
    private CrosshairUIController crosshairUIController;
    private GameObject detectedObject;
    private bool isInteractableObject = true;
    private bool isCollectibleItem = false;

    public GameObject GetDetectedObject(bool isCollectibleItemRequested) 
    {
        if (isCollectibleItem && isCollectibleItemRequested)
            return detectedObject;
        else if (!isCollectibleItem && !isCollectibleItemRequested)
            return detectedObject;
        else
            return null;
    }
    private void Start()
    {
        crosshairUIController = GameObject.Find("Crosshair_Image").GetComponent<CrosshairUIController>();
    }
    private void Update()
    {
        detectedObject = TryDetect();
    }
    private GameObject TryDetect()
    {
        if (inventoryUIController.isOpened)
            return null;

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, reachDistance))
        {
            Transform target = hit.transform;
            if (target.CompareTag("Craft Station"))
            {
                isCollectibleItem = false;
                crosshairUIController.TryChangeColor(isInteractableObject);
                return target.gameObject;
            }
            else if (target.CompareTag("Collectible Item"))
            {
                isCollectibleItem = true;
                crosshairUIController.TryChangeColor(isInteractableObject);
                return target.gameObject;
            }
        }
        crosshairUIController.TryChangeColor(!isInteractableObject);
        return null;
    }
}
