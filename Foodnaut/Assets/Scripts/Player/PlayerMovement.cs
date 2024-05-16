using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speedWalk;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpPower;
    [SerializeField] private float speedRun;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float smoothTime = 0.1f; 

    private bool keyJump;
    private bool keyRun;
    private bool keySit;
    private bool keyW;
    private bool keyA;
    private bool keyS;
    private bool keyD;

    private Vector3 walkDirection;
    private Vector3 velocity;
    private float currentSpeed;

    // Используйте эту переменную для хранения текущей скорости для интерполяции
    private Vector3 currentVelocity;

    private void Start()
    {
        currentSpeed = speedWalk;
    }

    private void Update()
    {
        GetKeys();
    }

    private void GetKeys()
    {
        keyJump = Input.GetKey(KeyCode.Space);
        keyRun = Input.GetKey(KeyCode.LeftShift);
        keySit = Input.GetKey(KeyCode.LeftControl);
        keyW = Input.GetKey(KeyCode.W);
        keyA = Input.GetKey(KeyCode.A);
        keyS = Input.GetKey(KeyCode.S);
        keyD = Input.GetKey(KeyCode.D);
    }

    private void FixedUpdate()
    {
        bool groundCheck = characterController.isGrounded;
        TryJump(keyJump, groundCheck);
        TryRun(keyRun);
        TrySit(keySit);
        Walk();
        DoGravity(groundCheck);
    }

    private void Walk()
    {
        if (keyW)
            walkDirection += transform.forward;
        if (keyA)
            walkDirection -= transform.right;
        if (keyS)
            walkDirection -= transform.forward;
        if (keyD)
            walkDirection += transform.right;

        walkDirection.Normalize();

        // Плавное изменение скорости движения
        Vector3 targetVelocity = walkDirection * currentSpeed;
        currentVelocity = Vector3.SmoothDamp(currentVelocity, targetVelocity, ref currentVelocity, smoothTime);

        characterController.Move(currentVelocity * Time.fixedDeltaTime);
        walkDirection = Vector3.zero;
    }

    private void DoGravity(bool isGrounded)
    {
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y -= gravity * Time.fixedDeltaTime;
        }
        characterController.Move(velocity * Time.fixedDeltaTime);
    }

    private void TryJump(bool keyPressed, bool isGrounded)
    {
        if (keyPressed && isGrounded)
            velocity.y = jumpPower;
    }

    private void TryRun(bool keyPressed)
    {
        currentSpeed = keyPressed ? speedRun : speedWalk;
    }

    private void TrySit(bool keyPressed)
    {
        characterController.height = keyPressed ? 1f : 2f;
    }
}