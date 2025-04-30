using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum EnemyStatus
{
    Idle,
    Walk,
    Attack,
    Damage,
    Death,
}

public class EnemyScript : MonoBehaviour
{
    //プレイヤーの変数
    [SerializeField] GameObject m_playerObject;
    PlayerScript m_playerScript;
    Vector3 m_playerPosition;

    //Enemyステータス用変数
    EnemyStatus m_enemyStatus = EnemyStatus.Idle;
    
    //アニメーション用変数
    private Animator m_animator;
    private NavMeshAgent m_agent;

    //アタック用変数
    [SerializeField] SphereCollider m_attackCollider;
    EnemyAttackScript m_enemyAttackScript;
    public float m_rotationSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_playerScript = m_playerObject.GetComponent<PlayerScript>();
        m_enemyAttackScript = GetComponent<EnemyAttackScript>();
        m_animator = GetComponent<Animator>();
        m_agent = GetComponent<NavMeshAgent>();
    }

    private void doInit()
    {
        m_enemyStatus = EnemyStatus.Idle;
        m_agent.SetDestination(transform.position);
    }

    private void doSearch()
    {
        //プレイヤーが近づくと追跡ステートに移行する
        if (Vector3.Distance(transform.position, m_playerPosition) <= 10.0f)
        {
            m_enemyStatus = EnemyStatus.Walk;
            m_animator.SetBool("IdleFlag", false);
        }
    }
    private void doMove()
    {
        //ナビメッシュのターゲットをプレイヤーの位置に変更する
        m_agent.SetDestination(m_playerPosition);

        //攻撃ステートに移行する
        if(Vector3.Distance(transform.position,m_playerPosition) <= 3.0f)
        {
            m_enemyStatus = EnemyStatus.Attack;
            m_agent.SetDestination(transform.position);
            m_animator.SetBool("WalkFlag", false);
        }

        //待機ステートに移行する
        if (Vector3.Distance(transform.position, m_playerPosition) > 10.0f)
        {
            m_enemyStatus = EnemyStatus.Idle;
            m_agent.SetDestination(transform.position);
            m_animator.SetBool("WalkFlag", false);
        }
    }

    private void doAttack()
    {
        ////攻撃中プレイヤーの方向を向く
        //Vector3 direction = m_playerPosition - transform.position;  // プレイヤー方向のベクトル
        //direction.y = 0;
        //Quaternion m_targetRotation = Quaternion.LookRotation(direction);
        //transform.rotation = Quaternion.Slerp(transform.rotation, m_targetRotation, m_rotationSpeed * Time.deltaTime);
        
        //攻撃判定を有効にする
        m_attackCollider.enabled = true;
    }

  

    private void AttackEnd()
    {
        //プレイヤーが攻撃範囲内にいると再び攻撃
        if (Vector3.Distance(transform.position, m_playerPosition) <= 3.0f)
        {
            // エネミーの正面をプレイヤーに向ける（補完させたい）
            Vector3 direction = m_playerPosition - transform.position;  // プレイヤー方向のベクトル
            direction.y = 0;  // Y軸方向の回転を無効にして、XZ平面のみで回転させる
            transform.rotation = Quaternion.LookRotation(direction);  // プレイヤーの方向を向く
        }
        //プレイヤーが近くにいると追跡状態
        else if (Vector3.Distance(transform.position, m_playerPosition) <= 10.0f)
        {
            //ステータスを歩くに変更
            m_enemyStatus = EnemyStatus.Walk;
            m_animator.SetBool("AttackFlag", false);
            //攻撃判定を無効にする
            m_attackCollider.enabled = false;
            Debug.Log("攻撃終了");
        }
        //プレイヤーが離れていると待機状態
        else
        {
            //ステータスを待機状態に変更
            m_enemyStatus = EnemyStatus.Idle;
            m_animator.SetBool("AttackFlag", false);
            Debug.Log("攻撃終了");
        }
    }

    private void doDamage()
    {
        Debug.Log("敵がダメージを受けた");
    }

    private void doDeath()
    {
        Debug.Log("敵が4んだ");
    }

    private void doAnimation()
    {
        switch (m_enemyStatus)
        {
            case EnemyStatus.Idle:
                m_animator.SetBool("IdleFlag",true);
                doSearch();
                break;
            case EnemyStatus.Walk:
                m_animator.SetBool("WalkFlag", true);
                doMove();
                break;
            case EnemyStatus.Attack:
                m_animator.SetBool("AttackFlag", true);
                doAttack();
                break;
            case EnemyStatus.Damage:
                m_animator.SetBool("DamageFlag", true);
                doDamage();
                break;
            case EnemyStatus.Death:
                m_animator.SetBool("DeathFlag", true);
                m_animator.SetBool("IdleFlag", false);
                m_animator.SetBool("WalkFlag", false);
                m_animator.SetBool("AttackFlag", false);
                m_animator.SetBool("DamageFlag", false);
                doDeath();
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        doAnimation();
        m_playerPosition = m_playerObject.transform.position;
    }
}
