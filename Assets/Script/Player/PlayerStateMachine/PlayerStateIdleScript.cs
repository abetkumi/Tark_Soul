using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの待機ステート
public class PlayerStateIdleScript : IPlayerStateScript
{
    private GameObject _player;  //プレイヤー
    private PlayerScript _playerScript; //プレイヤーのスクリプト
    private Animator _animator;

    public PlayerStateIdleScript(GameObject InsertPlayer)
    {
        _player = InsertPlayer;
        _playerScript = _player.GetComponent<PlayerScript>();
        _animator = _player.GetComponent<Animator>();
    }

    public override void Start()
    {
        _animator.CrossFadeInFixedTime("Idle", 0.3f);
        Debug.Log("待機状態");

    }

    public override void End()
    {

    }

    public override void Update()
    {
        StateUpdate();
    }


    void StateUpdate()
    {
        //左クリックしたら攻撃
        if (Input.GetMouseButtonDown(0) || Input.GetButton("Attack"))
        {
            _playerScript.SetPlayerState(new PlayerStateNormalAttackScript(_player));

            return;
        }

        //右クリックしたら
        if(Input.GetMouseButtonDown(1) || Input.GetButtonDown("Dodge")) 
        {
            _playerScript.SetPlayerState(new PlayerStateRollingScript(_player));

            return;
        }

        if (Input.GetButton("Guard"))
        {
            _playerScript.SetPlayerState(new PlayerStateGuard(_player));
            return;
        }

        //スティックの入力があったら
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            _playerScript.SetPlayerState(new PlayerStateWalkScript(_player));

            return;
        }
    }
}