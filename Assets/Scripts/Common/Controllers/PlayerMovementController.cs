using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour {

    public float inputX;
    public float inputZ;
    public Vector3 desiredMoveDirection;
    public bool blockRotationPlayer;
    public float desiredRotationSpeed;
    public Animator anim;
    public float speed;
    public float allowPlayerRotation;
    public Camera cam;
    public CharacterController controller;
    //public PlayerStats stats;
    public bool isGrounded;

    float verticalVel;
    Vector3 moveVector;

    public VirtualJoystick joystick;
    //public FloatingJoystick FJoystick;

    private void Start()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
        controller = GetComponent<CharacterController>();
        //stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        InputMagnitude();
        //InputMagnitudeFloatingJoYstick();

        isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            verticalVel -= 0;
        }
        else
        {
            verticalVel -= 2;
        }

        moveVector = new Vector3(0f, verticalVel, 0f);
        controller.Move(moveVector);
    }


    void PlayerMoveAndRotation()
    {
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * inputZ + right * inputX;
        
        if(blockRotationPlayer == false)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                Quaternion.LookRotation(desiredMoveDirection), 
                desiredRotationSpeed);
        }
    }

    void InputMagnitude()
    {
        //Calculate the input vectors
        inputX = joystick.Horizontal();// Input.GetAxis("Horizontal");
        inputZ = joystick.Vertical();//Input.GetAxis("Vertical");

        anim.SetFloat("inputZ", inputZ, 0.0f, Time.deltaTime * 2f);
        anim.SetFloat("inputX", inputX, 0.0f, Time.deltaTime * 2f);
        
        //Calculate the input magnitude
        speed = new Vector2(inputX, inputZ).sqrMagnitude;

        //Physically move the player
        if(speed >= allowPlayerRotation)
        {
            anim.SetFloat("inputMagnitude", speed, 0f, Time.deltaTime);
            if (inputX != 0 && inputZ != 0)
                PlayerMoveAndRotation();
        }
        else if (speed < allowPlayerRotation)
        {
            anim.SetFloat("inputMagnitude", speed, 0f, Time.deltaTime);
        }
    }

    /*
    void InputMagnitudeFloatingJoYstick() {
        //Calculate the input vectors
        inputX = FJoystick.Horizontal();// Input.GetAxis("Horizontal");
        inputZ = FJoystick.Vertical();//Input.GetAxis("Vertical");

        anim.SetFloat( "inputZ", inputZ, 0.0f, Time.deltaTime * 2f );
        anim.SetFloat( "inputX", inputX, 0.0f, Time.deltaTime * 2f );

        //Calculate the input magnitude
        speed = new Vector2( inputX, inputZ ).sqrMagnitude;

        //Physically move the player
        if (speed >= allowPlayerRotation) {
            anim.SetFloat( "inputMagnitude", speed, 0f, Time.deltaTime );
            if (inputX != 0 && inputZ != 0)
                PlayerMoveAndRotation();
        } else if (speed < allowPlayerRotation) {
            anim.SetFloat( "inputMagnitude", speed, 0f, Time.deltaTime );
        }
    }*/
}
