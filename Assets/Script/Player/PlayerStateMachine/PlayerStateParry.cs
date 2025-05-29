using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateParry : IPlayerStateScript
{
    private GameObject _player;  //プレイヤー
    private PlayerScript _playerScript; //プレイヤーのスクリプト
    private Animator _animator;

    public PlayerStateParry(GameObject InsertPlayer)
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

    }
}
