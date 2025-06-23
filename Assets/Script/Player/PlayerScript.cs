using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



//プレイヤー用スクリプトクラス
public class PlayerScript : MonoBehaviour, IDamageable
{
    [Header("Move")]
    [SerializeField] private float WalkSpeed;     //プレイヤーの歩く速度
    [SerializeField] private float SprintSpeed;   //プレイヤーの走る速度
    [SerializeField] private float RollingSpeed;  //ロリーング回避の速度

    [Header("Status")]
    [SerializeField] private int PlayerStartHP;  //プレイヤーの初期HP
    [SerializeField] private Slider PlayerHPUI;

    [SerializeField, Space(15)] private AudioClip []SE;

    private PlayerHPBar _playerHPBarScript;
    private Animator _animator = null;   //アニメーター
    private CharacterController _characterController;    //プレイヤー用キャラクターコントローラ

    private AudioSource _audioSource;

    private PlayerStateManagerScript _playerStateManager;   //プレイヤーステートマネージャー
    private CapsuleCollider _swordCollider;    //プレイヤーの持っている剣に付けられたコライダー
    private BoxCollider _guardCollider; //プレイヤーの前方に設置されているガード用のコライダー
    private int _PlayerHP;

    private bool _isInvincible = false; //無敵フラグ


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _playerStateManager = new PlayerStateManagerScript(this.gameObject);
        _swordCollider = GameObject.Find("mixamorig:Sword_joint").GetComponent<CapsuleCollider>();
        _guardCollider = GameObject.Find("GuardCollision").GetComponent<BoxCollider>();
        _PlayerHP = PlayerStartHP;
        _playerHPBarScript = PlayerHPUI.GetComponent<PlayerHPBar>();
        _playerHPBarScript.Init(PlayerStartHP);
        _audioSource = GetComponent<AudioSource>();
    }


    public float GetWalkSpeed()
    {
        return WalkSpeed;
    }
    public float GetSprintSpeed()
    {
        return SprintSpeed;
    }

    public float GetRollingSpeed()
    {
        return RollingSpeed;
    }
    public CapsuleCollider GetSwordCollider()
    {
        return _swordCollider;
    }
    public BoxCollider GetGuardCollider()
    {
        return _guardCollider;
    }

    //無敵中か
    public bool IsInvincible()
    {
        return _isInvincible;
    }

    // Update is called once per frame
    void Update()
    {
        _playerStateManager.Update();

        //doMove();

        //if(Input.GetMouseButtonDown(0))
        //{
        //    doAttack();
        //}
    }

    public void PlaySE(String SEName)
    {
       foreach(AudioClip se in SE)
       {
            if(se.name == SEName)
            {
                _audioSource.clip = se;

                _audioSource.Play();
            }
       }

        
    }

    public void StopSE()
    {
        _audioSource.Stop();
    }

    public void SetSEPitch(float pitch)
    {
        _audioSource.pitch = pitch;
    }

    public void SetPlayerState(IPlayerStateScript state)
    {
        _playerStateManager.SetPlayerState(state);
    }

    //IDamageableから継承？した被ダメージ処理
    public void ReceivedDamage(int value)
    {
        //無敵中なら処理を飛ばす
        if(_isInvincible)
        {
            return;
        }

        //死亡しているなら処理を飛ばす
        if(_animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            return;
        }

        //無敵時間
        StartInvincibleTime(2f);

        _PlayerHP -= value;

        _playerHPBarScript.HPUpdate(_PlayerHP);

        Debug.Log(_PlayerHP);

        if (_PlayerHP <= 0)
        {
            Debug.Log("プレイヤーが死んだ！この人でなし！");
            SetPlayerState(new PlayerStateDead(this.gameObject));
            return;
        }

        //ステートを被ダメージに
        SetPlayerState(new PlayerStateReceiveDamageScript(this.gameObject));
    }
    
    //無敵時間を設定
    //引数で何秒無敵にするか指定
    public async void StartInvincibleTime(float time)
    {
        Debug.Log("無敵時間開始");
        _isInvincible = true;

        await UniTask.Delay(TimeSpan.FromSeconds(time));
        _isInvincible = false;
        Debug.Log("無敵時間終了");

    }

    public void AnimationEvent(string eventName)
    {
        _playerStateManager.AnimationEvent(eventName);
    }
}
