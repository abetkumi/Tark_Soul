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

    public PlayerStateNormalAttackScript(GameObject InsertPlayer)
    {
        _player = InsertPlayer;
        _playerScript = _player.GetComponent<PlayerScript>();
        _animator = _player.GetComponent<Animator>();
    }

    public override void Start()
    {
        Debug.Log("攻撃");
        _animator.CrossFadeInFixedTime("attack_1", 0.3f);
        _playerScript.GetSwordCollider().enabled = true;
    }

    public override void End()
    {
        _playerScript.GetSwordCollider().enabled = false;
    }

    public override void Update()
    {
        StateUpdate();
    }


    void StateUpdate()
    {
        if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            _playerScript.SetPlayerState(new PlayerStateIdleScript(_player));
        }
    }

}
