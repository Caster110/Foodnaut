using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftStationUI : MonoBehaviour
{
    [SerializeField] private GameObject playerCameraPosition;
    [SerializeField] private CameraMovement cameraMovementScript;
    [SerializeField] private PlayerMovement playerMovementScript;
    private Vector3 defaultLocalPlayerCameraPosition = new Vector3(0, 0.655f, -0.057f);
    private Quaternion lastCameraRotation;
    private bool isOpened = false;
    public void ToggleStoveMode(GameObject cameraPosition)
    {
        
    }
    public void Off()
    {
        playerCameraPosition.transform.rotation = lastCameraRotation;
        playerCameraPosition.transform.localPosition = defaultLocalPlayerCameraPosition;
        ToggleMovement();
        isOpened = false;
        Debug.Log(playerCameraPosition.transform.position);
    }
    public void On(Transform craftStationCameraPosition)
    {
        ToggleMovement();
        playerCameraPosition.transform.rotation = craftStationCameraPosition.rotation;
        playerCameraPosition.transform.position = craftStationCameraPosition.position;
        lastCameraRotation = craftStationCameraPosition.rotation;
        isOpened = true;
        Debug.Log(playerCameraPosition.transform.position);
    }
    private void ToggleMovement()
    {
        cameraMovementScript.enabled = !cameraMovementScript.enabled;
        playerMovementScript.enabled = !playerMovementScript.enabled;
    }
}
