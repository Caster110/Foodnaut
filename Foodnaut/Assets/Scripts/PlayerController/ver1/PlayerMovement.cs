using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller; 
    public Transform groundCheck;
    public LayerMask groundMask; //поверхность проверки
    public float speedPlayer = 3f; //скорость игрока 
    public float gravity = -9.0f; //значение гравитации 
    public float jumpHeight = 1f; //высота прыжка
    public float groundDistance = 0.4f;
    Vector3 velocity; //ускорение свободного падения 
    bool isGrounded; //переменная проверки нахождения игрока на земле

    void Update()
    {
        //сфера, проверяющая, коснулся ли игрок земли
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        //перемещение игрока 
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        //вектор перемещения
        Vector3 move = transform.right * xInput + transform.forward * zInput;
        controller.Move(move * speedPlayer * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //прыжок
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        //присед
        if (Input.GetKey(KeyCode.LeftControl))
            controller.height = 1f;
        else
            controller.height = 2f;

        //бег
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                speedPlayer = 6f;
            else
                speedPlayer = 3f;
        }
    }
}
