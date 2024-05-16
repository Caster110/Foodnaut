using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    [SerializeField] private Transform playerTransform;
    private float xRotation;
    private float yRotation;
    private float mouseX;
    private float mouseY;
    private Vector3 localPosition = new Vector3(0, 0.655f, -0.057f);

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() 
    {
        mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }

    private void LateUpdate() 
    {
        transform.localPosition = localPosition;

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerTransform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}