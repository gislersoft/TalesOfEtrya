using UnityEngine;

public class FSM_Movement : AradisFSM {

    [Header("Movement")]
    public const float movementSpeed = 2.5f;
    public const float stoppingDistance = 1;
    public const float stoppingDistanceOffset = 0.3f;
    public bool facingRight;
    public const float lerpSpeed = 10;

    [Header("Jump")]
    public const float jumpForce = 3;
    const float k_GroundedRadius = .2f;
    public bool isGrounded = false;

    public AttackCheck attackCheck;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        movementHash = Animator.StringToHash("AradisMovement");

        facingRight = (agentGFX.localRotation.y < 0) ? true : false;
        attackCheck = animator.GetComponent<AttackCheck>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FaceTowardsEnemy();
        CheckGroundStatus();
        if (useFSM)
        {
            if (!FSM_Taunt.isTaunting)
            {
                Movement(animator);
                Jump(animator);
            }
        }
        else
        {

        }
    }

    public void Jump(Animator animator)
    {
        if(targetTransform.localPosition.y > agentTransform.localPosition.y && isGrounded)
        {
            if (attackCheck.isAttacking)
            {
                agentRigidbody.AddForce(new Vector2(agentTransform.up.x, agentTransform.up.y) * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }

        
    }

    public void Movement(Animator animator)
    {
        Vector3 direction = new Vector3(targetTransform.localPosition.x - agentTransform.localPosition.x, 0f, 0f);

        float distance = Mathf.Abs(targetTransform.localPosition.x - agentTransform.localPosition.x);
        if ((stoppingDistance - stoppingDistanceOffset) <= distance && distance <= (stoppingDistance + stoppingDistanceOffset))
        {
            DoNothing(animator);
        }
        else if (distance < (stoppingDistance - stoppingDistanceOffset))
        {
            MoveRight(direction, animator);
        }
        else
        {
            MoveLeft(direction, animator);
        }

        

        //if (direction.x == 0)
        //{
            
        //}
        //else if (direction.x > 0)
        //{
        //    //"Going left"
            
        //}
        //else
        //{
            
        //}
    }

    public void FaceTowardsEnemy()
    {
        float xPosDiff = agentTransform.position.x - targetTransform.position.x;
        float yRotDiff = agentTransform.localRotation.y - targetTransform.localRotation.y;

        if ((xPosDiff > 0.01f && !facingRight) || (xPosDiff < 0.01f && facingRight))
        {
            facingRight = !facingRight;
        }
    }

    public void MoveLeft(Vector3 direction, Animator animator)
    {
        agentTransform.transform.localPosition =
            Vector3.Lerp(agentTransform.transform.localPosition, agentTransform.transform.localPosition + direction, Time.deltaTime);

        if (facingRight)
        {
            animator.SetFloat(movementHash, Mathf.Lerp(animator.GetFloat(movementHash), 0, Time.deltaTime * lerpSpeed));
        }
        else
        {
            animator.SetFloat(movementHash, Mathf.Lerp(animator.GetFloat(movementHash), 1, Time.deltaTime * lerpSpeed));
        }
    }

    public void MoveRight(Vector3 direction, Animator animator)
    {
        agentTransform.transform.localPosition =
            Vector3.Lerp(agentTransform.transform.localPosition, agentTransform.transform.localPosition - direction, Time.deltaTime);

        if (facingRight)
        {
            animator.SetFloat(movementHash, Mathf.Lerp(animator.GetFloat(movementHash), 1, Time.deltaTime * lerpSpeed));
        }
        else
        {
            animator.SetFloat(movementHash, Mathf.Lerp(animator.GetFloat(movementHash), 0, Time.deltaTime * lerpSpeed));
        }
    }

    public void DoNothing(Animator animator)
    {
        animator.SetFloat(movementHash, Mathf.Lerp(animator.GetFloat(movementHash), 0, Time.deltaTime * lerpSpeed));
    }

    public void CheckGroundStatus()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(agentTransform.position, k_GroundedRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != agentTransform.gameObject)
                isGrounded = true;
        }
    }
}
