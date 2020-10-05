using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class _Aradis : _Agent {

    public bool isGrounded = true;
    public float checkGroundDistance = 0.01f;

    private void Start()
    {
        startPosition = agentTransform.transform.position;
    }

    private void FixedUpdate()
    {
        CheckGroundStatus();
        Debug.DrawRay(transform.position, -transform.up * checkGroundDistance, Color.blue);
    }

    public override void SetUp()
    {
        targetNeuralNetwork.Init();

        threadNeuralNetwork = new _NeuralNetwork();
        threadNeuralNetwork.Init(targetNeuralNetwork);

        agentTransform.transform.position = startPosition;
    }

    public override int GetAction()
    {
        if(UnityEngine.Random.Range(0f, 1f) < epsilon)
        {
            return UnityEngine.Random.Range(0, Enum.GetNames(typeof(_AradisActions)).Length);
        }
        else
        {
            var input = screenInput.GetInputTensor();

            var result = threadNeuralNetwork.Predict(input);

            return NN_Utils.Argmax(result);
        }

        
    }

    public override double GetValue()
    {
        return threadNeuralNetwork.Value_funtion;
    }

    public override void UpdateNetworks(List<float> accumRewards)
    {
        for (int i = 0; i < accumRewards.Count; i++)
        {
            reward = accumRewards[i] + gamma * reward;
            //Accum gradients of policy
            //Accum gradaients of value
        }

        PerformAsynchronousUpdate();
    }

    public override void PerformAsynchronousUpdate()
    {

    }

    public override float MoveForward()
    {
        Collider[] blockTest = Physics.OverlapCapsule(
                new Vector3(
                    capsuleCollider.center.x,
                    capsuleCollider.center.y + capsuleCollider.height / 2f,
                    capsuleCollider.center.z + movementSpeed),
                new Vector3(
                    capsuleCollider.center.x,
                    capsuleCollider.center.y - capsuleCollider.height / 2f,
                    capsuleCollider.center.z + movementSpeed),
                capsuleCollider.radius);

        if (blockTest.Where(col => col.gameObject.tag == "Wall").ToArray().Length == 0)
        {
            agentTransform.transform.position = Vector3.Lerp(
                agentTransform.transform.position,
                new Vector3(
                    agentTransform.transform.position.x, 
                    agentTransform.transform.position.x, 
                    agentTransform.transform.position.z + movementSpeed),
                Time.deltaTime
                );
        }

        return rewardsDict["Move"];
    }

    public override float MoveBackward()
    {
        Collider[] blockTest = Physics.OverlapCapsule(
                new Vector3(
                    capsuleCollider.center.x,
                    capsuleCollider.center.y + capsuleCollider.height / 2f,
                    capsuleCollider.center.z - movementSpeed),
                new Vector3(
                    capsuleCollider.center.x,
                    capsuleCollider.center.y - capsuleCollider.height / 2f,
                    capsuleCollider.center.z - movementSpeed),
                capsuleCollider.radius);

        if (blockTest.Where(col => col.gameObject.tag == "Wall").ToArray().Length == 0)
        {
            agentTransform.transform.position = Vector3.Lerp(
                agentTransform.transform.position,
                new Vector3(
                    agentTransform.transform.position.x,
                    agentTransform.transform.position.y,
                    agentTransform.transform.position.z - movementSpeed),
                Time.deltaTime
                );
        }

        return rewardsDict["Move"];
    }

    public override float Jump()
    {
        
        Collider[] blockTest = Physics.OverlapCapsule(
               new Vector3(
                   capsuleCollider.center.x,
                   capsuleCollider.center.y + capsuleCollider.height / 2f + jumpForce,
                   capsuleCollider.center.z),
               new Vector3(
                   capsuleCollider.center.x,
                   capsuleCollider.center.y - capsuleCollider.height / 2f + jumpForce,
                   capsuleCollider.center.z),
               capsuleCollider.radius);

        if (blockTest.Where(col => col.gameObject.tag == "Wall").ToArray().Length == 0 && isGrounded)
        {
            Debug.Log("S");
            agentRigidbody.AddForce(agentTransform.up * jumpForce, ForceMode.Impulse);
        }

        return rewardsDict["Jump"];
    }

    public override float Attack()
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(
            agentViewpoint.transform.position,
            castRadius,
            agentViewpoint.forward,
            out hitInfo,
            meleeRange))
        {
            if (hitInfo.transform.tag == StringsInGame.PlayerName)
            {
                return rewardsDict["Hit"];
            }
        }
        return rewardsDict["No Hit"];
    }

    public override bool KilledEnemy()
    {
        return (enemyStats.CurrentHealth <= 0) ? true : false;
    }

    public override  bool Died()
    {
        return (agentStats.CurrentHealth <= 0) ? true : false;
    }

    public void CheckGroundStatus()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up, out hit, checkGroundDistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}

public enum _AradisActions
{
    DO_NOTHING,
    MOVE_LEFT,
    MOVE_RIGHT,
    JUMP,
    ATTACK,
}