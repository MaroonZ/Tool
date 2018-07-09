using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIK : MonoBehaviour {
    public GameObject eyePoint;
    //public GameObject lineArch;
    [SerializeField]
    Animator anim;
    [SerializeField]
    public Transform lookTarget;
    [SerializeField]
    public Transform leftHandTarget;
    [SerializeField]
    public Transform rightHandTarget;
    [SerializeField]
    Transform leftFootTarget;
    [SerializeField]
    Transform rightFootTarget;

    void Start () {
		
	}
    private void OnAnimatorIK(int layerIndex)
    {
        if (!anim) return;
        if (lookTarget)
        {
            anim.SetLookAtWeight(1);
            anim.SetLookAtPosition(lookTarget.position);
        }
        if (leftHandTarget)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
        }
        if (rightHandTarget)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
            anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
        }
        if (leftFootTarget)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
            anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootTarget.position);
            anim.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootTarget.rotation);
        }
        if (rightFootTarget)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
            anim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootTarget.position);
            anim.SetIKRotation(AvatarIKGoal.RightFoot, rightFootTarget.rotation);
        }

    }
}
