using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    // переменные для чувствительности мыши для осей X и Y
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    // ориентация игрока в пространстве
    [SerializeField] private Transform orientation;

    // поворот по координатам X и Y
    private float xRotation;
    private float yRotation;

    private float mouseX;
    private float mouseY;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
    }

    private void FixedUpdate()
    {
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
