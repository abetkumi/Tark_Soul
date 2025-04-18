using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�v���C���[�̃A�N�V�����X�e�[�^�X
enum PlayerActionStatus
{
    Idle,
    Walk,
    Run,
    Attack,
}


//�v���C���[�p�X�N���v�g�N���X
public class PlayerScript : MonoBehaviour
{
    [SerializeField] float PlayerWalkSpeed;     //�v���C���[�̕������x
    [SerializeField] float PlayerSprintSpeed;   //�v���C���[�̑��鑬�x

    PlayerActionStatus playerActionStatus = PlayerActionStatus.Idle;


    private Animator animator = null;   //�A�j���[�^�[
    private float vert, horiz;  //�����͗p�ϐ�
    private CharacterController characterController;    //�v���C���[�p�L�����N�^�[�R���g���[��


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        doMove();

        if(Input.GetMouseButtonDown(0))
        {
            doAttack();
        }
    }

    //�ړ��p���\�b�h
    void doMove()
    {
        //L�X�e�B�b�N�̏c���̓��͂��擾
        vert = Input.GetAxis("Vertical");
        horiz = Input.GetAxis("Horizontal");

        //�J�����̐��ʃx�N�g������A��(x.z)�����̃x�N�g���𒊏o�����K��
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        //L�X�e�B�b�N�̓��͂ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * vert + Camera.main.transform.right * horiz;

        //�ړ������Ƀv���C���[�𓮂���
        //�X�v�����g�{�^��(���̃R�[�h�����������͍�Shift)�������Ƒ���
        if(Input.GetButton("Sprint")) 
        {
            characterController.Move(moveForward * PlayerSprintSpeed * Time.deltaTime);
            animator.SetBool("Walk", false);
            animator.SetBool("Run", true);
        }
        else
        {
            characterController.Move(moveForward * PlayerWalkSpeed * Time.deltaTime);
            animator.SetBool("Walk", true);
            animator.SetBool("Run", false);

        }

        // �L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);

        }
    }

    void doAttack()
    {
        animator.SetBool("Attack_1", true);
    }


    void StateTransitionAttack()
    {
        playerActionStatus = PlayerActionStatus.Attack;
    }

    void StateTransitionWalk()
    {
        playerActionStatus = PlayerActionStatus.Walk;
    }

    void StateTransitionRun()
    {
        playerActionStatus = PlayerActionStatus.Run;
    }

    void AnimationStateUpdate()
    {
        switch(playerActionStatus)
        {
            case PlayerActionStatus.Attack:
                break;
            case PlayerActionStatus.Walk:
                break;

        }
    }
}
