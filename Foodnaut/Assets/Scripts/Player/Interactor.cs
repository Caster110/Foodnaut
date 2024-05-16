using UnityEngine;
public class Interactor : MonoBehaviour
{
    [SerializeField] private CameraMovement cameraMovementScript;
    [SerializeField] private PlayerMovement playerMovementScript;
    [SerializeField] private RaycastDetector raycastDetector;
    [SerializeField] private InputChecker inputChecker;
    [SerializeField] private ItemPlayerStorage itemPlayerStorage;
    [SerializeField] private UIController _UIController;
    [SerializeField] private Transform playerCameraPosition;
    private void Update()
    {
        if (inputChecker.CanCloseUI())
        {
            _UIController.CloseUI(playerCameraPosition);
            ActivateAllMovement();
            return;
        }
        else if (inputChecker.CanInteract())
        {
            GameObject interactableObject;
            if (interactableObject = raycastDetector.GetDetectedObjectWithTag("CraftStation"))
            {
                DeactivateAllMovement();
                _UIController.OpenCraftStation(interactableObject);
            }
            else if (interactableObject = raycastDetector.GetDetectedObjectWithTag("Blender"))
            {
                DeactivateAllMovement();
                _UIController.OpenBlender(interactableObject);
            }
        }
        else if (inputChecker.CanPickUp())
        {
            GameObject interactableObject;
            if (interactableObject = raycastDetector.GetDetectedObjectWithTag("CollectibleItem"))
            {
                itemPlayerStorage.TryAddItem(interactableObject);
            }
            else if (interactableObject = raycastDetector.GetDetectedObjectWithTag("Blender"))
            {
                itemPlayerStorage.TryAddItem(interactableObject);
            }
        }
        else if (inputChecker.CanOpenInventory())
        {
            DeactivateCameraMovement();
            _UIController.OpenInventory();
        }
    }
    private void ActivateAllMovement()
    {
        cameraMovementScript.enabled = true;
        playerMovementScript.enabled = true;
    }
    private void DeactivateAllMovement()
    {
        cameraMovementScript.enabled = false;
        playerMovementScript.enabled = false;
    }
    private void DeactivateCameraMovement()
    {
        cameraMovementScript.enabled = false;
    }

}
