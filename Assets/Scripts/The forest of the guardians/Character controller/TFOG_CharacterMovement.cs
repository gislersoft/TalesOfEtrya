using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class TFOG_CharacterMovement : MonoBehaviour
{
    //Input management
    [Header("Input")]
    private float inputX;
    private float inputY;
    private const float inputZ = 1;
    private bool jumpRequest = false;
    private bool descendRequest = false;
    public bool testInCel = false;

    //Animation
    private Animator animator;
    private int inputZHashCode;
    private int isGroundHash;
    private int hitTriggerHashCode;
    [Header("Animation"), Range(1f, 10f)]
    public float animDampTime;

    //Movement and jumping
    private Rigidbody m_Rigidbody;
    [Header("Movement parameters")]
    [Range(1f, 50f)]
    public float runSpeed;
    [Range(0f, 20f)]
    public float strafeSpeed;

    [Header("Jump parameters")]
    [Range(1f, 10f)]
    public float jumpVelocity = 1f;
    [Range(1f, 10f)]
    public float fallMultiplier = 2.5f;
    [Range(1f, 10f)]
    public float descendMultiplier = 3f;
    [Range(0.01f, 5f)]
    public float maxCheckGroundDistance = 0.01f;
    [Range(1f, 10f)]
    public float maxYVelocity = 6f;
    public bool checkForGroundStatus = false;
    public bool isGrounded;
    public bool isInAir = false;
    bool canMove = true;

    [Header("Speed increase")]
    public float elapsedTimeUntilIncresingSpeed = 10f;
    private float countdownToIncreaseSpeed;
    public ValueGrowth speedValueGrowth;

    [Header("Slowing radious")]
    public float slowingRadius = 20;
    public Transform target;
    private Vector3 targetPosition;

    [Header("Transparency")]
    public Material transparentMaterial;
    public Vector3 castOffsetFromGround;
    public Vector3 rotationOffsetOfCast;
    public float distanceToSide = 10f;
    public LayerMask hitLayer;
    private RaycastHit hitLeft, hitRight;

    private void Start()
    {
        animator = GetComponent<Animator>();
        inputZHashCode = Animator.StringToHash("inputZ");
        isGroundHash = Animator.StringToHash("isGrounded");
        hitTriggerHashCode = Animator.StringToHash("getHit");
        animator.SetFloat(inputZHashCode, inputZ, 0, Time.deltaTime);
        animator.SetBool(isGroundHash, isGrounded);
        m_Rigidbody = GetComponent<Rigidbody>();
        countdownToIncreaseSpeed = elapsedTimeUntilIncresingSpeed;
        canMove = true;
        var objects = GameObject.FindGameObjectsWithTag("LastPlatform");
        if(objects.Length != 0)
        {
            target = objects[0].transform;
            targetPosition = target.position;
        }
    }

    private void Update()
    {
        if(target == null)
        {
            var objects = GameObject.FindGameObjectsWithTag("LastPlatform");
            if (objects.Length != 0)
            {
                target = objects[0].transform;
                targetPosition = target.position;
            }
        }

        if (canMove == false)
            return;
        InputMagnitude();
        Move();
        if (checkForGroundStatus)
        {
            CheckGroundStatus();
        }
        
        countdownToIncreaseSpeed -= Time.deltaTime;
    }

    private void CheckForTrees()
    {
        if (Physics.Raycast(transform.position + castOffsetFromGround, transform.right + rotationOffsetOfCast, out hitRight, distanceToSide, hitLayer))
        {
            MeshRenderer meshRenderer = hitRight.transform.GetComponent<MeshRenderer>();

            if (meshRenderer)
            {
                if (meshRenderer.material != transparentMaterial)
                {
                    meshRenderer.material = transparentMaterial;
                }
            }
        }

        if (Physics.Raycast(transform.position + castOffsetFromGround, -transform.right + rotationOffsetOfCast, out hitLeft, distanceToSide, hitLayer))
        {
            MeshRenderer meshRenderer = hitLeft.transform.GetComponent<MeshRenderer>();

            if (meshRenderer)
            {
                if(meshRenderer.material != transparentMaterial)
                {
                    meshRenderer.material = transparentMaterial;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (canMove == false)
            return;
        Jump();
        CheckForTrees();
    }

    private void LateUpdate()
    {
        if (canMove == false)
            return;
        if (countdownToIncreaseSpeed < 0)
        {
            if (speedValueGrowth.growthFunction == GROWTH_FUNCTION.LINEAR)
            {
                runSpeed = speedValueGrowth.linearGrowth.LinearGrowth(runSpeed);
            }

            else if (speedValueGrowth.growthFunction == GROWTH_FUNCTION.POLYNOMIAL)
            {
                runSpeed = speedValueGrowth.polynomialGrowth.PolynomialGrowth(runSpeed);
            }

            else if (speedValueGrowth.growthFunction == GROWTH_FUNCTION.EXPONENTIAL)
            {
                runSpeed = speedValueGrowth.exponentialGrowth.ExponentialGrowth(Time.timeSinceLevelLoad);
            }

            countdownToIncreaseSpeed = elapsedTimeUntilIncresingSpeed;
        }
    }

    public void Jump()
    {

        checkForGroundStatus = true;
        isGrounded = false;

        if (jumpRequest && !isInAir)
        {
            //m_Rigidbody.velocity += Vector3.up * jumpVelocity;
            m_Rigidbody.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            jumpRequest = false;
            isInAir = true;
            animator.SetBool(isGroundHash, isGrounded);
        }

        if (descendRequest && isInAir)
        {
            //m_Rigidbody.velocity -= Vector3.up * jumpVelocity;
            m_Rigidbody.AddForce(-Vector3.up * (jumpVelocity * descendMultiplier), ForceMode.Impulse);
            descendRequest = false;
        }

        if(m_Rigidbody.velocity.y > maxYVelocity)
        {
            m_Rigidbody.velocity = new Vector3(0, maxYVelocity, runSpeed);
        }


        if (!isGrounded)
        {
            if (m_Rigidbody.velocity.y < 0)
            {
                m_Rigidbody.velocity += Vector3.up * (Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime);
            }
        }
    }

    void Move()
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        float vel_y = (isGrounded) ? Mathf.Clamp(0f, 0.1f, m_Rigidbody.velocity.y) : m_Rigidbody.velocity.y;
        if (distance < slowingRadius)
        {
            

            m_Rigidbody.velocity = new Vector3(0f, vel_y, runSpeed * (distance / slowingRadius));
            countdownToIncreaseSpeed = float.MaxValue;
        }
        else
        {
            m_Rigidbody.velocity = new Vector3(0f, vel_y, runSpeed);
        }

        if (inputX < 0f)
        {
            LaneLimitBehavior.PreviousLane();
        }
        else if (inputX > 0f)
        {
            LaneLimitBehavior.NextLane();
        }

        Vector3 newPosition = new Vector3(LaneLimitBehavior.CurrentLaneLimit(), transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = Vector3.Slerp(transform.localPosition, newPosition, Time.deltaTime * strafeSpeed);
    }

    void InputMagnitude()
    {
        if (!canMove)
            return;

        if (testInCel)
        {
            if (Input.touchCount > 0 &&
                Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                inputX = Input.GetTouch(0).deltaPosition.x;
                inputY = Input.GetTouch(0).deltaPosition.y;

                if (Mathf.Abs(inputX) > Mathf.Abs(inputY))
                {
                    inputY = 0f;
                }
                else
                {
                    inputX = 0f;
                    if (inputY > 0f)
                    {
                        jumpRequest = true;
                    }
                    else if (inputY < 0f)
                    {
                        descendRequest = true;
                    }
                }

                if (!isGrounded)
                {
                    inputY = 0;
                }
            }
            else
            {
                inputX = 0f;
                inputY = 0f;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                inputX = -1f;
            }

            else if (Input.GetKeyDown(KeyCode.D))
            {
                inputX = 1f;
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                //inputY = 1f;
                jumpRequest = true;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                inputY = -1f;
                descendRequest = true;
            }
            else
            {
                inputX = 0f;
                inputY = 0f;
            }
        }
    }

    private void CheckGroundStatus()
    {
        Vector3 start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        if (Physics.Raycast(start, -transform.up, maxCheckGroundDistance))
        {
            isGrounded = true;
            checkForGroundStatus = false;
            isInAir = false;
            animator.SetBool(isGroundHash, isGrounded);
        }
    }

    public void StopMovement(bool wonTheGame)
    {
        runSpeed = 0f;
        canMove = false;

        m_Rigidbody.AddForce(new Vector3(0, 0, -m_Rigidbody.velocity.z));
    }

    public void GetHitAnimation()
    {
        animator.SetTrigger(hitTriggerHashCode);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if(target != null)
        {
            Gizmos.DrawWireSphere(target.position, slowingRadius);
        }

        Gizmos.color = Color.blue;
        
        Gizmos.DrawRay(transform.position + castOffsetFromGround, (transform.right + rotationOffsetOfCast) * distanceToSide);
        Gizmos.DrawRay(transform.position + castOffsetFromGround, (-transform.right + rotationOffsetOfCast) * distanceToSide);
    }
}

