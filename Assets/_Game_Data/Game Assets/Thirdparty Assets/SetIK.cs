using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIK : MonoBehaviour
{
    
    public Transform rightHand, leftHand;
    public Transform rightFoot, leftFoot;
    public bool ikActive = false;
    protected Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAnimatorIK()
    {

      //  if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
          //  if (ikActive)
            {

                
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);



                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1.0f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1.0f);

                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1.0f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1.0f);

                
                animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHand.rotation);
                animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
                animator.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
                animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFoot.position);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFoot.rotation);
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFoot.position);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFoot.rotation);
            }


        }
    }
}
