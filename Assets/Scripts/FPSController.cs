using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SCP_FPSController : MonoBehaviour
{
    [Header("ƒвижение")]
    public float walkSpeed = 6.0f;          // скорость ходьбы
    public float sprintSpeed = 10.0f;       // скорость бега (удержива€ левый Shift)
    public float jumpSpeed = 8.0f;          // сила прыжка
    public float gravity = 20.0f;           // гравитаци€

    [Header(" амера")]
    public float mouseSensitivity = 2.0f;   // чувствительность мыши
    public float verticalRotationLimit = 80.0f; // предел вертикального поворота

    private float verticalRotation = 0f;    // текущий угол поворота камеры по вертикали
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Camera playerCamera;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    // ќбработка поворота камеры
    void HandleMouseLook()
    {
        // √оризонтальный поворот персонажа
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, mouseX, 0);

        // ¬ертикальный поворот камеры
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);
        if (playerCamera)
        {
            playerCamera.transform.localEulerAngles = new Vector3(verticalRotation, 0, 0);
        }
    }

    // ќбработка движени€ персонажа
    void HandleMovement()
    {
        if (controller.isGrounded)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

            // ¬ычисл€ем вектор движени€
            Vector3 move = new Vector3(inputX, 0, inputZ);
            // Ќормализуем вектор, чтобы при диагональном движении скорость не увеличивалась
            if (move.magnitude > 1f)
                move.Normalize();

            // ѕреобразуем локальное направление в мировое
            move = transform.TransformDirection(move) * currentSpeed;
            moveDirection.x = move.x;
            moveDirection.z = move.z;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

}
