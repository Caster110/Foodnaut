using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speedWalk;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpPower;
    [SerializeField] private float speedRun;
    [SerializeField] private CharacterController characterController;

    private float keyHorizontal;
    private float keyVertical;
    private bool keySit;
    private bool keyJump;
    private bool keyRun;

    private Vector3 walkDirection;
    private Vector3 velocity;
    private float currentSpeed;

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
        keyRun= Input.GetKey(KeyCode.LeftShift);
        keySit = Input.GetKey(KeyCode.LeftControl);
        keyHorizontal = Input.GetAxis("Horizontal");
        keyVertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        bool groundCheck = characterController.isGrounded;
        walkDirection = keyHorizontal * transform.right + keyVertical * transform.forward;
        TryJump(keyJump, groundCheck);
        TryRun(keyRun);
        TrySit(keySit);
        Walk(walkDirection);
        DoGravity(groundCheck);
    }

    private void Walk(Vector3 direction)
    {
        characterController.Move(direction * currentSpeed * Time.fixedDeltaTime);
    }

    private void DoGravity(bool isGrounded)
    {
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = 0f;
            return;
        }
        velocity.y -= gravity * Time.fixedDeltaTime;
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