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

    [SerializeField] int BossHP;

    //ボス用変数
    BOSSStatus m_bossStatus;
    private Animator m_animator;
    private NavMeshAgent m_agent;
    //ボスのステート変更用変数
    private float m_searchArea = 30.0f;
    private float m_attackArea = 3.0f;
    public float m_attackAngleThreshold = 45.0f; //角度の閾値（度）
    private float m_speed = 0.7f;
    private bool m_dead = false;
    [SerializeField] SphereCollider m_attackCollider;
    //ボスのHP管理用変数
    BOSSHPBarScript m_bossHPScript;
    GameObject m_BossHPBar;
    public int m_bossHP = 100;
    //Attack用変数
    bool attackFlag = false;

    //霧用変数
    [SerializeField] GameObject m_mistObject;

    //BGM用変数
    [SerializeField] GameObject m_bgmObject;
    BGMScript m_bgmScript;

    //SE用変数
    [SerializeField] AudioClip m_bossWalkSE;
    [SerializeField] AudioClip m_bossAttackSE;
    [SerializeField] AudioClip m_bossDeadSE;
    AudioSource m_audioSource;

    //
    [SerializeField] GameObject m_clearObject;
    ClearScript m_clearScript;

    // Start is called before the first frame update
    void Start()
    {
        m_clearScript = m_clearObject.GetComponent<ClearScript>();
        m_audioSource = GetComponent<AudioSource>();
        m_playerScript = m_playerObject.GetComponent<PlayerScript>();
        m_animator = GetComponent<Animator>();
        m_agent = GetComponent<NavMeshAgent>();
        m_bgmScript = m_bgmObject.GetComponent<BGMScript>();
        m_BossHPBar = UIManager.GetUIManager().NewUI(1);
        m_bossHPScript = m_BossHPBar.GetComponent<BOSSHPBarScript>();
        m_bossHPScript.Init(BossHP);
        doInit();
    }

    public void OnDestroy()
    {
        UIManager.GetUIManager().NonActiveUI("BOSSHPBar(Clone)");
    }

    void doInit()
    {
        m_bossStatus = BOSSStatus.Idle;
        m_agent.SetDestination(transform.position);
    }

    //
    public void doWalk()
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
        float angleBackFromEnemy = Vector3.Angle(transform.forward * -1.0f, (m_playerObject.transform.position - transform.position).normalized);

        float distance = Vector3.Distance(m_playerObject.transform.position, transform.position);

        //ナビメッシュのターゲットをプレイヤーの位置に変更する
        m_agent.SetDestination(m_playerObject.transform.position);
        m_animator.SetFloat("Walk", m_speed);

        //SEを鳴らす
        if(!m_audioSource.isPlaying)
        {
            m_audioSource.PlayOneShot(m_bossWalkSE);
        }
      

        //攻撃ステートに移行する
        if (angleFromEnemy < m_attackAngleThreshold && Vector3.Distance(transform.position, m_playerObject.transform.position) <= m_attackArea)
        {
            m_bossStatus = BOSSStatus.Attack;
            m_agent.enabled = false;
            m_animator.SetFloat("Walk", 0.0f);
        }
        else if (angleBackFromEnemy < m_attackAngleThreshold && Vector3.Distance(transform.position, m_playerObject.transform.position) <= m_attackArea)
        {
            m_bossStatus = BOSSStatus.Attack;
            m_agent.enabled = false;
            m_animator.SetFloat("Walk", 0.0f);
        }
    }

    void doAttack()
    {
        if (attackFlag == true)
        {
            return;
        }

        int attackType = Random.Range(0, 2);
        switch (attackType)
        {
            case 0:
                m_animator.SetTrigger("Attack1");
                attackFlag = true;
                break;
            case 1:
                m_animator.SetTrigger("Attack2");
                attackFlag = true;
                break;
            case 2:
                m_animator.SetTrigger("Attack3");
                attackFlag = true;
                break;
        }
    }

    async void AttackStart()
    {
        m_attackCollider.enabled = true;

        await UniTask.Delay(800);
        m_audioSource.PlayOneShot(m_bossAttackSE);
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
        attackFlag = false;
        Debug.Log("ボスアタックエンド");
    }

    public void ReceivedDamage(int value)
    {
        Debug.Log("敵がダメージを受けた");

        //攻撃判定をオフに
        m_attackCollider.enabled = false;

        //HPを減らす
        m_bossHPScript.DecreaseGauge(value);

        //HPが0以下ならデス
        if (m_bossHPScript.GetCurrentValue() <= 0)
        {
            m_bossStatus = BOSSStatus.Death;

            return;
        }
    }

    void doDamage()
    {
        
        if (m_bossHPScript.GetCurrentValue() <= 0)
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
        if (m_dead == true)
        {
            return;
        }

        m_dead = true;
        m_animator.SetTrigger("Death");
        m_bgmScript.BGMStop();

        //霧を削除
        await UniTask.Delay(1000);
        Destroy(m_mistObject);
        //SEを鳴らす
        m_audioSource.Stop();
        m_audioSource.pitch = 1.0f;
        m_audioSource.PlayOneShot(m_bossDeadSE);
        await UniTask.Delay(5000);

        //ボス削除
        Destroy(this.gameObject);
        
        m_clearScript.ClearActive();
        //BGMをステージBGMに変更
        m_bgmScript.BGMPlay(m_bgmScript.m_stageAudioClip);
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
