using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



//プレイヤー用スクリプトクラス
public class PlayerScript : MonoBehaviour, IDamageable
{
    [Header("Move")]
    [SerializeField] float WalkSpeed;     //プレイヤーの歩く速度
    [SerializeField] float SprintSpeed;   //プレイヤーの走る速度
    [SerializeField] float RollingSpeed;  //ロリーング回避の速度

    [Header("Status")]
    [SerializeField] int PlayerStartHP;  //プレイヤーの初期HP
    [SerializeField] Slider PlayerHPUI;

    private PlayerHPBar _playerHPBarScript;
    private Animator _animator = null;   //アニメーター
    //private float vert, horiz;  //軸入力用変数
    private CharacterController _characterController;    //プレイヤー用キャラクターコントローラ

    private PlayerStateManagerScript _playerStateManager;   //プレイヤーステートマネージャー
    private CapsuleCollider _swordCollider;    //プレイヤーの持っている剣に付けられたコライダー
    private int _PlayerHP;




    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _playerStateManager = new PlayerStateManagerScript(this.gameObject);
        _swordCollider = GameObject.Find("mixamorig:Sword_joint").GetComponent<CapsuleCollider>();
        _PlayerHP = PlayerStartHP;
        _playerHPBarScript = PlayerHPUI.GetComponent<PlayerHPBar>();
        _playerHPBarScript.Init(PlayerStartHP);
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

    public void SetPlayerState(IPlayerStateScript state)
    {
        _playerStateManager.SetPlayerState(state);
    }

    //IDamageableから継承？した被ダメージ処理
    public void ReceivedDamage(int value)
    {
        //死亡しているなら処理を飛ばす
        if(_animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            return;
        }

        _PlayerHP -= value;

        _playerHPBarScript.HPUpdate(_PlayerHP);

        if (_PlayerHP <= 0)
        {
            Debug.Log("プレイヤーが死んだ！この人でなし！");
            SetPlayerState(new PlayerStateDead(this.gameObject));
            return;
        }

        //ステートを被ダメージに
        SetPlayerState(new PlayerStateReceiveDamageScript(this.gameObject));
    }
    
    public void AnimationEvent(string eventName)
    {
        _playerStateManager.AnimationEvent(eventName);
    }
}
