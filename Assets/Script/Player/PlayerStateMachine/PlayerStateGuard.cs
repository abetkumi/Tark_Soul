using Cysharp.Threading.Tasks;
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

    string nowAnimationName;

    public PlayerStateGuard(GameObject InsertPlayer)
    {
        _player = InsertPlayer;
        _playerScript = _player.GetComponent<PlayerScript>();
        _animator = _player.GetComponent<Animator>();
        _characterController = _player.GetComponent<CharacterController>();
    }

    public override void Start()
    {
        _animator.CrossFadeInFixedTime("IdleGuard", 0.3f);
        nowAnimationName = "IdleGuard";
        _playerScript.GetGuardCollider().enabled = true;


        Debug.Log("ガード");
    }

    public override void End()
    {
        Debug.Log("ガード終了");

        _playerScript.GetGuardCollider().enabled = false;
    }

    public override void Update()
    {
        Move();

        string playAnimationName = AnimationName();
        if(nowAnimationName != playAnimationName)
        {
            _animator.CrossFadeInFixedTime(playAnimationName, 0.3f);
            nowAnimationName = playAnimationName;
        }

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

    //再生するアニメーションの名前設定
    string AnimationName()
    {
        //移動方向によってアニメーションを変える

        float forwardVecDot = Vector3.Dot(_player.transform.forward, _moveForward);

        if (forwardVecDot >= 0.5f)
        {
            return "ForwardGuardWalk";
        }
        else if (forwardVecDot <= -0.5f)
        {
            return "BackGuardWalk";
        }

        float rightVecDot = Vector3.Dot(_player.transform.right, _moveForward);

        if (rightVecDot >= 0.5f)
        {
            return "RightGuardWalk";
        }
        else if (rightVecDot <= -0.5f)
        {
            return "LeftGuardWalk";
        }

        return "IdleGuard";

    }

    void StateUpdate()
    {
        if(!Input.GetButton("Guard"))
        {
            _playerScript.SetPlayerState(new PlayerStateIdleScript(_player));
        }

    }
}
