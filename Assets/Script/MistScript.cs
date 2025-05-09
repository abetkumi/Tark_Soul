using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistScript : MonoBehaviour
{
    //侵入可能エリア
    [SerializeField] BoxCollider m_collider;

    //プレイヤーが侵入可能エリアに入ると
    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            //アクションボタンで侵入可能になる
            if (Input.GetButton("Action"))
            {
                m_collider.enabled = false;
            }   
        }
    }

    //侵入可能エリアからプレイヤーが外に出ると
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //当たり判定を有効にする
            m_collider.enabled = true;
        }
    }
}
