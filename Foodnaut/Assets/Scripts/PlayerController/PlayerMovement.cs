using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed; // скорость игрока 
    public float groundDrag;

    public float jumpForce; // сила прыжка
    public float jumpCooldown; // перезарядка прыжка
    public float airMultipiler; // множитель воздуха 
    bool readyToJump = true; // готов ли к прожку? 

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight; // высота игрока 
    public LayerMask whatIsGround; // маска земли
    bool grounded; // игрок на земле?


    public Transform orientation; // ориентация игрока в пространстве 

    // ввод с клавиатуры 
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection; // вектор направления движения игрока

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        // проверка земли под ногами
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        PlayerInput();
        SpeedControl();

        // заземление игрока 
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    // метод ввода данных с клавиатуры 
    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // расчёт направления движения 
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultipiler, ForceMode.Force);
    }

    private void SpeedControl()
    {
        // получение скорости на плоскости 
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // лимит скорости
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // скорость по оси Y
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}