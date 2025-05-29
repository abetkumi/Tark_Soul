using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateGuard : IPlayerStateScript
{
    private GameObject _player;  //プレイヤー
    private PlayerScript _playerScript; //プレイヤーのスクリプト
    private Animator _animator;
    private CharacterController _characterController;

    private float vert, horiz;  //軸入力用変数

    private Vector3 _moveForward;    //移動ベクトル

    public PlayerStateGuard(GameObject InsertPlayer)
    {
        _player = InsertPlayer;
        _playerScript = _player.GetComponent<PlayerScript>();
        _animator = _player.GetComponent<Animator>();
        _characterController = _player.GetComponent<CharacterController>();
    }

    public override void Start()
    {
        _animator.CrossFadeInFixedTime("BackGuardWalk", 0.3f);

        Debug.Log("ガード");
    }

    public override void End()
    {
        _animator.SetBool("LowerWalk", false);

    }

    public override void Update()
    {
        Move();
        Animation();
        StateUpdate();
    }

    void Move()
    {
        //Lスティックの縦横の入力を取得
        vert = Input.GetAxis("Vertical");
        horiz = Input.GetAxis("Horizontal");

        //カメラの正面ベクトルから、横(x.z)方向のベクトルを抽出し正規化
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        //Lスティックの入力とカメラの向きから、移動方向を決定
        _moveForward = cameraForward * vert + Camera.main.transform.right * horiz;

        //移動方向にプレイヤーを動かす
        _characterController.Move(_moveForward * _playerScript.GetWalkSpeed() * Time.deltaTime);

        
    }

    void Animation()
    {
        //移動方向によってアニメーションを変える

        if (Vector3.Dot(_player.transform.forward, _moveForward) > 0)
        {

            _animator.SetBool("LowerWalk", true);

        }
        else
        {
            //_animator.CrossFadeInFixedTime("BackGuardWalk", 0.3f);

            _animator.SetBool("LowerWalk", false);
        }


        //プレイヤー正面ベクトルを右に90度回したベクトルを用意する
        Quaternion rotation = Quaternion.AngleAxis(90, Vector3.up);
        Vector3 right = rotation * _player.transform.forward;

        right.z = 0;


        if(Vector3.Dot(right, _moveForward) > 0)
        {
            //右
            Debug.Log("右");
        }
        else
        {
            //左
            Debug.Log("左");
        }
        

        
    }

    void StateUpdate()
    {
        if(Input.GetButtonUp("Guard"))
        {
            _playerScript.SetPlayerState(new PlayerStateIdleScript(_player));
        }
    }
}
