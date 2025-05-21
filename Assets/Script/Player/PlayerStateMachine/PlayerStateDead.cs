using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDead : IPlayerStateScript
{
    private GameObject _player; //プレイヤー
    private PlayerScript _playerScript; //プレイヤーのスクリプト
    private Animator _animator;

    public PlayerStateDead(GameObject insertPlayer)
    {
        _player = insertPlayer;
        _playerScript = _player.GetComponent<PlayerScript>();
        _animator = _player.GetComponent<Animator>();
    }

    public override void Start()
    {
        _animator.CrossFadeInFixedTime("Death", 0.3f);
    }
}
