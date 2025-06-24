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
        _animator.CrossFadeInFixedTime("IdleGuard", 0.0f);
        nowAnimationName = "IdleGuard";
        _playerScript.GetGuardCollider().enabled = true;

        Debug.Log("ガード");

        _animator.SetBool("guard", true);
    }

    public override void End()
    {
        _playerScript.GetGuardCollider().enabled = false;
        _animator.SetBool("guard", false);
        _animator.SetBool("back", false);
        _animator.SetBool("forward", false);
        _animator.SetBool("right", false);
        _animator.SetBool("left", false);
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

        float forwardVecDot = Vector3.Dot(_player.transform.forward, _moveForward);

        _animator.SetBool("back", false);
        _animator.SetBool("forward", false);
        _animator.SetBool("right", false);
        _animator.SetBool("left", false);


        if (forwardVecDot >= 0.5f)
        {
            _animator.SetBool("forward", true);
            return;


        }
        else if(forwardVecDot <= -0.5f)
        {
            _animator.SetBool("back", true);
            return;

        }

        float rightVecDot = Vector3.Dot(_player.transform.right, _moveForward);

        if (rightVecDot >= 0.5f)
        {
            _animator.SetBool("right", true);
            return;


        }
        else if (rightVecDot <= -0.5f)
        {
            _animator.SetBool("left", true);
            return;


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
