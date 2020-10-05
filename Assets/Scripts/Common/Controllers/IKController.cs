using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour {

    public static IKController instance;

    private void Awake()
    {
        instance = this;
    }

    public Animator anim;
    public bool isIKActive;

    #region Left Hand
    public Transform leftHandTarget;
    [Range(0, 1)]
    public float leftHandWeight = 1;
    [Range(0, 1)]
    public float leftHandRotWeight = 1;
    #endregion

    #region Right Hand
    public Transform rightHandTarget;
    [Range(0, 1)]
    public float rightHandWeight = 1;
    [Range(0, 1)]
    public float rightHandRotWeight = 1;
    #endregion

    #region Right Elbow
    public Transform rightElbowTarget;
    [Range(0, 1)]
    public float rightElbowWeight = 1;
    #endregion

    #region Left Elbow
    public Transform leftElbowTarget;
    [Range(0, 1)]
    public float leftElbowWeight = 1;
    #endregion

    private void OnAnimatorIK(int layerIndex)
    {
        if (isIKActive)
        {
            if (leftHandTarget != null)
            {
                anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandRotWeight);
                anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
            }

            if (leftElbowTarget != null)
            {
                anim.SetIKHintPosition(AvatarIKHint.LeftElbow, leftElbowTarget.position);
                anim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, leftElbowWeight);
            }

            if (rightHandTarget != null)
            {
                anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandRotWeight);
                anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
            }

            if (rightElbowTarget != null)
            {
                anim.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowTarget.position);
                anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, rightElbowWeight);
            }
        }
    }

    public void ActivateIK()
    {
        isIKActive = true;
    }

    public void DeactivateIK()
    {
        isIKActive = false;
    }

    public void SetIKTranforms(
        Transform leftElbow, 
        Transform leftHand, 
        Transform rightElbow,
        Transform rightHand)
    {
        leftElbowTarget = leftElbow;
        leftHandTarget = leftHand;
        rightElbowTarget = rightElbow;
        rightHandTarget = rightHand;
    }
}
