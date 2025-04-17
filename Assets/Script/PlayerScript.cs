using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�p�X�N���v�g�N���X
public class PlayerScript : MonoBehaviour
{
    [SerializeField] float PlayerWalkSpeed;
    [SerializeField] float PlayerSprintSpeed;

    private float vert, horiz;  //�����͗p�ϐ�
    private CharacterController characterController;    //�v���C���[�p�L�����N�^�[�R���g���[��


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        doMove();
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

        }
        else
        {
            characterController.Move(moveForward * PlayerWalkSpeed * Time.deltaTime);
        }

        // �L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }
}
