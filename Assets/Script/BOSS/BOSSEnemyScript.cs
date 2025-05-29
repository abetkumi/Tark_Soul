using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;

enum BOSSStatus
{
    Idle,
    Walk,
    Attack,
    Damage,
    Death,
}

public class BOSSEnemyScript : MonoBehaviour, IDamageable
{
    //プレイヤー用変数
    [SerializeField] GameObject m_playerObject;
    PlayerScript m_playerScript;

    //ボス用変数
    BOSSStatus m_bossStatus;
    private Animator m_animator;
    private NavMeshAgent m_agent;
    //ボスのステート変更用変数
    private float m_searchArea = 30.0f;
    private float m_attackArea = 3.0f;
    public float m_attackAngleThreshold = 45.0f; //角度の閾値（度）
    private float m_speed = 0.7f;
    [SerializeField] SphereCollider m_attackCollider;
    //ボスのHP管理用変数
    public int m_bossHP = 1;
    BOSSHPBarScript m_bossHPScript;
    [SerializeField] GameObject m_bossHPObject;

    //霧用変数
    [SerializeField] GameObject m_mistObject;

    // Start is called before the first frame update
    void Start()
    {
        m_playerScript = m_playerObject.GetComponent<PlayerScript>();
        m_animator = GetComponent<Animator>();
        m_agent = GetComponent<NavMeshAgent>();
        m_bossHPScript = m_bossHPObject.GetComponent<BOSSHPBarScript>();
        doInit();
    }

    void doInit()
    {
        m_bossStatus = BOSSStatus.Idle;
        m_agent.SetDestination(transform.position);
    }

    //
    public void doSearch()
    {
        m_bossStatus = BOSSStatus.Walk;
    }

    void doMove()
    {
        Vector3 m_directionToBOSS = (transform.position - m_playerObject.transform.position).normalized;
        // プレイヤーの正面ベクトルとエネミーへの方向ベクトルの角度を取得
        float angleFromPlayer = Vector3.Angle(m_playerObject.transform.forward, m_directionToBOSS);

        // エネミーがプレイヤーの方向を向いているか
        float angleFromEnemy = Vector3.Angle(transform.forward, (m_playerObject.transform.position - transform.position).normalized);

        float distance = Vector3.Distance(m_playerObject.transform.position, transform.position);

        //ナビメッシュのターゲットをプレイヤーの位置に変更する
        m_agent.SetDestination(m_playerObject.transform.position);
        m_animator.SetFloat("Walk", m_speed);

        //攻撃ステートに移行する
        if (angleFromEnemy < m_attackAngleThreshold && Vector3.Distance(transform.position, m_playerObject.transform.position) <= m_attackArea)
        {
            m_bossStatus = BOSSStatus.Attack;
            m_agent.enabled = false;
            m_animator.SetTrigger("Attack");
            m_animator.SetFloat("Walk", 0.0f);
        }

        //待機ステートに移行する
        if (Vector3.Distance(transform.position, m_playerObject.transform.position) > m_searchArea)
        {
            m_bossStatus = BOSSStatus.Idle;
            m_agent.SetDestination(transform.position);
            m_animator.SetFloat("Walk", 0.0f);

        }
    }

    void doAttack()
    {
        
    }

    void AttackStart()
    {
        m_attackCollider.enabled = true;
    }

    private async void AttackEnd()
    {

        m_attackCollider.enabled = false;
        await UniTask.Delay(200);

        //待機ステートに移行する
        if (m_bossStatus != BOSSStatus.Death)
        {
            m_agent.enabled = true;
            m_bossStatus = BOSSStatus.Walk;
        }
        Debug.Log("ボスアタックエンド");
    }

    public void ReceivedDamage(int value)
    {
        Debug.Log("敵がダメージを受けた");

        //攻撃判定をオフに
        m_attackCollider.enabled = false;

        //HPを減らす
        m_bossHP -= value;

        m_bossHPScript.HPUpdate(m_bossHP);

        //HPが0以下ならデス
        if (m_bossHP <= 0)
        {
            m_bossStatus = BOSSStatus.Death;

            return;
        }
    }

    void doDamage()
    {
        
        if (m_bossHP <= 0.0f)
        {
            m_bossStatus = BOSSStatus.Death;
        }
        else
        {
            m_bossStatus = BOSSStatus.Walk;
        }
    }

    async void doDeath()
    {
        m_animator.SetTrigger("Death");

        await UniTask.Delay(1000);
        Destroy(m_mistObject);
        await UniTask.Delay(5000);
        Destroy(this.gameObject);
        Destroy(m_bossHPObject);
    }

    void doAnimation()
    {
        switch (m_bossStatus)
        {
            case BOSSStatus.Idle:
                break;
            case BOSSStatus.Walk:
                doMove();
                break;
            case BOSSStatus.Attack:
                doAttack();
                break;
            case BOSSStatus.Damage:
                doDamage();
                break;
            case BOSSStatus.Death:
                doDeath();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        doAnimation();
    }
}
