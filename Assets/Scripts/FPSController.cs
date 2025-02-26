using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SCP_FPSController : MonoBehaviour
{
    [Header("��������")]
    public float walkSpeed = 6.0f;          // �������� ������
    public float sprintSpeed = 10.0f;       // �������� ���� (��������� ����� Shift)
    public float jumpSpeed = 8.0f;          // ���� ������
    public float gravity = 20.0f;           // ����������

    [Header("������")]
    public float mouseSensitivity = 2.0f;   // ���������������� ����
    public float verticalRotationLimit = 80.0f; // ������ ������������� ��������

    private float verticalRotation = 0f;    // ������� ���� �������� ������ �� ���������
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

    // ��������� �������� ������
    void HandleMouseLook()
    {
        // �������������� ������� ���������
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, mouseX, 0);

        // ������������ ������� ������
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);
        if (playerCamera)
        {
            playerCamera.transform.localEulerAngles = new Vector3(verticalRotation, 0, 0);
        }
    }

    // ��������� �������� ���������
    void HandleMovement()
    {
        if (controller.isGrounded)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

            // ��������� ������ ��������
            Vector3 move = new Vector3(inputX, 0, inputZ);
            // ����������� ������, ����� ��� ������������ �������� �������� �� �������������
            if (move.magnitude > 1f)
                move.Normalize();

            // ����������� ��������� ����������� � �������
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
