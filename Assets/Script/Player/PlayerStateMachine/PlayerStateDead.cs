using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStateDead : IPlayerStateScript
{
    private GameObject _player; //プレイヤー
    private PlayerScript _playerScript; //プレイヤーのスクリプト
    private Animator _animator;
    private GameObject _GameOverUI;

    public PlayerStateDead(GameObject insertPlayer)
    {
        _player = insertPlayer;
        _playerScript = _player.GetComponent<PlayerScript>();
        _animator = _player.GetComponent<Animator>();
    }

    public override void Start()
    {
        _animator.CrossFadeInFixedTime("Death", 0.3f);
        _GameOverUI = UIManager.GetUIManager().NewUI(4);
        _GameOverUI.GetComponent<GameOverUI>().FadeIn();

    }

    public override void Update()
    {

    }

    public override void AnimationEvent(string EventName)
    {
        if (EventName == "AnimationEnd")
        {
            GameOver();
        }
    }

    async void GameOver()
    {
        //タイトルシーンに移動する
        await SceneManager.LoadSceneAsync("Title").ToUniTask();
    }
}
