using UnityEngine;

public class FightPlayerMovement : MonoBehaviour {
    //public bool testInCel = false;
    public Joystick joystick;
    [Header("Enemy")]
    public Transform targetTransform;

    [Header("Components")]
    public Rigidbody2D playerRigidbody;
    public Animator animator;
    public Transform playerGFX;
    //Animation hashes
    private int movementHash;
    private int jumpHash;
    private int groundedHash;
    private int directionHash;

    [Header("Movement")]
    public float movementSpeed = 3f;
    public float maxMovementForce = 10f;
    public bool facingRight;

    [Header("Jump")]
    public bool jumpRequest;
    public float jumpForce = 3f;
    const float k_GroundedRadius = .1f;
    public bool isGrounded = true;
    [SerializeField] private LayerMask m_WhatIsGround;

    public void Start()
    {
        jumpRequest = false;
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        facingRight = (playerGFX.localRotation.y < 0) ? true : false; 

        movementHash = Animator.StringToHash("MovementMagnitude");
        jumpHash = Animator.StringToHash("PlayerJumpMagnitude");
        groundedHash = Animator.StringToHash("PlayerGrounded");
        directionHash = Animator.StringToHash("Direction");
    }
    // Update is called once per frame
    void Update () {
        if (TFOG_GameManager.Instance.isGameOver)
            return;

        float Horizontal = joystick.Horizontal;
        Move(Horizontal);

        float Vertical = joystick.Vertical;

        if (Vertical > 0.5f && isGrounded)
            jumpRequest = true;
    }
    private void FixedUpdate()
    {
        if (TFOG_GameManager.Instance.isGameOver)
            return;
        if (jumpRequest)
        {
            Jump();
        }
    }
    private void LateUpdate()
    {
        FaceTowardsEnemy();
        CheckGroundStatus();
        animator.SetFloat(movementHash,
            Mathf.Clamp01(
                (playerRigidbody.velocity.x < 0) ? -playerRigidbody.velocity.x : playerRigidbody.velocity.x)
                );

        animator.SetFloat(jumpHash,
                Mathf.Lerp(Mathf.Clamp01(Mathf.Abs(playerRigidbody.velocity.y)), 1, Time.deltaTime)

            );
    }


    public void Move(float movement)
    {
        playerRigidbody.AddForce(new Vector2(movement * movementSpeed, 0), ForceMode2D.Impulse);

        float clampedHorizontal = Mathf.Clamp(playerRigidbody.velocity.x, -maxMovementForce, maxMovementForce);

        playerRigidbody.velocity = new Vector2(clampedHorizontal, playerRigidbody.velocity.y);    
        
        if(facingRight && playerRigidbody.velocity.x < 0)
        {
            animator.SetFloat(directionHash, -1);
        }
        else if(facingRight && playerRigidbody.velocity.x > 0)
        {
            animator.SetFloat(directionHash, 1);
        }
        else if (!facingRight && playerRigidbody.velocity.x > 0)
        {
            animator.SetFloat(directionHash, -1);
        }
        else if (!facingRight && playerRigidbody.velocity.x < 0)
        {
            animator.SetFloat(directionHash, 1);
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            jumpRequest = false;
            animator.SetBool(groundedHash, false);
            isGrounded = false;
            playerRigidbody.AddForce(new Vector2(transform.up.x, transform.up.y) * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void CheckGroundStatus()
    {
        //isGrounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, k_GroundedRadius, m_WhatIsGround);

        if(colliders.Length != 0)
        {
            isGrounded = true;
            animator.SetBool(groundedHash, true);
        }
        //Debug.Log(colliders.Length);
        //for (int i = 0; i < colliders.Length; i++)
        //{
        //    Debug.Log(colliders[i].name);
        //    if (colliders[i].gameObject != gameObject)
        //        isGrounded = true;
        //    animator.SetBool(groundedHash, true);
        //}
    }

    public void FaceTowardsEnemy()
    {
        float xPosDiff = transform.position.x - targetTransform.position.x;
        float yRotDiff = transform.localRotation.y - targetTransform.localRotation.y;

        if ((xPosDiff > 0.01f && facingRight) || (xPosDiff < 0.01f && !facingRight))
        {
            facingRight = !facingRight;
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, k_GroundedRadius);
    }

    public void JumpRequest()
    {
        if (isGrounded)
        {
            jumpRequest = true;
        }
    }
}
