using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    [SerializeField] GameObject m_playerObject;
    [SerializeField] GameObject m_respawnObject;
    [SerializeField] GameObject m_UIObject;
    [SerializeField] GameObject m_bornFireObject;
    UI_TextScript m_UI_text;
    RespawnScript m_respawnScript;
    bool m_Save = false;

    // Start is called before the first frame update
    void Start()
    {
        m_UI_text = m_UIObject.GetComponent<UI_TextScript>();
        m_respawnScript = m_respawnObject.GetComponent<RespawnScript>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Input.GetButtonDown("Action") && m_Save == false)
            {
                SetRespawn();
                m_Save = true;
                m_bornFireObject.SetActive(true);
            }
        }
    }

    void SetRespawn()
    {
        m_UI_text.SearchUI_On("かがり火を灯した");
        Debug.Log("セーブしました。");
        m_respawnScript.m_respawnPoint = transform.position;
    }
}
