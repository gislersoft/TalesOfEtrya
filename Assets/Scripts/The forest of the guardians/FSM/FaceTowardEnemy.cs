using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTowardEnemy : MonoBehaviour {

    public Transform agentTransform;
    public Transform agentGFX;

    public Transform targetTransform;

    public bool facingRight;

    public float rotSpeed = 10;

    public AttackCheck attackCheck;
    public PlayerAttack playerAttack;

    public bool blockFacing;

    private void Start()
    {
        facingRight = (agentGFX.localRotation.y < 0) ? true : false;
        blockFacing = false;
    }

    private void Update()
    {
        if(!blockFacing)
            FaceTowardsEnemy();
    }

    public void FaceTowardsEnemy()
    {
        float xPosDiff = agentTransform.position.x - targetTransform.position.x;
        float yRotDiff = agentTransform.localRotation.y - targetTransform.localRotation.y;

        if ((xPosDiff > 0.01f && !facingRight) || (xPosDiff < 0.01f && facingRight))
        {
            Flip();
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;

        agentGFX.localRotation = new Quaternion(
                agentGFX.localRotation.x,
                agentGFX.localRotation.y * -1,
                agentGFX.localRotation.z,
                agentGFX.localRotation.w
            );
        if(attackCheck != null)
        {
            attackCheck.handOffset.x *= -1;
        }
        if(playerAttack != null)
        {
            playerAttack.handOffset.x *= -1;
        }
    }
}
