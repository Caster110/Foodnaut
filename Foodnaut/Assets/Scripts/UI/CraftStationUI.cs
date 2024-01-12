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
    public void SwitchStoveMode(GameObject cameraPosition)
    {
        
    }
    public void Off()
    {
        playerCameraPosition.transform.rotation = lastCameraRotation;
        playerCameraPosition.transform.localPosition = defaultLocalPlayerCameraPosition;
        SwitchMovement();
        Debug.Log(playerCameraPosition.transform.position);
    }
    public void On(Transform craftStationCameraPosition)
    {
        SwitchMovement();
        playerCameraPosition.transform.rotation = craftStationCameraPosition.rotation;
        playerCameraPosition.transform.position = craftStationCameraPosition.position;
        lastCameraRotation = craftStationCameraPosition.rotation;
        Debug.Log(playerCameraPosition.transform.position);
    }
    private void SwitchMovement()
    {
        cameraMovementScript.enabled = !cameraMovementScript.enabled;
        playerMovementScript.enabled = !playerMovementScript.enabled;
    }
}
