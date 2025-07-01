
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;            


public class UI_TextScript : MonoBehaviour
{
    [SerializeField]
    GameObject m_messageObject;
    RectTransform m_messageObjectRectTransform;

    [SerializeField]
    TextMeshProUGUI m_messageText;

    [SerializeField]
    Animator m_animator;

    // メッセージ自動非表示
    const int Delay_TIME = 6;
    bool m_isAutoOff = false;

    //SE
    [SerializeField] GameObject seUIObject;
    UI_SEScript seUI;

    void Awake()
    {
        // RectTransformを取得しておく
        m_messageObjectRectTransform = m_messageObject.GetComponent<RectTransform>();
        //アニメーションを取得
        m_animator = m_messageText.GetComponent<Animator>();
        // 最初は非表示
        //m_messageObject.SetActive(false);
        seUI = seUIObject.GetComponent<UI_SEScript>();
        SearchUI_On("漁村");
        
    }


    // UIを表示＆更新
    // mode=false…名前表示モード mode=true…説明文表示モード
    async public void SearchUI_On(string text)
    {
        if (m_isAutoOff)
        {
            return;
        }

        // テキストを表示
        m_messageObject.SetActive(true);
        m_messageText.text = text;
        m_animator.SetBool("ON",true);

        AutoOff();
        await UniTask.Delay(700);
        //SE
        seUI.SEPlay(seUI.m_stageUI_SE);
    }


    // UIを非表示にする
    public void SearchUI_Off()
    {
        m_messageObject.SetActive(false);
        m_isAutoOff = false;
        m_animator.SetBool("ON", false);
        Debug.Log("UI非表示");
    }


    // 自動でオフにする
    async public void AutoOff()
    {
        await UniTask.Delay(3000);
        SearchUI_Off();
        
    }

}