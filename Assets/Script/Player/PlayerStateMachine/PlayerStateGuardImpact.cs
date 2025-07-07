using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateGuardImpact : IPlayerStateScript
{
    GameObject _player;
    PlayerScript _playerScript;
    private Animator _animator;

    public PlayerStateGuardImpact(GameObject InsertPlayer)
    {
        _player = InsertPlayer;
        _playerScript = _player.GetComponent<PlayerScript>();
        _animator = _player.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        _animator.CrossFadeInFixedTime("GuardImpact", 0.3f);
        Debug.Log("ガード受け開始");

    }

    // Update is called once per frame
    public override void Update()
    {
    }

    public override void AnimationEvent(string EventName)
    {
        if (EventName == "AnimationEnd")
        {
            _playerScript.SetPlayerState(new PlayerStateGuard(_player));
            Debug.Log("ガード受け終了");
        }
    }
}
