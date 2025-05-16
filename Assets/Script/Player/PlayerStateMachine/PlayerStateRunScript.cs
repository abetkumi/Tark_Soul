using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateRunScript : IPlayerStateScript
{
    private GameObject _player;  //プレイヤー
    private PlayerScript _playerScript; //プレイヤーのスクリプト
    private Animator _animator;
    private CharacterController _characterController;

    private float vert, horiz;  //軸入力用変数

    public PlayerStateRunScript(GameObject InsertPlayer)
    {
        _player = InsertPlayer;
        _playerScript = _player.GetComponent<PlayerScript>();
        _animator = _player.GetComponent<Animator>();
        _characterController = _player.GetComponent<CharacterController>();
    }

    public override void Start()
    {
        _animator.CrossFadeInFixedTime("ForwardRun", 0.3f);
        Debug.Log("ダッシュ");

    }

    public override void End()
    {

    }

    public override void Update()
    {
        StateUpdate();
        Move();
    }

    void StateUpdate()
    {
        //左クリックしたら攻撃
        if (Input.GetMouseButtonDown(0))
        {
            _playerScript.SetPlayerState(new PlayerStateNormalAttackScript(_player));

            return;
        }

        //右クリックしたら回避
        if (Input.GetMouseButtonDown(1))
        {
            _playerScript.SetPlayerState(new PlayerStateRollingScript(_player));

            return;
        }

        //スティックの入力が無くなったら
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            _playerScript.SetPlayerState(new PlayerStateIdleScript(_player));

            return;
        }

        if (!Input.GetButton("Sprint"))
        {
            _playerScript.SetPlayerState(new PlayerStateWalkScript(_player));

            return;
        }
    }

    void Move()
    {
        //Lスティックの縦横の入力を取得
        vert = Input.GetAxis("Vertical");
        horiz = Input.GetAxis("Horizontal");

        //カメラの正面ベクトルから、横(x.z)方向のベクトルを抽出し正規化
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        //Lスティックの入力とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * vert + Camera.main.transform.right * horiz;


        _characterController.Move(moveForward * _playerScript.GetSprintSpeed() * Time.deltaTime);


        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            _player.transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }
}
