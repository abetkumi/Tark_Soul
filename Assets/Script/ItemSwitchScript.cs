using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSwitchScript : MonoBehaviour
{
    //当たり判定
    [SerializeField] BoxCollider m_collider;
    //開閉するゲート用変数
    [SerializeField] GameObject Gate_right;
    [SerializeField] GameObject Gate_left;

    Animator m_animator_Lever;
    Animator m_animator_Right;
    Animator m_animator_Left;

    AudioSource m_audioSource;
    public AudioClip m_leverClip;
    public AudioClip m_doorClip;

    // Start is called before the first frame update
    void Start()
    {
        m_animator_Lever = GetComponent<Animator>();
        m_animator_Right = Gate_right.GetComponent<Animator>();
        m_animator_Left = Gate_left.GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
    }

    //プレイヤーがレバーの範囲に入ると
    public void OnTriggerStay(Collider col)
    {
        if(col.tag == "Player")
        {
            //アクションボタンでゲートとレバーのアニメーションを再生する
            if (Input.GetButtonDown("Action"))
            {
                m_animator_Lever.SetTrigger("Gate Open");
                m_animator_Right.SetTrigger("Gate Open");
                m_animator_Left.SetTrigger("Gate Open");
                if (!m_audioSource.isPlaying)
                {
                    m_audioSource.PlayOneShot(m_leverClip);
                    m_audioSource.PlayOneShot(m_doorClip);
                }

                Destroy(m_collider);
            }
        }
    }
}
