using UnityEngine;

public class RaycastDetector : MonoBehaviour
{
    [SerializeField] private float reachDistance = 2.5f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private UIController inventoryUI;
    [SerializeField] private CrosshairUIController crosshairUIController;

    private Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
    private GameObject detectedObject;
    private GameObject previousDetectedObject;
    private string detectedObjectType = null;
    private readonly string[] interactableTypes = {"CraftStation", "CollectibleItem", "Blender"};

    public GameObject GetDetectedObjectWithTag(string tagOfRequestedObject) 
    {
        if (detectedObjectType == tagOfRequestedObject)
            return detectedObject;
        else
            return null;
    }

    private void Update()
    {
        GameObject currentDetectedObject = TryDetect();

        if (currentDetectedObject != detectedObject)
        {
            if (previousDetectedObject != null)
            {
                // Отключаем Outline на предыдущем объекте
                Outline outlineScript = previousDetectedObject.GetComponent<Outline>();
                if (outlineScript != null)
                {
                    outlineScript.enabled = false;
                }
            }

            detectedObject = currentDetectedObject;

            if (detectedObject != null)
            {
                Debug.Log(detectedObject);
                // Включаем Outline на текущем объекте
                Outline outlineScript = detectedObject.GetComponent<Outline>();
                if (outlineScript != null)
                {
                    outlineScript.enabled = true;
                }
            }

            // Обновляем предыдущий обнаруженный объект
            previousDetectedObject = detectedObject;
        }
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
            {
                if (target.CompareTag(i))
                {
                    detectedObjectType = target.tag;
                    isInteractableObject = true;
                    crosshairUIController.TryChangeColor(isInteractableObject);
                    return target.gameObject;
                }
            }
        }

        crosshairUIController.TryChangeColor(isInteractableObject);
        return null;
    }
}