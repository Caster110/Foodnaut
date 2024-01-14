using UnityEngine;

public class InteractableUI : MonoBehaviour
{
    [SerializeField] private Transform craftStationCameraPosition;
    private Vector3 defaultLocalPlayerCameraPosition = new Vector3(0, 0.655f, -0.057f);
    private Quaternion lastCameraRotation;
    public void Off(Transform playerCameraPosition)
    {
        playerCameraPosition.rotation = lastCameraRotation;
        playerCameraPosition.localPosition = defaultLocalPlayerCameraPosition;
        //UI panels
    }
    public void On(Transform playerCameraPosition)
    {
        lastCameraRotation = playerCameraPosition.rotation;
        playerCameraPosition.rotation = craftStationCameraPosition.rotation;
        playerCameraPosition.position = craftStationCameraPosition.position;
        //UI panels
    }
}
