using System.Collections.Generic;
using UnityEngine;

public abstract class _Agent : MonoBehaviour {

    public _NeuralNetwork threadNeuralNetwork;
    public _NeuralNetwork targetNeuralNetwork;
    public ScreenInput screenInput;

    [Header("Agent parameters")]
    public Transform agentTransform;
    public Transform agentViewpoint;
    public Rigidbody agentRigidbody;
    public CapsuleCollider capsuleCollider;
    public int movementSpeed;
    public int jumpForce;
    public float meleeRange;
    public float castRadius;
    public readonly Dictionary<string, float> rewardsDict = new Dictionary<string, float>()
    {
        {"Move", 0f },
        {"Jump", 0f },
        {"No Hit", -0.01f },
        {"Hit", 0.1f },
        {"Won", 1f },
        {"Lose", -1f }
    };
    public Vector3 startPosition;

    [Header("Stats")]
    public CharacterStats agentStats;
    public CharacterStats enemyStats;

    [Header("Hyper parameters")]
    public float reward;
    public float gamma;
    public float epsilon;

    public abstract void SetUp();
    public abstract int GetAction();
    public abstract double GetValue();
    public abstract void UpdateNetworks(List<float> accumRewards);
    public abstract void PerformAsynchronousUpdate();
    public abstract float MoveForward();
    public abstract float MoveBackward();
    public abstract float Jump();
    public abstract float Attack();
    public abstract bool KilledEnemy();
    public abstract bool Died();
}
