using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの待機ステート
public class PlayerStateIdleScript : IPlayerStateScript
{
    private GameObject _player;  //プレイヤー
    private PlayerScript _playerScript; //プレイヤーのスクリプト

    public PlayerStateIdleScript(GameObject InsertPlayer)
    {
        _player = InsertPlayer;
    }

    public override void Start()
    {
        _playerScript = _player.GetComponent<PlayerScript>();
        _playerScript.SetAnimatorState();
    }

    public override void End()
    {

    }

    public override void Update()
    {

    }


    void StateUpdate()
    {
        
    }

}