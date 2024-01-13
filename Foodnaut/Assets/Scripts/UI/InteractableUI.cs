using UnityEngine;

public class InteractableUI : MonoBehaviour
{
    [SerializeField] private Transform craftStationCameraPosition;
    private Vector3 defaultLocalPlayerCameraPosition = new Vector3(0, 0.655f, -0.057f);
    private Quaternion lastCameraRotation;
    public void Off(GameObject playerCameraPosition)
    {
        playerCameraPosition.transform.rotation = lastCameraRotation;
        playerCameraPosition.transform.localPosition = defaultLocalPlayerCameraPosition;
        //UI panels
    }
    public void On(GameObject playerCameraPosition)
    {
        lastCameraRotation = playerCameraPosition.transform.rotation;
        playerCameraPosition.transform.rotation = craftStationCameraPosition.rotation;
        playerCameraPosition.transform.position = craftStationCameraPosition.position;
        //UI panels
    }
}
