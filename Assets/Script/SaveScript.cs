using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    [SerializeField] GameObject m_playerObject;
    [SerializeField] GameObject m_UIObject;
    [SerializeField] GameObject m_bornFireObject;
    UI_TextScript m_UI_text;
    bool m_Save = false;

    // Start is called before the first frame update
    void Start()
    {
        m_UI_text = m_UIObject.GetComponent<UI_TextScript>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Input.GetButtonDown("Action") && m_Save == false)
            {
                Respawn();
                m_Save = true;
                m_bornFireObject.SetActive(true);
            }
        }
    }

    void Respawn()
    {
        m_UI_text.SearchUI_On("かがり火を灯した");
        Debug.Log("セーブしました。");
    }
}
