using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーのステート管理クラス
public class PlayerStateManagerScript
{
    private GameObject _player;  //プレイヤー

    private IPlayerStateScript _playerState;

    //コンストラクタ
    public PlayerStateManagerScript(GameObject InsertPlayer)
    {
        _player = InsertPlayer;
        //とりあえず待機ステートで初期化
        _playerState = new PlayerStateIdleScript(InsertPlayer);
        _playerState.Start();
    }

    public void SetPlayerState(IPlayerStateScript state)
    {
        _playerState.End();

        _playerState = state;
        _playerState.Start();
    }

    //ステート処理
    public void Update()
    {
        _playerState.Update();
    }
    



}

