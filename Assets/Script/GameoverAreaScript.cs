using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    GameObject m_playerObject;
    //リスポーンお試し用
    RespawnScript m_respawnScript;
    [SerializeField] GameObject m_saveObject;

    // Start is called before the first frame update
    void Start()
    {
        m_respawnScript = m_saveObject.GetComponent<RespawnScript>();
    }

    //プレイヤーがゲームオーバーエリアに入ると
    void OnTriggerEnter()
    {
        //リスポーンが作動しているかのお試し
        m_respawnScript.Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
