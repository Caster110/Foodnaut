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
    private void LateUpdate()
    {
        GetKeys();
    }
    private void GetKeys()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
    }
    private void FixedUpdate()
    {
        transform.localPosition = localPosition;
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerTransform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
