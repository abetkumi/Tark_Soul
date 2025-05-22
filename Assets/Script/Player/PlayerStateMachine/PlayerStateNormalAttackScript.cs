using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

//プレイヤーの通常攻撃スクリプト
public class PlayerStateNormalAttackScript : IPlayerStateScript
{
    private GameObject _player;  //プレイヤー
    private PlayerScript _playerScript; //プレイヤーのスクリプト
    private Animator _animator;
    private bool _isContinuousAttack = false;   //連続攻撃フラグ

    public PlayerStateNormalAttackScript(GameObject InsertPlayer)
    {
        _player = InsertPlayer;
        _playerScript = _player.GetComponent<PlayerScript>();
        _animator = _player.GetComponent<Animator>();
    }

    public override void Start()
    {
        Debug.Log("攻撃");
        _animator.CrossFadeInFixedTime("NormalAttack", 0.3f);
        //_playerScript.GetSwordCollider().enabled = true;
    }

    public override void End()
    {
        _playerScript.GetSwordCollider().enabled = false;
    }

    public override void Update()
    {
        //左クリックしたら連続攻撃予約
        if (Input.GetMouseButtonDown(0))
        {
            _isContinuousAttack = true;
        }
    }

    public override void AnimationEvent(string EventName)
    {
        if(EventName == "AttackStart")
        {
            _playerScript.GetSwordCollider().enabled = true;
        }

        if(EventName == "AttackEnd")
        {
            _playerScript.GetSwordCollider().enabled = false;
        }

        if (EventName == "AnimationEnd")
        {
            Debug.Log("攻撃終了");

            _playerScript.SetPlayerState(new PlayerStateIdleScript(_player));
        }

        if(EventName == "AttackContinuationPoint")
        {

            if (_isContinuousAttack)
            {
                Debug.Log("攻撃継続");

                _animator.CrossFadeInFixedTime("attack", 0.3f);

                _isContinuousAttack = false;
            }
        }
    }

}
