using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public float moveSpeedShift = 4.7f;
    public float jumpForce = 2f;
    public float sensitivity = 2f;
    public float smoothTime = 0.1f;
    public AudioSource jumpSound;
    public AudioSource groundSound;
    public GameObject escMenu;
    private bool isEscMenuActive = false;

    private Rigidbody rb;
    private Camera cam;
    private Vector3 moveInput;
    private float yaw = 0f;
    private float pitch = 0f;
    private Vector3 currentVelocity;
    public bool isCameraEnabled = true;
    public bool isCameraDisabled = true;

    private float defaultFOV;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        defaultFOV = cam.fieldOfView;
    }

    void Update()
    {
        HandleInput();
        RotateCamera();
        Jump();
        if (Input.GetKey(KeyCode.B))
        {
            if (cam.fieldOfView > defaultFOV - 35f)
            {
                float targetFOV = defaultFOV - 35f;
                float speed = 20f;
    
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * speed);
            }
        }
        else
        {
            if (cam.fieldOfView != defaultFOV)
            {
                float speed = 10f;
    
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, defaultFOV, Time.deltaTime * speed);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isEscMenuActive = !isEscMenuActive; // Переключение состояния
            escMenu.gameObject.SetActive(isEscMenuActive);
            
            // Включаем или выключаем камеру в зависимости от состояния меню
            isCameraEnabled = !isEscMenuActive;
            isCameraDisabled = !isEscMenuActive;

            // Изменение состояния курсора
            Cursor.lockState = isEscMenuActive ? CursorLockMode.Confined : CursorLockMode.Locked;
    
            // Если меню активно, заморозить время, иначе возобновить
            Time.timeScale = isEscMenuActive ? 0 : 1;
        }
    }

    void HandleInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveInput = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = moveSpeedShift;
        }
        else
        {
            moveSpeed = 3.5f;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.F1))
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (Input.GetKey(KeyCode.F2))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void RotateCamera()
    {
        if (isCameraEnabled)
        {
            yaw += sensitivity * Input.GetAxis("Mouse X");
            pitch -= sensitivity * Input.GetAxis("Mouse Y");
        }
        if (isCameraDisabled)
        {
            pitch = Mathf.Clamp(pitch, -90f, 90f);
            transform.eulerAngles = new Vector3(0f, yaw, 0f);
            cam.transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpSound.Play();
        }
    }

    bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        groundSound.Play();
        return Physics.Raycast(ray, 1.1f);
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = transform.TransformDirection(moveInput);
        Vector3 targetVelocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, smoothTime);
    }

    public void Die()
    {
        Debug.Log("Player died!");
    }
}
