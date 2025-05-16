using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateReceiveDamageScript : IPlayerStateScript
{
    private GameObject _player;  //プレイヤー
    private PlayerScript _playerScript; //プレイヤーのスクリプト
    private Animator _animator;

    public PlayerStateReceiveDamageScript(GameObject InsertPlayer)
    {
        _player = InsertPlayer;
        _playerScript = _player.GetComponent<PlayerScript>();
        _animator = _player.GetComponent<Animator>();
    }

    public override void Start()
    {
        _animator.CrossFadeInFixedTime("ReceivedDamage", 0.3f);
        Debug.Log("被ダメージ");

    }

    public override void End()
    {

    }

    public override void Update()
    {

    }

    public override void AnimationEvent(string EventName)
    {
        if (EventName == "AnimationEnd")
        {
            _playerScript.SetPlayerState(new PlayerStateIdleScript(_player));
        }
    }

}
