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

public class BOSSEnemyScript : MonoBehaviour
{
    //プレイヤー用変数
    [SerializeField] GameObject m_playerObject;
    PlayerScript m_playerScript;

    //ボス用変数
    BOSSStatus m_bossStatus;
    private Animator m_animator;
    private NavMeshAgent m_agent;
    private float m_searchArea = 30.0f;
    private float m_attackArea = 3.0f;
    private float m_speed = 0.7f;
    public float m_bossHP = 100.0f;
    [SerializeField] SphereCollider m_attackCollider;

    //霧用変数
    [SerializeField] GameObject m_mistObject;

    // Start is called before the first frame update
    void Start()
    {
        m_playerScript = m_playerObject.GetComponent<PlayerScript>();
        m_animator = GetComponent<Animator>();
        m_agent = GetComponent<NavMeshAgent>();
        doInit();
    }

    void doInit()
    {
        m_bossStatus = BOSSStatus.Idle;
        m_agent.SetDestination(transform.position);
    }

    void doSearch()
    {
        //プレイヤーが近づくと追跡ステートに移行する
        if (Vector3.Distance(transform.position, m_playerObject.transform.position) <= m_searchArea)
        {
            m_bossStatus = BOSSStatus.Walk;
        }
    }

    void doMove()
    {
        //ナビメッシュのターゲットをプレイヤーの位置に変更する
        m_agent.SetDestination(m_playerObject.transform.position);
        m_animator.SetFloat("Walk", m_speed);

        //攻撃ステートに移行する
        if (Vector3.Distance(transform.position, m_playerObject.transform.position) <= m_attackArea)
        {
            m_bossStatus = BOSSStatus.Attack;
            m_agent.SetDestination(transform.position);
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
        m_attackCollider.enabled = true;
    }

    private async void AttackEnd()
    {

        await UniTask.Delay(200);
     
        //攻撃ステートに移行する
        if (Vector3.Distance(transform.position, m_playerObject.transform.position) <= m_attackArea)
        {
            m_bossStatus = BOSSStatus.Attack;
            m_agent.SetDestination(transform.position);
            m_animator.SetTrigger("Attack");
            m_animator.SetFloat("Walk", 0.0f);
        }
        //待機ステートに移行する
        else if (Vector3.Distance(transform.position, m_playerObject.transform.position) > m_searchArea)
        {
            m_bossStatus = BOSSStatus.Idle;
            m_agent.SetDestination(transform.position);
        }
        else
        {
            m_bossStatus = BOSSStatus.Walk;
        }
        m_attackCollider.enabled = false;
        Debug.Log("ボスアタックエンド");
    }

    void doDamage()
    {
        if (m_bossHP <= 0.0f)
        {
            m_bossStatus = BOSSStatus.Death;
        }
    }

    async void doDeath()
    {
        m_animator.SetTrigger("Death");

        await UniTask.Delay(1000);
        Destroy(m_mistObject);
    }

    void doAnimation()
    {
        switch (m_bossStatus)
        {
            case BOSSStatus.Idle:
                doSearch();
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
