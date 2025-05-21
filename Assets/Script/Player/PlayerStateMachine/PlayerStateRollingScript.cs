using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーのローリングステート用スクリプト、アニメーションイベントでステートを抜ける
public class PlayerStateRollingScript : IPlayerStateScript
{
    private GameObject _player;  //プレイヤー
    private PlayerScript _playerScript; //プレイヤーのスクリプト
    private Animator _animator;
    private CharacterController _characterController;
    private bool _decelerationFlag = false;


    Vector3 _roringVec; //回避する方向

    public PlayerStateRollingScript(GameObject InsertPlayer)
    {
        _player = InsertPlayer;
        _playerScript = _player.GetComponent<PlayerScript>();
        _animator = _player.GetComponent<Animator>();
        _characterController = _player.GetComponent<CharacterController>();
    }

    public override void Start()
    {
        _animator.CrossFadeInFixedTime("ForwardRoll",0.3f);
        Debug.Log("ローリング");

        _roringVec = _player.transform.forward;

        _characterController.detectCollisions = false;


    }

    public override void End()
    {
        _characterController.detectCollisions = true;

    }

    public override void Update()
    {
        if(_decelerationFlag)
        {
            _characterController.Move(_roringVec * _playerScript.GetRollingSpeed() * Time.deltaTime * (1.5f - _animator.GetCurrentAnimatorStateInfo(0).normalizedTime));
        }
        else
        {
            _characterController.Move(_roringVec * _playerScript.GetRollingSpeed() * Time.deltaTime);
        }
    }

    public override void AnimationEvent(string EventName)
    {
        Debug.Log(EventName);

        if(EventName == "MoveEnd")
        {
            _decelerationFlag = true;
        }

        if(EventName == "AnimationEnd")
        {
            _playerScript.SetPlayerState(new PlayerStateIdleScript(_player));
        }
    }
}
