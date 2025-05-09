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
        if (Input.GetMouseButtonDown(0))
        {
            _playerScript.SetPlayerState(new PlayerStateNormalAttackScript(_player));
        }

        //右クリックしたら
        if(Input.GetMouseButtonDown(1)) 
        {
            _playerScript.SetPlayerState(new PlayerStateRollingScript(_player));
        
        }

        //スティックの入力があったら
        if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            _playerScript.SetPlayerState(new PlayerStateWalkScript(_player));
        }
    }
}