using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensivity = 600f; //сенса мыши
    public Transform playerBody;
    private float xRotation = 0f;
    void Start()
    {
        //блокировка курсора 
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //координаты мыши 
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        //вращение по осям X и Y
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //ограничение переворота камеры
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
