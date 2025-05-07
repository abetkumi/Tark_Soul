using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public float m_bossHP = 100.0f;

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
        if (Vector3.Distance(transform.position, m_playerObject.transform.position) <= 10.0f)
        {
            m_bossStatus = BOSSStatus.Walk;
        }
    }

    void doMove()
    {

    }

    void doAttack()
    {

    }

    void doDamage()
    {
        if (m_BOSSHP <= 0.0f)
        {
            m_bossStatus = BOSSStatus.Death;
        }
    }

    void doDeath()
    {
        m_animator.SetTrigger("Die");
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
