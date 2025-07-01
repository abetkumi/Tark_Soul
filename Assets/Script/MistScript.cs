using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MistScript : MonoBehaviour
{
    //侵入可能エリア
    [SerializeField] BoxCollider m_collider;
    [SerializeField] GameObject m_BOSSHPBarObject;
    [SerializeField] GameObject m_bossObj;
    [SerializeField] GameObject m_bgmObject;
    BOSSEnemyScript m_boss;
    BGMScript m_bgmScript;

    private void Start()
    {
        m_boss = m_bossObj.GetComponent<BOSSEnemyScript>();
        m_bgmScript = m_bgmObject.GetComponent<BGMScript>();
    }

    //プレイヤーが侵入可能エリアに入ると
    async public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            //アクションボタンで侵入可能になる
            if (Input.GetButtonDown("Action"))
            {
                m_collider.enabled = false;
                m_boss.doWalk();
                //ボスBGMを流す
                m_bgmScript.BGMStop();
                await UniTask.Delay(2000);
                m_bgmScript.BGMPlay(m_bgmScript.m_bossAudioClip);
            }   
        }
    }

    //侵入可能エリアからプレイヤーが外に出ると
    async private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //当たり判定を有効にする
            m_collider.enabled = true;

            await UniTask.Delay(1000);
            m_BOSSHPBarObject.SetActive(true);
        }
    }
}
