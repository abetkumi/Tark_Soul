using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//プレイヤー用スクリプトクラス
public class PlayerScript : MonoBehaviour, IDamageable
{
    [SerializeField] float PlayerWalkSpeed;     //プレイヤーの歩く速度
    [SerializeField] float PlayerSprintSpeed;   //プレイヤーの走る速度
    [SerializeField] float PlayerRollingSpeed;  //ロリーング回避の速度

    private Animator _animator = null;   //アニメーター
    //private float vert, horiz;  //軸入力用変数
    private CharacterController _characterController;    //プレイヤー用キャラクターコントローラ

    private PlayerStateManagerScript _playerStateManager;   //プレイヤーステートマネージャー
    private CapsuleCollider _swordCollider;    //プレイヤーの持っている剣に付けられたコライダー

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _playerStateManager = new PlayerStateManagerScript(this.gameObject);
        _swordCollider = GameObject.Find("mixamorig:Sword_joint").GetComponent<CapsuleCollider>();
    }

    public float GetWalkSpeed()
    {
        return PlayerWalkSpeed;
    }
    public float GetSprintSpeed()
    {
        return PlayerSprintSpeed;
    }

    public float GetRollingSpeed()
    {
        return PlayerRollingSpeed;
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
        //ステートを被ダメージに
        SetPlayerState(new PlayerStateReceiveDamageScript(this.gameObject));
    }
    
    public void AnimationEvent(string eventName)
    {
        _playerStateManager.AnimationEvent(eventName);
    }
}
