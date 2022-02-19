using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAlly : StateMachineBehaviour
{

    [SerializeField] AvatarTarget targetBodyPart = AvatarTarget.Root;
    [SerializeField, Range(0, 1)] float start = 0, end = 1;

    [HeaderAttribute("match target")]
    public Vector3 matchPosition;       // �w��p�[�c�����B���ė~�������W
    public Quaternion matchRotation;    // ���B���ė~������]

    [HeaderAttribute("Weights")]
    public Vector3 positionWeight = Vector3.one;        // matchPosition�ɗ^����E�F�C�g�B(1,1,1)�Ŏ��R�A(0,0,0)�ňړ��ł��Ȃ��Be.g. (0,0,1)�őO��̂�
    public float rotationWeight = 0;            // ��]�ɗ^����E�F�C�g�B

    private MatchTargetWeightMask weightMask;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetComponent<UnityChanScript>().lockontargetEnemy != null
            && animator.GetComponent<UnityChanScript>().lockon==true)
        {
            animator.keepAnimatorControllerStateOnDisable = true;
            matchPosition = animator.GetComponent<UnityChanScript>().lockontargetEnemy.transform.position;
            matchRotation = animator.GetComponent<UnityChanScript>().lockontargetEnemy.transform.rotation;
            weightMask = new MatchTargetWeightMask(positionWeight, rotationWeight);
        }
       
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetComponent<UnityChanScript>().lockontargetEnemy != null
            && animator.GetComponent<UnityChanScript>().lockon == true)
        {
            animator.MatchTarget(matchPosition, matchRotation, targetBodyPart, weightMask, start, end);
        }
    }
}
