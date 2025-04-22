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

    // Start is called before the first frame update
    void Start()
    {
        m_playerScript = m_playerObject.GetComponent<PlayerScript>();
        m_animator = GetComponent<Animator>();
        m_agent = GetComponent<NavMeshAgent>();
    }

    private void doInit()
    {
        m_enemyStatus = EnemyStatus.Idle;
    }

    private void doSearch()
    {
        if (Vector3.Distance(transform.position, m_playerPosition) < 5.0f)
        {
            m_enemyStatus = EnemyStatus.Walk;
            m_animator.SetBool("IdleFlag", false);
        }
    }
    private void doMove()
    {
        m_agent.SetDestination(m_playerPosition);
    }

    private void doAttack()
    {

    }

    private void doDamage()
    {

    }

    private void doDeath()
    {

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
                break;
            case EnemyStatus.Damage:
                m_animator.SetBool("DamageFlag", true);
                break;
            case EnemyStatus.Death:
                m_animator.SetBool("DeathFlag", true);
                m_animator.SetBool("IdleFlag", false);
                m_animator.SetBool("WalkFlag", false);
                m_animator.SetBool("AttackFlag", false);
                m_animator.SetBool("DamageFlag", false);
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
