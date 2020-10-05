using UnityEngine;

public class CameraController : MonoBehaviour
{

    private const float Y_ANGLE_MIN = -50.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    //[HideInInspector]
    public Transform lookAt;
    public Transform playerViewpoint;

    public VirtualJoystick joystick;
    public FloatingJoystick FJoystick;
    protected Transform camTransform;

    public float sensitivity = 4.0f;
    protected float currentX = 0.0f;
    protected float currentY = 0.0f;

    [SerializeField]
    protected bool blockYRotation = false;
    [SerializeField]
    private bool invertYAxis = false;

    //Garbage collector optimization variables
    public Vector3 offset;
    protected Quaternion rotation;
    private Vector3 m_Velocity = Vector3.zero;

    protected void Start()
    {
        camTransform = transform;
        //lookAt = playerViewpoint;
    }

    private void Update()
    {
        //currentX += joystick.Horizontal() * (sensitivity / 2f); 
        //currentY += joystick.Vertical() * (sensitivity / 2f);

        currentX += FJoystick.Horizontal * (sensitivity / 2f);
        currentY += FJoystick.Vertical * (sensitivity / 2f);

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    void LateUpdate()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        rotation = Quaternion.Euler(currentY, -currentX, 0);


        if (invertYAxis)
        {
            rotation = Quaternion.Euler(-currentY, -currentX, 0);
        }

        if (blockYRotation)
        {
            rotation = Quaternion.Euler(0, -currentX, 0);
        }

        Vector3 desiredPosition = lookAt.position + (rotation * offset);

        camTransform.position = Vector3.SmoothDamp(camTransform.position, desiredPosition, ref m_Velocity, sensitivity * Time.deltaTime);

        //camTransform.position = Vector3.Lerp(camTransform.position, desiredPosition, sensitivity * Time.deltaTime);

        camTransform.LookAt(lookAt);
    }

    public void TransitionToPlayer()
    {
        blockYRotation = false;
        lookAt = playerViewpoint;
    }


    
}