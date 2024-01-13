using UnityEngine;

public class InteractableUI : MonoBehaviour
{
    [SerializeField] private GameObject playerCameraPosition;
    private Vector3 defaultLocalPlayerCameraPosition = new Vector3(0, 0.655f, -0.057f);
    private Quaternion lastCameraRotation;
    public void Off()
    {
        playerCameraPosition.transform.rotation = lastCameraRotation;
        playerCameraPosition.transform.localPosition = defaultLocalPlayerCameraPosition;
        //UI panels
    }
    public void On(Transform craftStationCameraPosition)
    {
        playerCameraPosition.transform.rotation = craftStationCameraPosition.rotation;
        playerCameraPosition.transform.position = craftStationCameraPosition.position;
        lastCameraRotation = craftStationCameraPosition.rotation;
        //UI panels
    }
}
