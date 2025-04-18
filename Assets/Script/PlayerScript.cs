using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//プレイヤーのアクションステータス
enum PlayerActionStatus
{
    Idle,
    Walk,
    Run,
    Attack,
}


//プレイヤー用スクリプトクラス
public class PlayerScript : MonoBehaviour
{
    [SerializeField] float PlayerWalkSpeed;     //プレイヤーの歩く速度
    [SerializeField] float PlayerSprintSpeed;   //プレイヤーの走る速度

    PlayerActionStatus playerActionStatus = PlayerActionStatus.Idle;


    private Animator animator = null;   //アニメーター
    private float vert, horiz;  //軸入力用変数
    private CharacterController characterController;    //プレイヤー用キャラクターコントローラ


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

    //移動用メソッド
    void doMove()
    {
        //Lスティックの縦横の入力を取得
        vert = Input.GetAxis("Vertical");
        horiz = Input.GetAxis("Horizontal");

        //カメラの正面ベクトルから、横(x.z)方向のベクトルを抽出し正規化
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        //Lスティックの入力とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * vert + Camera.main.transform.right * horiz;

        //移動方向にプレイヤーを動かす
        //スプリントボタン(このコードを書いた時は左Shift)を押すと走る
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

        // キャラクターの向きを進行方向に
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
